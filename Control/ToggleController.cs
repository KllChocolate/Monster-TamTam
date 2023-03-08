using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public static ToggleController instance;
    public List<Toggle> toggleButtons = new List<Toggle>();

    private void Awake()
    {
        instance = this; 
    }
    public void OnEnable()
    {
        int numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        for (int i = 0; i < toggleButtons.Count; i++)
        {
            toggleButtons[i].isOn = (i < numPlayers);
        }
    }

    public void UpdateToggles()
    {
        int count = 0;
        foreach (Toggle button in toggleButtons)
        {
            if (button.isOn)
            {
                count++;
            }
        }
        if (count > 3)
        {
            foreach (Toggle button in toggleButtons)
            {
                if (button.isOn)
                {
                    button.isOn = false;
                    count--;
                    if (count <= 3)
                    {
                        break;
                    }
                }
            }
        }
    }
}
