using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSlotThree : MonoBehaviour
{
    public Image image;
    public Text NameText;
    public GameObject PlayerPrefab;
    public PlayerStatus PlayerStatus;
    private Toggle toggle;

    public static PlayerSlotThree instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        toggle= GetComponent<Toggle>();
    }
    private void Update()
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
    public void add()
    {
        SpawnPlayerThree.instance.add(PlayerPrefab);
    }

    public void remove()
    {
       SpawnPlayerThree.instance.remove(Array.IndexOf(SpawnPlayerThree.instance.Player, PlayerPrefab));
       
    }
    public void OnToggleValueChanged()
    {
        if(toggle.isOn) 
        { 
            add(); 
        }
        else { remove(); }
        if(SpawnPlayerThree.instance.canAdd == false)
        {
            toggle.isOn = false;
        }
    }

}


