using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandJob : MonoBehaviour
{
    private Toggle toggle;
    public Texture2D customCursor;
    private Vector2 hotspot;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        hotspot = new Vector2(customCursor.width / 2, customCursor.height / 2);

    }
    public void Update()
    {
        if (toggle.isOn)
        {
            Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider2d = player.GetComponent<Collider2D>();
                float distance = Vector2.Distance(mouseWorldPosition, collider2d.bounds.center); // �ӹǳ������ҧ�����ҧ�������� player
                if (collider2d != null && distance <= collider2d.bounds.extents.magnitude*2) // ����������������㹾�鹷��ͧ player
                {
                    PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
                    if (player != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            playerStatus.love();
                            player.GetComponent<DragObject>().enabled = false;
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            playerStatus.stopLove();
                            player.GetComponent<DragObject>().enabled = true;
                        }
                    }
                }
                else // �����������͡�ҡ��鹷��ͧ player
                {
                    PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
                    if (playerStatus.isLoving) // ������� player ���ѧ�١�غ
                    {
                        playerStatus.stopLove();
                        player.GetComponent<DragObject>().enabled = true;
                    }
                }
            }
        }
        else Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

