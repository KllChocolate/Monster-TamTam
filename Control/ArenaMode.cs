using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMode : MonoBehaviour
{
    public GameObject oneVsOneUI;
    public GameObject threeVsThreeUI;
    public GameObject SelectPlayer;
    public GameObject SelectPlayers;


    public void onevsone()
    {
        oneVsOneUI.SetActive(true);
    }
    public void closeonevsone()
    {
        oneVsOneUI.SetActive(false);
    }
    public void threevsthree()
    {
        threeVsThreeUI.SetActive(true);
    }
    public void closethreevsthree()
    {
        threeVsThreeUI.SetActive(false);
    }
    public void selectPlayer()
    {
        SelectPlayer.SetActive(true);
    }
    public void closeSelectPlayer()
    {
        SelectPlayer.SetActive(false);
    }
    public void selectPlayers()
    {
        SelectPlayers.SetActive(true);
    }
    public void closeselectPlayers()
    {
        SelectPlayers.SetActive(false);
    }
}
