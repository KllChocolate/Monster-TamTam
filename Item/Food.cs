using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Animator animator;
    public float food;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(eat());
        }
    }
    IEnumerator eat()
    {
        yield return new WaitForSeconds(2f);
        PlayerStatusStr.instance.currentFood += food;
        PlayerStatusAgi.instance.currentFood += food;
        PlayerStatusDex.instance.currentFood += food;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

}
