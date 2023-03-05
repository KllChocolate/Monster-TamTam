using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public PlayerAttack playerAttack;

    public static MpBar instance;

    private void Awake()
    {
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
        slider.maxValue = playerAttack.maxMp;
        slider.value = playerAttack.currentMp;

    }
    public void UpdateSlider()
    {
        slider.value = playerAttack.currentMp;
    }

}

