using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;
    public int money;
    public float timer = 3;
    public float time = 0;

    public static ArenaManager instance;

    public void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        time += Time.deltaTime;    
    }
    void Update()
    {
        if (time >= timer)
        {
            check();
            time = 0;
        }
        time += Time.deltaTime;
    }

    public void check()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (players.Length == 0)
        {
            loseUI.SetActive(true);
            StartCoroutine(comeback());
        }

        if (enemies.Length == 0)
        {
            winUI.SetActive(true);
            MoneyTotal.instance.money += money;
            StartCoroutine(comeback());
            Destroy(players[0]);
            Destroy(players[1]);
            Destroy(players[2]);
        }
    }

    IEnumerator comeback()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Monster TamTam Lobby");

    }

}
