using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInNet : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 0.5f;
    private Vector2 moveDirection = Vector2.right;
    private bool stop = false;
    public float walkTime = 5;
    private float walkTimer = 0;
    public float waitTime = 0;
    private float waitTimer = 0;

    private Animator animator;

    public static PlayerInNet instance;

    private void Awake()
    {
        instance = this;
    }
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
