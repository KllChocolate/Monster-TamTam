using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTotal : MonoBehaviour
{
    public static MoneyTotal instance;

    public int money = 0;
    public Text moneyText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    private void Update()
    {
        UpdateMoneyDisplay();
    }

    public void AddMoney(int amount)
    {
        money += amount;

    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        { 
            money -= amount; 
        }
        else
        {
            return;
        }

    }

    private void UpdateMoneyDisplay()
    {
        moneyText.text = money.ToString();
    }
}
