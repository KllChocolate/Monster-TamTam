using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private Vector2 position = new Vector2(7.62f, -6.66f);
    public float speed;
    private int direction;
    private float delay;
    private float delayTimer = 3f;
    public float topPosition = -3.2f;
    public float bottomPosition = -6.66f;

    private void Update()
    {
        position.y += direction * speed * Time.deltaTime;

        if (position.y >= topPosition)
        {
            speed = 0;
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                speed = 2;
                direction = -1;
                delay = delayTimer;
            }
        }
        if (position.y <= bottomPosition)
        {
            speed = 0;
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                speed = 2;
                direction = 1;
                delay = delayTimer;
            }
        }
            transform.position = position;
    }
}
