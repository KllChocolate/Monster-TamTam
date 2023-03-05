using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRight : MonoBehaviour
{
    public bool isButtom = false;
    private Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void leftAndright()
    {

        if (isButtom == true)
        {
            animator.Play("Left");
            isButtom = false;
        }
        else
        {
            animator.Play("Right");
            isButtom = true;
        }

    }
}
