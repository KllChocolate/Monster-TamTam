using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    Animator animator;
    private float capsule;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        capsule = Random.Range(300, 700);
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
        PlayerStatus.instance.timetosick += capsule;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

}
