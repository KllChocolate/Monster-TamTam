using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class LoadSlot : MonoBehaviour
{
    public Toggle toggle;
    public Text text;
    public static LoadSlot instance;

    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [SerializeField] private GameData profileData;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        LoadProfileData();
        text = GetComponentInChildren<Text>();
        text.text = profileId.ToString();
    }

    public void LoadProfileData()
    {
        profileData = DataPersistenceManager.instance.GetGameData();
    }

    public string GetProfileId()
    {
        return profileId;
    }
    public void SetProfileId(string id)
    {
        profileId = id;
    }
    public void ConfirmLoad()
    {
        if (toggle.isOn)
        {
            DataPersistenceManager.instance.Load(GetProfileId());    
        }
     
    }
}
