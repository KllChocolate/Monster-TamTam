using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Animator animator;

    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
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
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
        }
        else animator.SetBool("Run", false);
    }
}
