using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCapsule : MonoBehaviour
{
    public int amount = 100;
    public GameObject Capsule;
    private Toggle toggle;
    public LayerMask spawnLayerMask;


    public void Start()
    {
        toggle = GetComponent<Toggle>();
    }
    public void Update()
    {
        if (toggle.isOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MoneyTotal.instance.money >= amount)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        spawnPosition.z = 0f;
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, spawnLayerMask);
                        if (hit.collider != null)
                        {
                            Instantiate(Capsule, spawnPosition, Quaternion.identity);
                            MoneyTotal.instance.money -= amount;
                        }
                    }
                }
            }
        }
    }
}

