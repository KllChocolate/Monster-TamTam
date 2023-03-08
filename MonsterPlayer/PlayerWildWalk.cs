using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWildWalk : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2;
    private Vector2 moveDirection = Vector2.right;
    public bool stop = false;
    public float walkTime = 2;
    private float walkTimer = 0;
    public float waitTime = 5;
    private float waitTimer = 0;
    public float findRange = 20;
    public bool incatch = false;

    private Animator animator;

    public static PlayerWildWalk instance;

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
        
        GameObject[] Net = GameObject.FindGameObjectsWithTag("Net");
        List<GameObject> netInRange = new List<GameObject>();
        //หาแล้วจัดเข้ากลุ่ม
        foreach (GameObject net in Net)
        {
            float distance = Vector2.Distance(transform.position, net.transform.position);
            //กำหนดระยะค้นหา
            if (distance <= findRange)
            {
                netInRange.Add(net);//เพิ่มเข้ากลุ่ม
            }
        }
        if (netInRange.Count > 0)//ถ้ามากกว่า 0 ตัว
        {
            if (incatch == false)
            {
                speed = 6;
                waitTime = 0;
                walkTime = 10;
                StartCoroutine(Destory());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Net"))
        {
            incatch = true;
            speed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Net"))
        {
            speed += Time.deltaTime;
        }
    }

    private IEnumerator Destory()
    {
        yield return new WaitForSeconds(1f);
        if (incatch == false)
        {
            yield return new WaitForSeconds(6f);
            Destroy(gameObject);
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
