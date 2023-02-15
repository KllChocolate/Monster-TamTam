using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanJob : MonoBehaviour
{
    private Toggle toggle;
    private GameObject PooPoo;
    public Texture2D customCursor;
    private Vector2 hotspot;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        PooPoo = GameObject.FindGameObjectWithTag("PooPoo");
        hotspot = new Vector2(customCursor.width / 2, customCursor.height / 2);

    }
    public void Update()
    {
        if (toggle.isOn)
        {
            Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
            if (PooPoo != null)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collider2d = PooPoo.GetComponent<Collider2D>();
                    if (collider2d != null && collider2d.OverlapPoint(mouseWorldPosition))
                    {
                        Destroy(PooPoo);
                    }
                }
            }
        }
        else Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
