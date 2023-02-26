using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outdoor : MonoBehaviour
{
    private Toggle toggle;
    public GameObject OutdoorUI;
    public GameObject ArenaUI;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void Update()
    {
        if (toggle.isOn)
        {
            OutdoorUI.SetActive(true);
        }
        else OutdoorUI.SetActive(false);
    }

    public void openArena()
    {
        Time.timeScale = 0;
        ArenaUI.SetActive(true);
    }
    public void closeArena()
    {
        Time.timeScale = 1;
        ArenaUI.SetActive(false);
    }

}
