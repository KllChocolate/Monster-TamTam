using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Upgrade")]
    public GameObject banana;
    public Text bananbaText;
    public int bananaValue;
    public GameObject cherries;
    public Text cherriesText;
    public int cherriesValue;
    public GameObject melon;
    public Text melonText;
    public int melonValue;

    [Header("Shop")]
    public int KiwiValue;
    public Item kiwi;
    public Text kiwiText;
    public int OrangeValue;
    public Item orange;
    public Text orangeText;
    public int PineappleValue;
    public Item pineapple;
    public Text pineappleText;
    public int StrawberryValue;
    public Text strawberryText;
    public Item strawberry;


    public GameObject notEnoughMoney;
    void Start()
    {
        bananbaText.text = bananaValue.ToString();
        cherriesText.text = cherriesValue.ToString();
        melonText.text = melonValue.ToString();
        kiwiText.text = KiwiValue.ToString();
        orangeText.text = OrangeValue.ToString();
        pineappleText.text = PineappleValue.ToString();
        strawberryText.text = StrawberryValue.ToString();
    }


    void Update()
    {

    }

    public void Banana()
    { 
        if( bananaValue <= MoneyTotal.instance.money) 
        { 
            SpawnFood.instance.food = banana;
            bananbaText.text = "Sold";
            MoneyTotal.instance.money -= bananaValue;
            bananaValue = 0;
        }
        else 
        { 
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Cherries()
    {
        if (cherriesValue <= MoneyTotal.instance.money)
        {
            SpawnFood.instance.food = cherries;
            cherriesText.text = "Sold";
            MoneyTotal.instance.money -= cherriesValue;
            cherriesValue = 0;
        }
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Malon()
    {
        if (melonValue <= MoneyTotal.instance.money)
        {
            SpawnFood.instance.food = melon;
            melonText.text = "Sold";
            MoneyTotal.instance.money -= melonValue;
            melonValue = 0;
        }
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Kiwi()
    {
        if (KiwiValue <= MoneyTotal.instance.money)
        {
            MoneyTotal.instance.money -= KiwiValue;
            Inventory.instance.Add(kiwi);
        }
        
        if(KiwiValue > MoneyTotal.instance.money)
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Orange()
    {
        if (OrangeValue <= MoneyTotal.instance.money)
        {
            MoneyTotal.instance.money -= OrangeValue;
            Inventory.instance.Add(orange);

        }

        if (OrangeValue > MoneyTotal.instance.money)
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Pineapple()
    {
        if (PineappleValue <= MoneyTotal.instance.money)
        {
            MoneyTotal.instance.money -= PineappleValue;
            Inventory.instance.Add(pineapple);

        }
        if (PineappleValue > MoneyTotal.instance.money)
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Strawberry()
    {
        if (StrawberryValue <= MoneyTotal.instance.money)
        {
            MoneyTotal.instance.money -= StrawberryValue;
            Inventory.instance.Add(strawberry);

        }
        if (StrawberryValue > MoneyTotal.instance.money)
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public IEnumerator NotEnoughMoney()
    {
        yield return new WaitForSeconds(1);
        notEnoughMoney.SetActive(false);
    }
}
