using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public PlayerAttack playerAttack;

    public static HpBar instance;

    private void Awake()
    {
        slider = GetComponent<Slider>(); 
        instance = this;
    }
    private void Start()
    {
        slider = GetComponent<Slider>();
        playerAttack = player.GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        SetPlayer();
        UpdateSlider();
    }
    public void SetPlayer()
    {
        slider.maxValue = playerAttack.maxHp;
        slider.value = playerAttack.currentHp;
    }
    public void UpdateSlider()
    {
        slider.value = playerAttack.currentHp;
    }

}

