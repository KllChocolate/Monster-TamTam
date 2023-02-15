using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAddFood : MonoBehaviour
{
    private Vector2 mousePosition;
    private Rigidbody2D rigidbody2d;
    private bool isBeingClicked = false;
    private GameObject food;

    void Start()
    {
        food = GameObject.FindGameObjectWithTag("AddFood");
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            if (food != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collider2d = GetComponent<Collider2D>();

                    if (collider2d != null && collider2d.OverlapPoint(mouseWorldPosition))
                    {
                        isBeingClicked = true;
                        mousePosition = mouseWorldPosition - new Vector2(transform.position.x, transform.position.y);
                    }
                }
                else if (Input.GetMouseButton(0) && isBeingClicked)
                {
                    transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mousePosition.x,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mousePosition.y);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isBeingClicked = false;
                    rigidbody2d.velocity = Vector2.zero;
                }
            }
        
    }
}
