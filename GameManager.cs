using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject InventoryUI;
    public GameObject AreYouSureUI;
    public GameObject AreYouSureRestartUI;
    public GameObject AreYouSureExitUI;
    public GameObject AreYouSureMainmanuUI;
    public GameObject MonsterSelectUI;
    public GameObject str;
    public GameObject agi;
    public GameObject dex;
    public GameObject spawn;
    public GameObject shop;

    public Vector2 spawnLocation = new Vector2(-8.27f, 0f);

    private void Start()
    {
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


    public void MainScrene()
    {
        SceneManager.LoadScene("Monster TamTam MainMenu");
    }
    public void GoToSaveGame()
    {
        SceneManager.LoadScene("Monster TamTam SaveScene");
    }
    public void GoToLoadGame()
    {
        SceneManager.LoadScene("Monster TamTam LoadScene");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Monster TamTam Lobby");
    }
    public void ExitGame()
    {
        Application.Quit();
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
        MonsterSelectUI.SetActive(false);
        Instantiate(spawn, spawnLocation, Quaternion.identity);
    }
    public void No()
    {
        AreYouSureUI.SetActive(false);
    }
    public void Restart()
    {
        AreYouSureRestartUI.SetActive(true);
    }
    public void NoRestart()
    {
        AreYouSureRestartUI.SetActive(false);
    }
    public void Exit()
    {
        AreYouSureExitUI.SetActive(true);
    }
    public void NoExit()
    {
        AreYouSureExitUI.SetActive(false);
    }
    public void Mainmanu()
    {
        AreYouSureMainmanuUI.SetActive(true);
    }
    public void NoMainmanu()
    {
        AreYouSureMainmanuUI.SetActive(false);
    }
    public void openshop()
    {
        shop.SetActive(true);
    }
    public void closeshop()
    {
        shop.SetActive(false);

    }
}
