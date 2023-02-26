using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public Transform SpawnPlayer1;
    public Transform SpawnPlayer2;
    public Transform SpawnPlayer3;

    public void Start()
    {
        OnSceneLoaded();
    }
    void OnSceneLoaded()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (i == 0)
                players[i].transform.position = SpawnPlayer1.position;
            else if (i == 1)
                players[i].transform.position = SpawnPlayer2.position;
            else if (i == 2)
                players[i].transform.position = SpawnPlayer3.position;
            else
                Destroy(players[i]);

            players[i].transform.rotation = Quaternion.identity;
            players[i].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
