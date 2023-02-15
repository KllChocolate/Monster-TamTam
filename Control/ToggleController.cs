using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle[] toggleButtons;

    private void Start()
    {
        for (int i = 1; i < toggleButtons.Length; i++)
        {
            toggleButtons[i].isOn = false;
        }
    }

    public void OnToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            for (int i = 0; i < toggleButtons.Length; i++)
            {
                if (toggleButtons[i] != toggle)
                {
                    toggleButtons[i].isOn = false;
                }
            }
        }
    }
}
