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
    public float speed;
    public float stoppingDistance;


    private Transform target;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Vector2 moveDirection = target.position - transform.position;
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("Run", true);
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
        }
        else animator.SetBool("Run", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback), ForceMode2D.Impulse);
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage, direction);
        }
    }
    public void TakeDamage(float damage, Vector2 direction)
    {
        float finalDamage = damage / 2f * (1 - defense / 200f);
        currentHp -= finalDamage;
        if (currentHp <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EnemyStatus>().enabled = false;
            animator.SetBool("Run", false);

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
        GameObject[] hitEnemies = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in hitEnemies)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<PlayerStatus>().TakeDamage(damage, direction);

        }
    }
}
