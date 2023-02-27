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
    public PassData _money;

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
            StartCoroutine(comeback(players));
        }

        if (enemies.Length == 0)
        {
            winUI.SetActive(true);
            StartCoroutine(comeback(players));
            _money.Value += money;
        }
    }

    IEnumerator comeback(GameObject[] players)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Monster TamTam Lobby");
        Destroy(players[0]);
        Destroy(players[1]);
        Destroy(players[2]);
    }

}
