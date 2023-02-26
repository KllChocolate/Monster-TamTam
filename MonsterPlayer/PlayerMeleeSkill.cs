using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeSkill : MonoBehaviour
{
    public float damage;
    private Transform enemy;
    private Vector2 target;


    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        target = (Vector2)transform.position + (Vector2)(enemy.transform.position - transform.position).normalized;
        damage = PlayerStatus.instance.intelligent * 10;
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
        if (collider.GetComponent<EnemyStatus>() != null)
        {
            collider.GetComponent<EnemyStatus>().TakeDamage(damage);
        }
    }
    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
