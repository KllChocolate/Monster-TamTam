using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyTotal : MonoBehaviour
{
    public static EnergyTotal instance;

    public PassData energy;
    public Text energyText;
    private float lastEnergyUpdate;

    private void Awake()
    {
        instance= this;
    }
    void Start()
    {
        StartCoroutine(EnergyTimer());
        lastEnergyUpdate = PlayerPrefs.GetFloat("lastEnergyUpdate"); // โหลดค่า lastEnergyUpdate จาก PlayerPrefs
        UpdateEnergy();

    }
    private void Update()
    {
        UpdateEnergyDisplay();
        UpdateEnergy();
    }
    public void AddEnergy()
    {
        energy.Value += 1;
        if (energy.Value >= 300 )
        {
            energy.Value = 300;
        }
    }

    public void SpendEnergy(int amount)
    {
        if (energy.Value >= amount)
        {
            energy.Value -= amount;
        }
        else
        {
            return;
        }

    }
    public void UpdateEnergy()
    {
        float currentTime = Time.time; // เก็บเวลาปัจจุบัน
        float timeSinceLastUpdate = currentTime - lastEnergyUpdate; // คำนวณเวลาที่ผ่านไปตั้งแต่ครั้งล่าสุดที่อัปเดตค่าพลังงาน

        if (timeSinceLastUpdate >= 60) // ตรวจสอบว่าผ่านไปเวลา 1 นาทีหรือไม่
        {
            int energyToAdd = (int)(timeSinceLastUpdate / 60); // คำนวณจำนวนพลังงานที่ต้องเพิ่ม
            energy.Value += energyToAdd;
            lastEnergyUpdate = currentTime; // อัปเดตเวลาล่าสุดที่อัปเดตค่าพลังงานใน lastEnergyUpdate
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("lastEnergyUpdate", lastEnergyUpdate); // บันทึกค่า lastEnergyUpdate ลงใน PlayerPrefs เมื่อเกมถูกปิด
        PlayerPrefs.SetFloat("energy", energy.Value); // บันทึกค่า energy ลงใน PlayerPrefs เมื่อเกมถูกปิด
    }


    IEnumerator EnergyTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            AddEnergy();
        }
    }

    private void UpdateEnergyDisplay()
    {
        energyText.text = energy.Value.ToString();
    }
}
