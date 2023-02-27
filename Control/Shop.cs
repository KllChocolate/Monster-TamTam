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

    public PassData money;

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
        if( money.Value >= bananaValue) 
        { 
            SpawnFood.instance.food = banana;
            bananbaText.text = "Sold";
            money.Value -= bananaValue;
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
        if (money.Value >= cherriesValue)
        {
            SpawnFood.instance.food = cherries;
            cherriesText.text = "Sold";
            money.Value -= cherriesValue;
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
        if (money.Value >= melonValue)
        {
            SpawnFood.instance.food = melon;
            melonText.text = "Sold";
            money.Value -= melonValue;
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
        if (money.Value >= KiwiValue)
        {
            money.Value -= KiwiValue;
            Inventory.instance.Add(kiwi);
        }
        
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Orange()
    {
        if (money.Value >= OrangeValue)
        {
            money.Value -= OrangeValue;
            Inventory.instance.Add(orange);

        }
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Pineapple()
    {
        if (money.Value >= PineappleValue)
        {
            money.Value -= PineappleValue;
            Inventory.instance.Add(pineapple);

        }
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public void Strawberry()
    {
        if (money.Value >= StrawberryValue)
        {
            money.Value -= StrawberryValue;
            Inventory.instance.Add(strawberry);

        }
        else
        {
            notEnoughMoney.SetActive(true);
            StartCoroutine(NotEnoughMoney());
        }
    }
    public IEnumerator NotEnoughMoney()
    {
        yield return new WaitForSeconds(0.5f);
        notEnoughMoney.SetActive(false);
    }
}
