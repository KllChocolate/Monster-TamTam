using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
//using UnityEditorInternal;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    //public string selectedProfileId = "";
    public static DataPersistenceManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("เจอมากกว่า 1 Data Persistance Manager ในฉาก");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene) 
    {
        SaveGame();
    }
    public void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName, useEncryption);
        dataPersistenceObjects= FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();//(selectedProfileId);
        if(gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if (gameData == null)
        {
            Debug.Log("ไม่มีพบข้อมูลเกม ");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame() 
    {
        if (gameData == null)
        {
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);//, selectedProfileId);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    { 
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
    /*public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    public GameData GetGameData()
    {
        return gameData;
    }
    public string GetSelectedProfileId()
    {
        return selectedProfileId;
    }*/

}
