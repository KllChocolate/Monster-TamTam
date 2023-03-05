using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpEnemyBar : MonoBehaviour
{
    public Slider slider;
    public GameObject enemy;
    public EnemyStatus enemyStatus;
    void Start()
    {
        slider = GetComponent<Slider>();
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        SetPlayer();
    }
    public void Update()
    {
        UpdateSlider();
    }
    public void SetPlayer()
    {
        slider.maxValue = enemyStatus.maxMp;
        slider.value = enemyStatus.currentMp;
    }
    public void UpdateSlider()
    {
        slider.value = enemyStatus.currentMp;
    }
}
