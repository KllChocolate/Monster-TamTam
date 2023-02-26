using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIThree : MonoBehaviour
{
    public Transform playerParent;
    public PlayerSlotThree playerSlotPrefabThree;
    public Toggle[] toggleButtons;


    private void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerObjects.Length; i++)
        {
            PlayerStatus playerStatus = playerObjects[i].GetComponent<PlayerStatus>();
            if (playerStatus == null)
            {
                continue;
            }

            PlayerSlotThree playerSlotThree = Instantiate(playerSlotPrefabThree, playerParent);
            playerSlotThree.SetPlayer(playerStatus);

        }
    }

}
