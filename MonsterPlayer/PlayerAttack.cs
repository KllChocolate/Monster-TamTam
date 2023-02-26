using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerAttack : MonoBehaviour
{
    [Header("BasicStatus")]
    public float maxHp;
    public float currentHp;
    public float mp;
    public float useMp;
    public bool stop = false;
    public float walkTime = 1.0f;
    private float walkTimer = 0.0f;
    public float waitTime = 0.3f;
    private float waitTimer = 0.0f;
    private Vector2 moveDirection = Vector2.right;
    public bool death = false;
    private bool isAllEnemiesDead = false;

    [Header("Attack")]
    public float damage;
    public float attackSpeed;
    public float defense;
    public float findRange = 20;
    public float speed = 3;
    public float cooldown = 2;
    public float lastAttackTime = -Mathf.Infinity;
    public float lastFXTime = -Mathf.Infinity;
    public float stoppingDistance = 1f;
    public bool isAttacking = false;
    public bool readyAttack = true;
    public bool isEvading = false;
    public GameObject AreaAttack;
    public GameObject fxDush;
    public GameObject skill;
    public GameObject beforeskill;
    public float skillTime;
    public float rangeskill = 5;

    public static PlayerAttack instance;

    private Animator animator;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AreaAttack = gameObject.transform.Find("AreaAttack").gameObject;
        animator = GetComponent<Animator>();
        StartCoroutine(LoadStatus());
        skillTime = Random.Range(10, 15);
    }


    void Update()
    {
        if (isAllEnemiesDead) IsAllEnemiesDead();
        if (!stop)
        {
            walkTimer += Time.deltaTime;
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
            if (Time.time - lastFXTime > 0.5f)
            {
                StartCoroutine(SpawnFX());
            }
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
        //ตั้งค่าค้นหาศัตรู
        GameObject[] hitEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();
        //หาแล้วจัดเข้ากลุ่ม
        foreach (GameObject enemy in hitEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            //กำหนดระยะค้นหา
            if (distance <= findRange)
            {
                enemiesInRange.Add(enemy);//เพิ่มเข้ากลุ่ม
            }
        }
        if (enemiesInRange.Count > 0)//ถ้ามากกว่า 0 ตัว
        {
            GameObject closestEnemy = enemiesInRange[0];//ตัวที่ใกล้สุด
            Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            float closestDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);
            foreach (GameObject enemy in enemiesInRange)
            {
                if (enemy.CompareTag("Player") && enemy.activeSelf && !enemy.CompareTag("Dead"))
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestEnemy = enemy;
                        closestDistance = distance;
                    }
                }
            }
            if (Time.time - lastAttackTime > cooldown - attackSpeed && readyAttack) //พร้อมโจมตี
            {
                if (closestDistance > stoppingDistance)
                {
                    // Move towards the closest enemy.
                    transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
                    animator.SetBool("Run", true);
                }
                if (closestDistance <= stoppingDistance)
                {   
                    if (readyAttack)
                    {
                        Attack();
                        isAttacking = true;
                        readyAttack = false;
                    }
                }
                if (Time.time - lastFXTime > 0.5f)
                {
                    StartCoroutine(SpawnFX());
                }
                
            }
            //สกิล
            if (skillTime <= 0 && mp >= 5 && closestDistance <= rangeskill)
            {
                Instantiate(beforeskill, transform.position, Quaternion.identity);
                animator.SetTrigger("Skill");
                StartCoroutine(UseSkill());
                skillTime = Random.Range(6, 11);
                mp -= useMp;
            }
            else
            {
                skillTime -= Time.deltaTime;
            }
        }
        if (IsAllEnemiesDead())
        {
            isAllEnemiesDead = true;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        AreaAttack.SetActive(true);
        lastAttackTime = Time.time;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        AreaAttack.SetActive(false);
        yield return new WaitForSeconds(cooldown - attackSpeed);
        readyAttack = true;

    }
    //โดนดาเมจ
    public void TakeDamage(float damage)//(float damage, Vector2 direction)
    {
        float finalDamage = damage / 2f * (1 - defense / 200f);
        currentHp -= finalDamage;
        if (currentHp <= 0)
        {
            readyAttack = false;
            animator.SetBool("Death", true);
            animator.SetBool("Run", false);
            gameObject.tag = "Dead";
            death = true;
            GetComponent<PlayerAttack>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hit");
        }

    }
    //ควันที่พื้น
    private IEnumerator SpawnFX()
    {
        lastFXTime = Time.time;
        Vector3 spawnPosition = transform.position + new Vector3(0, -0.4f, 0);
        Instantiate(fxDush, spawnPosition, Quaternion.identity);
        yield return null;
    }
    private IEnumerator LoadStatus()
    {
        yield return new WaitForSeconds(0.1f);
        damage = PlayerStatus.instance.damage;
        maxHp = PlayerStatus.instance.maxHp;
        currentHp = PlayerStatus.instance.currentHp;
        attackSpeed = PlayerStatus.instance.attackSpeed;
        defense = PlayerStatus.instance.defense;
        mp = PlayerStatus.instance.maxMp;

    }
    private IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(skill, transform.position, Quaternion.identity);
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
    private bool IsAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyStatus>().currentHp > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, findRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, rangeskill);

    }


}
