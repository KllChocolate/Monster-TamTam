using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public InputField InputField;

    [Header("Profile")]
    [SerializeField] private string profileId = "";

    public void Start()
    {
        InputField = GetComponent<InputField>();
    }

    public void UpdateSaveName()
    {
        profileId = InputField.text.ToString();
    }

    public string GettProfileId()
    { 
        return profileId; 
    }
}
