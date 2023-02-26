using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public string nameScene;
    public int energy;

    public static SpawnPlayer instance;
    private void Awake()
    {
        instance= this;
    }


    public void Easy()
    {
        nameScene = "Monster TamTam Arena OneEasy";
        energy = 10;
    }

    public void Medium()
    {
        nameScene = "Monster TamTam Arena OneMedium";
        energy = 20;
    }

    public void Hard()
    {
        nameScene = "Monster TamTam Arena OneHard";
        energy = 30;
    }

    public void Use()
    {
        SceneManager.LoadScene(nameScene);
        DontDestroyOnLoad(playerPrefab.gameObject);
        EnergyTotal.instance.energy -= energy;
    }
}
