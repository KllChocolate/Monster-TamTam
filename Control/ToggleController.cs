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
        // ตั้งค่า isOn ของ Toggle ให้ตรงกับจำนวน player ทั้งหมดในฉาก
        int numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        for (int i = 0; i < toggleButtons.Count; i++)
        {
            toggleButtons[i].isOn = (i < numPlayers);
        }
    }

    public void UpdateToggles()
    {
        int count = 0; // ตัวนับจำนวน toggle ที่เปิดอยู่
        foreach (Toggle button in toggleButtons)
        {
            if (button.isOn)
            {
                count++;
            }
        }
        if (count > 3) // ตรวจสอบว่ามี toggle เปิดอยู่มากกว่า 3 หรือไม่
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
