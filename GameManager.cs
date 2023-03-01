using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject inventoryUI;
    public GameObject player;
    public Transform spawnPlayer;
    public GameObject shop;

    public Vector2 spawnLocation = new Vector2(-8.27f, 0f);

    private void Start()
    {
        player = SendPlayer.instance.player;
        OnSceneLoaded();
    }
    void Update()
    {

    }
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);

        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);

        }
    }
    public void Continue()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    public void MainScrene()
    {
        SceneManager.LoadScene("Monster TamTam MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void openshop()
    {
        shop.SetActive(true);
    }
    public void closeshop()
    {
        shop.SetActive(false);

    }
    void OnSceneLoaded()
    {
        Instantiate(player, spawnPlayer.position, Quaternion.identity);
        SendPlayer.instance.player = null;
    }
}
