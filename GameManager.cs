using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject monsterUI;
    public GameObject pauseUI;
    public GameObject inventoryUI;
    public GameObject AreYouSureUI;
    public GameObject str;
    public GameObject agi;
    public GameObject dex;
    public GameObject spawn;
    public Transform spawnPlayer;
    public GameObject shop;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player != null) 
        { 
            monsterUI.SetActive(false);
        }
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
        shop.SetActive(!shop.activeSelf);
    }
    public void closeshop()
    {
        shop.SetActive(false);

    }
    public void StrSelect()
    {
        spawn = str;
        AreYouSureUI.SetActive(true);
    }
    public void AgiSelect()
    {
        spawn = agi;
        AreYouSureUI.SetActive(true);
    }
    public void DexSelect()
    {
        spawn = dex;
        AreYouSureUI.SetActive(true);
    }
    public void Yes()
    {
        Instantiate(spawn, spawnPlayer.position, Quaternion.identity);
        monsterUI.SetActive(false);
    }
    public void No()
    {
        AreYouSureUI.SetActive(false);
    }

}
