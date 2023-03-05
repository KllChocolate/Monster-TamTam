using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackEnemy : MonoBehaviour
{
    public float damage;
    //public float knockback = 1f;
    public GameObject fxAttack;
    public AudioClip attackClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }


    void Update()
    {
        damage = EnemyStatus.instance.damage;

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerAttack>() != null)
        {
            audioSource.PlayOneShot(attackClip);
            Instantiate(fxAttack, transform.position, Quaternion.identity);
            //Vector2 direction = (collider.transform.position - transform.position).normalized;
            //collider.GetComponent<Rigidbody2D>().velocity = direction * knockback;
            collider.GetComponent<PlayerAttack>().TakeDamage(damage);//(damage, direction);
        }

    }

}
