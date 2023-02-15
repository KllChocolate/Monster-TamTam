using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    private Toggle toggle;
    public GameObject on;
    public GameObject off;
    void Start()
    {
        toggle= GetComponentInChildren<Toggle>();
        toggle.isOn = false;
    }

    void Update()
    {
        if(toggle.isOn)
        { 
            off.SetActive(false);
            on.SetActive(true);

        }
        else
        {
            off.SetActive(true);
            on.SetActive(false);

        }
    }

}
