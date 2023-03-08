using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseMoveCamera : MonoBehaviour
{
    public GameObject Camera;
    private CameraController _Camera;
    private Toggle toggle;

    void Start()
    {
        _Camera = Camera.GetComponent<CameraController>();
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle.isOn)
        {
            _Camera.enabled = true;
        }
        else _Camera.enabled = false;
    }
}
