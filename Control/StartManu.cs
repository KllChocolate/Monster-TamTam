using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManu : MonoBehaviour
{
 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Monster TamTam LoadScene");
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
