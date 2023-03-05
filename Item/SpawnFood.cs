using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour
{
    public int amount = 100;
    public GameObject apple;
    public GameObject banana;
    public GameObject cherries;
    public GameObject melon;
    public GameObject food;
    private Toggle toggle;
    public LayerMask spawnLayerMask;
    public PassData money;
    public AudioClip dropSound;
    public AudioSource audioSource;

    public static SpawnFood instance;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        toggle = GetComponent<Toggle>();
        food = apple;
    }
    public void Update()
    {
        if (toggle.isOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (money.Value >= amount)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        spawnPosition.z = 0f;
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, spawnLayerMask);
                        if (hit.collider != null)
                        {
                            Instantiate(food, spawnPosition, Quaternion.identity);
                            money.Value -= amount;
                            audioSource.PlayOneShot(dropSound);
                        }
                    }
                }
            }
        }
    }

}

