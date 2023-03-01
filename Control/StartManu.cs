using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManu : MonoBehaviour
{
    [SerializeField] private PassData money;
    [SerializeField] private PassData energy;
    [SerializeField] private GameObject continueGameButton;
    [SerializeField] private GameObject mainManu;
    [SerializeField] private GameObject loadGame;

    private void Start()
    { 
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.SetActive(false);
        }
        if (DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.SetActive(true);
        }

    }
    public void PlayGame()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        money.Value = 10000;
        energy.Value = 100;
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("Monster TamTam Game");
    }
    public void LoadGame()
    {
        loadGame.SetActive(true);
        mainManu.SetActive(false);
    }
    public void back()
    {
        loadGame.SetActive(false);
        mainManu.SetActive(true);
    }
    public void Option()
    {
        SceneManager.LoadScene("Monster TamTam OptionScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
