using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeSkill : MonoBehaviour
{
    public float damage = 50;
    private Transform player;
    private Vector2 target;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (Vector2)transform.position + (Vector2)(player.transform.position - transform.position).normalized;
    }

    void Update()
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine(DestroyBullet());
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerAttack>() != null)
        {
            collider.GetComponent<PlayerAttack>().TakeDamage(damage);
        }
    }
    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
