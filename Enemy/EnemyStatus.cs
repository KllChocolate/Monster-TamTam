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
    public float attackSpeed;
    public float defense;
    public float knockback = 1f;
    public float speed = 3;
    public float nearDistance = 4;
    public float stoppingDistance;
    public float fightspeed = 3;
    public float cooldown = 5;
    public float findRange = 20;
    public float lastAttackTime = -Mathf.Infinity;
    public float lastFXTime = -Mathf.Infinity;
    public bool readyAttack = false;
    public bool isAttacking = false;
    public bool isEvading = false;
    public GameObject AreaAttack;
    public GameObject fxDush;

    public static EnemyStatus instance;

    private Animator animator;
    private Vector3 direction;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AreaAttack.SetActive(false);
        animator = GetComponent<Animator>();
        currentHp = maxHp;
    }
    void Update()
    {

        //ตั้งค่าค้นหาศัตรู
        GameObject[] hitEnemies = GameObject.FindGameObjectsWithTag("Player");
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
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - closestEnemy.transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            float closestDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);
            foreach (GameObject enemy in enemiesInRange) //หาตัวใกล้สุด
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)//ชี้เป้า
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }
            if (Time.time - lastAttackTime > cooldown - attackSpeed) //เวลา-การโจมตีล่าสุด > cooldown
            {
                readyAttack = true;
                if (closestDistance > stoppingDistance) //ศัตรูที่อยู่ใกล้สุดแต่มากกว่าระยะหยุด
                {
                    animator.SetBool("Run", true);//วิ่งเข้าหา
                    transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
                }
                if (readyAttack && closestDistance <= stoppingDistance && !isAttacking)
                {
                    Attack();
                    animator.SetBool("Run", false);
                    isAttacking = true;
                }
                if (closestDistance > stoppingDistance && Time.time - lastFXTime > 0.5f)
                {
                    StartCoroutine(SpawnFX());
                }
            }
            else //ถ้าติดคูดาวจะวิ่งออกห่าง
            {
                isAttacking = false;
                animator.SetBool("Run", true);
                StartCoroutine(Evade(closestEnemy));
            }

        }

    }

    private void Attack()
    {
        //animator.SetTrigger("Attack");
        animator.SetBool("Run", false);
        AreaAttack.SetActive(true);
        lastAttackTime = Time.time;
        StartCoroutine(AttackCooldown());
        readyAttack = false;
        isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);
        AreaAttack.SetActive(false);
        yield return new WaitForSeconds(cooldown - attackSpeed);
        readyAttack = true;
    }
    private IEnumerator Evade(GameObject closestEnemy)
    {
        isEvading = true;
        float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        // สุ่มเวลาหยุดเคลื่อนที่เพื่อความสมจริง
        float randomDelay = Random.Range(0.2f, 0.8f);
        yield return new WaitForSeconds(randomDelay);

        if (distance < nearDistance)
        {
            Vector3 dirToPlay = transform.position - closestEnemy.transform.position;
            Vector3 newPos = transform.position + dirToPlay;

            // สุ่มการเคลื่อนที่ขึ้นหรือลงเพื่อหลบ
            float randomVertical = Random.Range(-1f, 1f);
            newPos.y += randomVertical;

            // สุ่มเปลี่ยนทิศทางเป็นขึ้นหรือลง
            if (Mathf.Abs(dirToPlay.x) > Mathf.Abs(dirToPlay.y))
            {
                newPos.y += Mathf.Sign(randomVertical) * Mathf.Abs(randomVertical) * (stoppingDistance / 2f);
            }
            else
            {
                newPos.x += Mathf.Sign(dirToPlay.x) * (stoppingDistance / 2f);
            }

            newPos.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            animator.SetBool("Run", true);
        }

        Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
        transform.right = direction;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }

        animator.SetBool("Run", true);

        yield return new WaitForSeconds(cooldown - attackSpeed);
        isEvading = false;
    }
    private IEnumerator SpawnFX()
    {
        lastFXTime = Time.time;
        Vector3 spawnPosition = transform.position + new Vector3(0, -0.4f, 0);
        Instantiate(fxDush, spawnPosition, Quaternion.identity);
        yield return null;
    }

    public void TakeDamage(float damage, Vector2 direction)
    {
        float finalDamage = damage / 2f * (1 - defense / 200f);
        currentHp -= finalDamage;
        if (currentHp <= 0)
        {
            readyAttack = false;
            animator.SetBool("Death", true);
            animator.SetBool("Run", false);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EnemyStatus>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hit");
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
