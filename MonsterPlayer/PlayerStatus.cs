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
    public float damage;
    public float attackSpeed;
    public float defense;
    public float knockback = 1;
    public float deathCooldown = 60;
    public float timepoopoo;
    public float stoppingDistance;
    public float timetosick;

    [Header("Status")]
    public float strength;
    public float agility;
    public float dexterity;

    [Header("ExpStatus")]
    public int level = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 250;

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
        currentHp = maxHp;
        currentFood = maxFood;
        animator = GetComponent<Animator>();
        strength = Random.Range(4, 11);
        agility = Random.Range(4, 11);
        dexterity = Random.Range(4, 11);
        timepoopoo = Random.Range(150, 300);
        timetosick = Random.Range(300, 800);

    }
    void Update()
    {
        maxHp = strength * 10;
        damage = strength;
        attackSpeed = agility;
        defense = dexterity;


        //Walk
        if (!stop)
        {
            walkTimer += Time.deltaTime;
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

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
            var target = GameObject.FindGameObjectWithTag("Food").transform;
            if (target == null) { return; } 
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
            var target = GameObject.FindGameObjectWithTag("Capsule").transform;
            if (target == null) { return; }
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
            Debug.Log("เข้ามาขี้");
            if (timepoopoo < 30)
            {
                Bathroom = StartCoroutine(pootime()); 
            }
            else StopCoroutine(Bathroom);
        }
        if (collider.gameObject.tag == "Enemy")
        {
            Attack();
            Vector2 direction = (collider.transform.position - transform.position).normalized * speed;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback), ForceMode2D.Impulse);
            collider.gameObject.GetComponent<EnemyStatus>().TakeDamage(damage, direction);
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
        while (currentFood >= metabolic)
        {
            yield return new WaitForSeconds(2);
            currentHp += 10f;
            currentFood -= metabolic;
            if (currentHp > maxHp) currentHp = maxHp;
            yield return new WaitForSeconds(1f);
        }
    }
    //เข้ายิม
    private IEnumerator IncreaseGymStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            strength += 1f;
            currentFood -= metabolic;
            timepoopoo -= 2f;
            yield return new WaitForSeconds(1f);
            IncreaseExperience(10);
        }
    }
    //สนามเด็กเล่น
    private IEnumerator IncreasePlaygroundStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            agility += 1f;
            currentFood -= metabolic;
            timepoopoo -= 2f;
            yield return new WaitForSeconds(1f);
            IncreaseExperience(10);
        }
    }
    //ห้องซ่อมมวย
    private IEnumerator IncreaseBoxingStats()
    {
        yield return new WaitForSeconds(2);
        while (currentFood >= metabolic)
        {
            dexterity += 1f;
            currentFood -= metabolic;
            timepoopoo -= 2f;
            yield return new WaitForSeconds(1f);
            IncreaseExperience(10);
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Attack();
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback), ForceMode2D.Impulse);
            collision.gameObject.GetComponent<EnemyStatus>().TakeDamage(damage, direction);
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
        //animator.SetTrigger("Attack");
        GameObject[] hitEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in hitEnemies)
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
