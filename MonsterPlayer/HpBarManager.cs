using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpBarManager : MonoBehaviour
{
    public Transform playerParent;
    public PlayerBar playerBarPrefab;

    private void Awake()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerStatus playerStatus = playerObject.GetComponent<PlayerStatus>();
            if (playerStatus == null)
            {
                continue;
            }

            PlayerBar playerBar = Instantiate(playerBarPrefab, playerParent);
            playerBar.SetPlayer(playerStatus, playerObject);
        }
    }
    
}

