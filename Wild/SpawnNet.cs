using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNet : MonoBehaviour
{
    public GameObject net;
    private Toggle toggle;
    public AudioClip dropSound;
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        toggle = GetComponent<Toggle>();
    }
    public void Update()
    {
        if (toggle.isOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnPosition.z = 0f;
                    Instantiate(net, spawnPosition, Quaternion.identity);
                    audioSource.PlayOneShot(dropSound);
                }

            }
        }
    }
}

