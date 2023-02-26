using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyTotal : MonoBehaviour
{
    public static EnergyTotal instance;

    public int energy;
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
        EnergyTotal.instance.UpdateEnergy();
    }
    public void AddEnergy()
    {
        energy += 1;
        if (energy >= 300 )
        {
            energy = 300;
        }
    }

    public void SpendEnergy(int amount)
    {
        if (energy >= amount)
        {
            energy -= amount;
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
            energy += energyToAdd;
            lastEnergyUpdate = currentTime; // อัปเดตเวลาล่าสุดที่อัปเดตค่าพลังงานใน lastEnergyUpdate
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("lastEnergyUpdate", lastEnergyUpdate); // บันทึกค่า lastEnergyUpdate ลงใน PlayerPrefs เมื่อเกมถูกปิด
        PlayerPrefs.SetInt("energy", energy); // บันทึกค่า energy ลงใน PlayerPrefs เมื่อเกมถูกปิด
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
        energyText.text = energy.ToString();
    }
}
