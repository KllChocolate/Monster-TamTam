using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemyBar : MonoBehaviour
{
    public Slider slider;
    public GameObject enemy;
    public EnemyStatus enemyStatus;
    void Start()
    {
        slider= GetComponent<Slider>();
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        SetPlayer();
    }
    public void Update()
    {
        UpdateSlider();
    }
    public void SetPlayer()
    {
        slider.maxValue = enemyStatus.maxHp;
        slider.value = enemyStatus.currentHp;
    }
    public void UpdateSlider()
    {
        slider.value = enemyStatus.currentHp;
    }
}
