using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [Header("Monster Selection")]
    [SerializeField] private GameObject monsterSelection;
    private MonsterSelectionData monsterSelectionData;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.Log("????ҡ???? 1 Data Persistance Manager 㹩ҡ");
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

    private void Start()
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        monsterSelection = GameObject.Find("Monster Selection");
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if (gameData == null)
        {
            Debug.Log("????վ?????????? ");
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

        dataHandler.Save(gameData);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
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
    /*public void LoadData(GameData data)
    {
        if (data is GameData gameData)
        {
            if (gameData.monsterSelectionData != null)
            {
                monsterSelectionData = gameData.monsterSelectionData;
                if (monsterSelection != null)
                {
                    monsterSelection.SetActive(monsterSelectionData.isMonsterSelectionActive);
                    monsterSelection.transform.position = monsterSelectionData.monsterSelectionPosition;
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (monsterSelection != null)
        {
            monsterSelectionData.isMonsterSelectionActive = monsterSelection.activeSelf;
            monsterSelectionData.monsterSelectionPosition = monsterSelection.transform.position;
            data.monsterSelectionData = monsterSelectionData;
        }
    }*/
}
