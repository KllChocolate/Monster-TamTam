using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTotal : MonoBehaviour
{
    public static MoneyTotal instance;

    public Text moneyText;
    public PassData money;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        moneyText.text = money.Value.ToString();
    }


    private void Update()
    {
        UpdateMoneyDisplay();
    }

    public void AddMoney(int amount)
    {
        money.Value += amount;
    }

    public void SpendMoney(int amount)
    {
        if (money.Value >= amount)
        {
            money.Value -= amount; 
        }
        else
        {
            return;
        }

    }

    private void UpdateMoneyDisplay()
    {
        moneyText.text = money.Value.ToString();
    }
}
