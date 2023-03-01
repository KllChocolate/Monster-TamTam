using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsManu : MonoBehaviour
{
    private SaveSlot[] saveSlots;
    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }
    public void SelectSave(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GettProfileId());
    }
    private void Start()
    {
        ActivateMenu();
    }
    public void ActivateMenu()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GettProfileId(), out profileData);
            //saveSlot.SetData(profileData);
        }
    }
    public void ConfirmSave()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}
