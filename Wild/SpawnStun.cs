using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnStun : MonoBehaviour
{
    public GameObject Stun;
    private Toggle toggle;
    public AudioClip dropSound;
    public AudioSource audioSource;
    public float timecooldown = 0;
    public bool readyToStun = true;

    public void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        toggle = GetComponent<Toggle>();
    }
    public void Update()
    {
        timecooldown += Time.deltaTime;
        if (toggle.isOn)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (Input.GetMouseButtonDown(0) && readyToStun && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            PlayerWildWalk playerWildWalk = hit.collider.GetComponent<PlayerWildWalk>();
                            if (playerWildWalk != null)
                            {
                                playerWildWalk.waitTime += 5;
                                playerWildWalk.stop = true;
                            }
                            Instantiate(Stun, hit.point, Quaternion.identity);
                            audioSource.PlayOneShot(dropSound);
                            timecooldown = 0;
                            readyToStun = false;
                            break;
                        }
                    }
                }
            }
        }
        if (timecooldown >= 5)
        {
            timecooldown = 5;
            readyToStun = true; 
        }
    }
}

