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
    public AudioClip victorySound;
    public AudioClip loseSound;

    public AudioSource audioSource;
    public static ArenaManager instance;

    public void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        time += Time.deltaTime;
        audioSource = GetComponentInChildren<AudioSource>();
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
            audioSource.PlayOneShot(loseSound);
            loseUI.SetActive(true);
            StartCoroutine(comeback());
        }

        if (enemies.Length == 0)
        {
            audioSource.PlayOneShot(victorySound);
            winUI.SetActive(true);
            StartCoroutine(comeback());
            _money.Value += money;
        }
    }

    IEnumerator comeback()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Monster TamTam Game");
    }

}
