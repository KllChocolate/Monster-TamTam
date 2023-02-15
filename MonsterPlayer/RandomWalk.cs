using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector2 moveDirection = Vector2.right;
    private Animator animator;
    private bool stop = false;
    public float walkTime = 2.0f;
    private float walkTimer = 0.0f;
    public float waitTime = 1.0f;
    public float waitTimer = 0.0f;


    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (!stop)
        {
            walkTimer += Time.deltaTime;
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
            
            animator.SetBool("Run", true);
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            if (walkTimer >= walkTime)
            {
                StopWalking();
            }
        }
        else
        {
            animator.SetBool("Run", false);
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                ResumeWalking();
                waitTimer = 0.0f;
            }
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
}
