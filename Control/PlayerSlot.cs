using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSlot : MonoBehaviour
{
    public Image image;
    public Text NameText;
    public GameObject PlayerPrefab;
    public PlayerStatus PlayerStatus;

    public static PlayerSlot instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }
    public void SetPlayer(PlayerStatus playerStatus)
    {
        PlayerStatus = playerStatus;
        PlayerPrefab = PlayerStatus.gameObject;
        image.sprite = PlayerStatus.monsterSprite;
        image.enabled = true;
        NameText.text = PlayerStatus.monsterName;
        NameText.enabled = true;
        UpdateNameText();
    }
    public void UpdateNameText()
    {
        NameText.text = PlayerStatus.monsterName.ToString();
    }
    public void Use()
    {
        SpawnPlayer.instance.playerPrefab = PlayerPrefab;
    }
}
