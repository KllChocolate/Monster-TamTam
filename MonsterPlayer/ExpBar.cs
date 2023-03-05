using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxExp)
    {
        slider.maxValue = maxExp;
        slider.value = maxExp;
    }

    public void SetCurrentHealth(float currentExp)
    {
        slider.value = currentExp;
    }
}

