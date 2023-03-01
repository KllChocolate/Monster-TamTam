using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStatus : MonoBehaviour
{
    [Header("BasicStatus")]
    public float maxHp = 100;
    public float currentHp;
    public float damage;
    public float defense;
    public float mp;
    public float useMp;

    public float speed = 3;
    public float stoppingDistance;
    public float cooldown = 5;
    public float findRange = 20;
    public float lastAttackTime = -Mathf.Infinity;
    public float lastFXTime = -Mathf.Infinity;
    public bool readyAttack = true;
    public bool isAttacking = false;
    public GameObject AreaAttack;
    public GameObject fxDush;
    public GameObject skill;
    public GameObject beforeskill;
    public float skillTime;
    public float rangeskill = 5;
    public bool isAllEnemiesDead = false;
    public bool stop = false;
    public float walkTime = 1.0f;
    private float walkTimer = 0.0f;
    public float waitTime = 0.3f;
    private float waitTimer = 0.0f;
    private Vector2 moveDirection = Vector2.right;

    public static EnemyStatus instance;

    private Animator animator;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AreaAttack.SetActive(false);
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        skillTime = Random.Range(10, 15);
        mp = Random.Range(5, 15);
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
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            if (walkTimer >= walkTime)
            {
                StopWalking();
            }
        }
        else
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                ResumeWalking();
                waitTimer = 0.0f;
            }
        }
        //��駤�Ҥ����ѵ��
        GameObject[] hitEnemies = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> enemiesInRange = new List<GameObject>();
        //�����ǨѴ��ҡ����
        foreach (GameObject enemy in hitEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            //��˹����Ф���
            if (distance <= findRange)
            {
                enemiesInRange.Add(enemy);//������ҡ����
            }
        }
        if (enemiesInRange.Count > 0)//����ҡ���� 0 ���
        {
            GameObject closestEnemy = enemiesInRange[0];//��Ƿ������ش
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
            if (Time.time - lastAttackTime > cooldown && readyAttack) //���������
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
                        animator.SetBool("Run", false);
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
            //ʡ��
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
            isAllEnemiesDead = true; // ��˹�����ѵ�ٵ���������
        }

    }

    private void Attack()
    {
        //animator.SetTrigger("Attack");
        AreaAttack.SetActive(true);
        lastAttackTime = Time.time;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        AreaAttack.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        readyAttack = true;

    }

    private IEnumerator SpawnFX()
    {
        lastFXTime = Time.time;
        Vector3 spawnPosition = transform.position + new Vector3(0, -0.4f, 0);
        Instantiate(fxDush, spawnPosition, Quaternion.identity);
        yield return null;
    }
    private IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(skill, transform.position, Quaternion.identity);
    }

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
            GetComponent<EnemyStatus>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hit");
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
    private bool IsAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<PlayerAttack>().currentHp > 0)
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, rangeskill);
    }
}
