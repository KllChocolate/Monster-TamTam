using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerStatus : MonoBehaviour
{
    [Header("PictureStatus")]
    public GameObject zzzUI;
    public GameObject stomachUI;
    public GameObject pooUI;
    public GameObject poopoo;
    public GameObject injured;
    public GameObject sick;
    public GameObject Love;

    [Header("Movement")]
    public float speed = 2;
    private Vector2 moveDirection = Vector2.right;
    private bool stop = false;
    public float walkTime = 2.0f;
    private float walkTimer = 0.0f;
    public float waitTime = 1.0f;
    private float waitTimer = 0.0f;

    [Header("BasicStatus")]
    public float maxHp = 100;
    public float currentHp;
    public float maxFood = 100;
    public float currentFood;
    public float metabolic = 20;

    [Header("TimeCooldown")]
    public float knockback = 1;
    public float deathCooldown = 60;
    public float timepoopoo;
    public float stoppingDistance = 0.5f;
    public float timetosick;
    public float chackDistance = 4;

    [Header("Status")]
    public float MaxStr;
    public float MinStr;
    public float strength;
    public float MaxAgi;
    public float MinAgi;
    public float agility;
    public float MaxDex;
    public float MinDex;
    public float dexterity;

    [Header("ExpStatus")]
    public int level = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 250;

    [Header("Attack")]
    public float damage;
    public float attackSpeed;
    public float defense;
    public float attackRange;
    public LayerMask enemyLayer;

    private Animator animator;

    public static PlayerStatus instance;
    private Transform target;

    private Coroutine Nursing;
    private Coroutine Gym;
    private Coroutine PlayGround;
    private Coroutine Boxing;
    private Coroutine Bathroom;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentFood = maxFood;
        animator = GetComponent<Animator>();
        strength = Random.Range(4, 11);
        agility = Random.Range(4, 11);
        dexterity = Random.Range(4, 11);
        timepoopoo = Random.Range(150, 300);
        timetosick = Random.Range(300, 800);
        maxHp = strength * 10;
        currentHp = maxHp;
    }
    void Update()
    {
        maxHp = strength * 10;
        damage = strength;
        attackSpeed = agility *0.1f;
        defense = dexterity;


        //Walk
        if (!stop)
        {
            walkTimer += Time.deltaTime;
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime * attackSpeed;

            animator.SetBool("Run", true);
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            if (walkTimer >= walkTime)
            {
                StopWalking();
            }
        }
        else
        {
            animator.SetBool("Run", false);
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                ResumeWalking();
                waitTimer = 0.0f;
            }
        }
        //อาหารเสริม
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, chackDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("AddFood"))
            {
                target = hitCollider.transform;
                float distance = Vector2.Distance(transform.position, target.position);
                if (distance > stoppingDistance)
                {
                    animator.SetBool("Run", true);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 0.5f);
                }
                if (distance <= stoppingDistance)
                {
                    StartCoroutine(Eat());
                    animator.SetBool("Run", false);
                    animator.SetTrigger("Eat");
                }
            }
        }
        //Hungy

        if (currentFood >= 50)
        {
            stomachUI.SetActive(false);
        }
        if (currentFood > 0) currentFood -= Time.deltaTime * 0.2f;
        else if (currentFood <= 0)
        {
            currentFood = 0;
            deathCooldown -= Time.deltaTime;
            timetosick -= Time.deltaTime * 10;
        }

        if (currentFood < 30)
        {
            stomachUI.SetActive(true);
            stop = true;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Food"))
                {
                    target = hitCollider.transform;
                    float distance = Vector2.Distance(transform.position, target.position);
                    if (distance > stoppingDistance)
                    {
                        animator.SetBool("Run", true);
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 0.5f);
                    }
                    if (distance <= stoppingDistance)
                    {
                        StartCoroutine(Eat());
                        animator.SetBool("Run", false);
                        animator.SetTrigger("Eat");
                    }
                }
            }
        }
        //Tired
        if (currentHp >= 80)
        {
            zzzUI.SetActive(false);
            animator.SetBool("Sleep", false);
        }
        if (currentHp <= 30) zzzUI.SetActive(true);
        else if (currentHp <= 0)
        {
            animator.SetBool("Sleep", true);
            animator.SetBool("Run", false);
            currentHp = 0;
        }
        //Poo
        if (timepoopoo > 0) timepoopoo -= Time.deltaTime; pooUI.SetActive(false);
        if (timepoopoo < 30) pooUI.SetActive(true);
        if (timepoopoo <= 0)
        {
            timepoopoo = Random.Range(150, 300);
            Vector3 spawnPosition = transform.position;
            Instantiate(poopoo, spawnPosition, Quaternion.identity);
            pooUI.SetActive(false);
            timetosick = -30;
        }

        //Sick
        if (timetosick <= 0)
        {
            timetosick = 0;
            sick.SetActive(true);
            stop = true;
            deathCooldown -= Time.deltaTime;
            if (currentFood < 30)
            {
                stomachUI.SetActive(true);
                stop = true;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Capsule"))
                    {
                        target = hitCollider.transform;
                        float distance = Vector2.Distance(transform.position, target.position);
                        if (distance > stoppingDistance)
                        {
                            animator.SetBool("Run", true);
                            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 0.5f);
                        }
                        if (distance <= stoppingDistance)
                        {
                            StartCoroutine(Eat());
                            animator.SetBool("Run", false);
                            animator.SetTrigger("Eat");
                        }
                    }
                }
            }
        }
        if (timetosick > 0)
        {
            sick.SetActive(false);
            timetosick -= Time.deltaTime;
        }

        if (deathCooldown <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(DeathCharacter());
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chackDistance);

        Gizmos.color = Color.blue;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chackDistance);

        /*foreach (Collider2D collider in colliders)
        {
            Gizmos.DrawWireSphere(collider.transform.position, 0.5f);
        }*/
    }

    //เข้าห้องแล้วเกิดอะไร
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Nursing"))
        {
            Nursing = StartCoroutine(IncreaseNursingStats());
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Gym"))
        {
            Gym = StartCoroutine(IncreaseGymStats());
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Playground"))
        {
            PlayGround = StartCoroutine(IncreasePlaygroundStats());
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Boxing"))
        {
            Boxing = StartCoroutine(IncreaseBoxingStats());
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bathroom"))
        {
            if (timepoopoo < 30)
            {
                Bathroom = StartCoroutine(pootime()); 
            }
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Arena"))
        {
            GetComponent<DragObject>().enabled = false;
            if (collider.gameObject.CompareTag("Enemy"))
            {
                target = collider.gameObject.transform;
                float distance = Vector2.Distance(transform.position, target.position);
                Vector2 direction = (collider.transform.position - transform.position).normalized * speed;
                if (distance > stoppingDistance)
                {
                    animator.SetBool("Run", true);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 0.5f);
                }
                if (distance <= stoppingDistance)
                {
                    Attack();
                    collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback), ForceMode2D.Impulse);
                    collider.gameObject.GetComponent<EnemyStatus>().TakeDamage(damage, direction);
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Nursing"))
        {
            StopCoroutine(Nursing);
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Gym"))
        {
            StopCoroutine(Gym);
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Playground"))
        {
            StopCoroutine(PlayGround);
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Boxing"))
        {
            StopCoroutine(Boxing);
        }

    }
    //ห้องพยาบาล
    private IEnumerator IncreaseNursingStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            currentHp += 10;
            currentFood -= metabolic;
            if (currentHp > maxHp) currentHp = maxHp;
            yield return new WaitForSeconds(1f);
            if (currentHp == maxHp) break;

        }
    }
    //เข้ายิม
    private IEnumerator IncreaseGymStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            strength += 1;
            agility -= 1;
            currentFood -= metabolic;
            timepoopoo -= 2;
            IncreaseExperience(10);
            if (strength >= MaxStr) strength = MaxStr;
            if (agility <= MinAgi) agility= MinAgi; 
            yield return new WaitForSeconds(1f);
        }
    }
    //สนามเด็กเล่น
    private IEnumerator IncreasePlaygroundStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            agility += 1;
            dexterity -= 1;
            currentFood -= metabolic;
            timepoopoo -= 2;
            IncreaseExperience(10);
            if (agility >= MaxAgi) agility = MaxAgi;
            if (dexterity <= MinDex) dexterity = MinDex;
            yield return new WaitForSeconds(1f);
        }
    }
    //ห้องซ่อมมวย
    private IEnumerator IncreaseBoxingStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            dexterity += 1;
            strength -= 1;
            currentFood -= metabolic;
            timepoopoo -= 2;
            IncreaseExperience(10);
            if (dexterity >= MaxDex) dexterity = MaxDex;
            if (strength <= MinStr) strength = MinStr;
            yield return new WaitForSeconds(1f);
        }
    }
    //ระบบเลเวล
    public void IncreaseExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= experienceToNextLevel)
        {
            level++;
            currentExperience -= experienceToNextLevel;
            experienceToNextLevel = (int)(experienceToNextLevel * 1.5f);
            maxFood = maxFood * 1.5f;
            currentHp = maxHp;
        }
    }

    public void TakeDamage(float damage, Vector2 direction)
    {
        float finalDamage = damage / 2f * (1 - defense / 200f);
        currentHp -= finalDamage;
        if (currentHp <= 0)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            animator.SetTrigger("Hit");
        }

    }
    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<EnemyStatus>().TakeDamage(damage, direction);
        }
    }
    public void StopWalking()
    {
        stop = true;
        walkTimer = 0.0f;
    }

    public void ResumeWalking()
    {
        stop = false;
        moveDirection = Random.insideUnitCircle;
    }
    public IEnumerator pootime()
    {
        Debug.Log("ขี้จะออกแล้ว");
        yield return new WaitForSeconds(3);
        Vector3 spawnPosition = transform.position;
        Instantiate(poopoo, spawnPosition, Quaternion.identity);
        timepoopoo = Random.Range(150, 300);
        pooUI.SetActive(false);

    }
    public IEnumerator Eat()
    {
        yield return new WaitForSeconds(4);
        stop = false;
    }
    public IEnumerator DeathCharacter()
    {
        yield return new WaitForSeconds(1);
        GetComponent<PlayerStatus>().enabled = false;
        Destroy(gameObject, 5);
    }

    public void love()
    {
        stop = true;
        Love.SetActive(true);
    }
    public void stopLove()
    {
        Love.SetActive(false);
        stop = false;
    }


}
