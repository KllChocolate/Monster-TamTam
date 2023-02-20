using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackEnemy : MonoBehaviour
{
    public float damage;
    public float knockback = 1f;
    public GameObject fxAttack;
    public float findRange;

    void Start()
    {
        findRange = EnemyStatus.instance.findRange;
    }

    void Update()
    {
        damage = EnemyStatus.instance.damage;

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerAttack>() != null)
        {
            Instantiate(fxAttack, transform.position, Quaternion.identity);
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            collider.GetComponent<Rigidbody2D>().velocity = direction * knockback;
            collider.GetComponent<PlayerAttack>().TakeDamage(damage, direction);
        }

    }

}
