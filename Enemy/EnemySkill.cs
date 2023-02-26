using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public float damage = 30;
    public float speed = 4;
    public float maxDistance = 5;
    public float currentDistance = 0;
    private Transform player;
    private Vector2 target;
    public GameObject effectHit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (Vector2)transform.position + (Vector2)(player.transform.position - transform.position).normalized * maxDistance;
    }

    void Update()
    {
        if (currentDistance < maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            currentDistance += speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Instantiate(effectHit, transform.position, Quaternion.identity);
            collider.GetComponent<PlayerAttack>().TakeDamage(damage);
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
