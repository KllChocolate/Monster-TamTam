using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Image image;
    public GameObject Player;
    public PlayerStatus PlayerStatus;
    public HpBar hpBar;
    public MpBar mpBar;

    public static PlayerBar instance;
    private void Awake()
    {
        instance = this;
    }

    public void SetPlayer(PlayerStatus playerStatus, GameObject playerObject)
    {
        PlayerStatus = playerStatus;
        image.sprite = PlayerStatus.monsterSprite;
        Player = playerObject;
        hpBar.player = Player;
        mpBar.player = Player;
    }

}
