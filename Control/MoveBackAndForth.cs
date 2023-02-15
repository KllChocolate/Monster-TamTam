using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public Vector2 startPosition = new Vector2(12, -2.9f);
    public Vector2 endPosition = new Vector2(-11, -2.9f);
    private Vector2 position;
    public float speed = 2f;
    private int direction;
    public float delay = 3f;
    private float delayTimer;

    private Animator animator;

    private void Start()
    {
        position = startPosition;
        delayTimer = delay;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        position.x += direction * speed * Time.deltaTime;
        animator.SetBool("Run", true);

        if (position.x >= startPosition.x)
        {
            speed = 0;
            delay -= Time.deltaTime;
            animator.SetBool("Run", false);
            if (delay <= 0)
            {
                speed = 2;
                direction = -1;
                delay = delayTimer;
                transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
            }
        }
        if (position.x <= endPosition.x)
        {
            speed = 0;
            delay -= Time.deltaTime;
            animator.SetBool("Run", false);
            if (delay <= 0)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                speed = 2;
                direction = 1;
                delay = delayTimer;
            }
        }
        transform.position = position;
    }
}
