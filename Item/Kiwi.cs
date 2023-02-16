using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi : MonoBehaviour
{
    Animator animator;
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
        PlayerStatus.instance.MaxStr += 1;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

}
