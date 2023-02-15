using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandJob : MonoBehaviour
{
    private Toggle toggle;
    private GameObject player;
    public Texture2D customCursor;
    private Vector2 hotspot;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        player = GameObject.FindGameObjectWithTag("Player");
        hotspot = new Vector2(customCursor.width / 2, customCursor.height / 2);

    }
    public void Update()
    {
        if (toggle.isOn)
        {
            Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
            if (player != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collider2d = player.GetComponent<Collider2D>();
                    if (collider2d != null && collider2d.OverlapPoint(mouseWorldPosition))
                    {
                        PlayerStatusStr.instance.love();
                        PlayerStatusAgi.instance.love();
                        PlayerStatusDex.instance.love();
                        player.GetComponent<DragObject>().enabled = false;
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    PlayerStatusStr.instance.stopLove();
                    PlayerStatusAgi.instance.stopLove();
                    PlayerStatusDex.instance.stopLove();
                    player.GetComponent<DragObject>().enabled = true;
                }
            }
        }
        else Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
