using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Transform playerParent;
    public PlayerSlot playerSlotPrefab;

    private void Start()
    {

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerStatus playerStatus = playerObject.GetComponent<PlayerStatus>();
            if (playerStatus == null)
            {
                continue;
            }

            PlayerSlot playerSlot = Instantiate(playerSlotPrefab, playerParent);
            playerSlot.SetPlayer(playerStatus);
        }
    }
}
