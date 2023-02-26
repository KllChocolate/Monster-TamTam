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
        lastEnergyUpdate = PlayerPrefs.GetFloat("lastEnergyUpdate"); // ��Ŵ��� lastEnergyUpdate �ҡ PlayerPrefs
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
        float currentTime = Time.time; // �����һѨ�غѹ
        float timeSinceLastUpdate = currentTime - lastEnergyUpdate; // �ӹǳ���ҷ���ҹ仵�����������ش����ѻവ��Ҿ�ѧ�ҹ

        if (timeSinceLastUpdate >= 60) // ��Ǩ�ͺ��Ҽ�ҹ����� 1 �ҷ��������
        {
            int energyToAdd = (int)(timeSinceLastUpdate / 60); // �ӹǳ�ӹǹ��ѧ�ҹ����ͧ����
            energy += energyToAdd;
            lastEnergyUpdate = currentTime; // �ѻവ��������ش����ѻവ��Ҿ�ѧ�ҹ� lastEnergyUpdate
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("lastEnergyUpdate", lastEnergyUpdate); // �ѹ�֡��� lastEnergyUpdate ŧ� PlayerPrefs ��������١�Դ
        PlayerPrefs.SetInt("energy", energy); // �ѹ�֡��� energy ŧ� PlayerPrefs ��������١�Դ
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
