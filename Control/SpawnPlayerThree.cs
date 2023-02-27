using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerThree : MonoBehaviour
{
    public string nameScene;
    public GameObject[] Player;
    public int energy;
    public PassData _energy;
    public bool canAdd;

    public static SpawnPlayerThree instance;

    public void Awake()
    {
        instance = this;
    }

    public void Easy()
    {
        nameScene = "Monster TamTam Arena ThreeEasy";
        energy = 10;
    }

    public void Medium()
    {
        nameScene = "Monster TamTam Arena ThreeMedium";
        energy = 20;
    }

    public void Hard()
    {
        nameScene = "Monster TamTam Arena ThreeHard";
        energy = 30;
    }

    public void Use()
    {
        SceneManager.LoadScene(nameScene);
        DontDestroyOnLoad(Player[0].gameObject);
        DontDestroyOnLoad(Player[1].gameObject);
        DontDestroyOnLoad(Player[2].gameObject);
        _energy.Value -= energy;


    }
    public void add(GameObject PlayerPrefab)
    {
        if (Player.Length < 3)
        {
            Array.Resize(ref Player, Player.Length + 1);
            Player[Player.Length - 1] = PlayerPrefab;
            canAdd = true;
        }
        else
        {
            Debug.Log("Cannot add more than 3 players.");
            canAdd = false;
        }
    }
    public void remove(int index)
    {
        if (index >= 0 && index < Player.Length)
        {
            for (int i = index; i < Player.Length - 1; i++)
            {
                Player[i] = Player[i + 1];
            }
            Array.Resize(ref Player, Player.Length - 1);
        }
    }
}
