using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSlotsManu : MonoBehaviour
{
    private LoadSlot[] loadSlots;
    private void Awake()
    {
        loadSlots = GetComponentsInChildren<LoadSlot>();
    }
    private void Start()
    {
        ActivateMenu();
    }
    public void ActivateMenu()
    {
        foreach (LoadSlot loadSlot in loadSlots)
        {
            string profileId = DataPersistenceManager.instance.GetSelectedProfileId();
            if (loadSlot.GetProfileId() == profileId)
            {
                loadSlot.SetProfileId(profileId);
                break;
            }
        }
    }

    public void LoadSaveGame()
    {
        DataPersistenceManager.instance.LoadGame();
    }
}
