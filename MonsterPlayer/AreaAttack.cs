using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public float damage;
    public float knockback = 1f;
    public GameObject fxAttack;


    void Start()
    {

    }

    void Update()
    {
        damage = PlayerAttack.instance.damage;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<EnemyStatus>() != null)
        {
            Instantiate(fxAttack, transform.position, Quaternion.identity);
            //Vector2 direction = (collider.transform.position - transform.position).normalized;
            //collider.GetComponent<Rigidbody2D>().velocity = direction * knockback;
            collider.GetComponent<EnemyStatus>().TakeDamage(damage);//(damage, direction);
        } 
        
    }

}
