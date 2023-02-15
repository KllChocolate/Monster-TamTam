using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpAndDown : MonoBehaviour
{
    public bool isButtom = false;
    private Animator animator;


    public void Start()
    {
        animator= GetComponent<Animator>();
    }
    public void UpAndDown()
    {

        if (isButtom == true)
        {
            animator.Play("Down");
            isButtom = false;
        }
        else
        {
            animator.Play("Up");
            isButtom = true;
        }

    }
}
