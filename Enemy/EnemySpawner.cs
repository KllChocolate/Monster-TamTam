using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    void Start()
    {
        Instantiate(enemy1, spawnPoint1.position, Quaternion.identity);
        Instantiate(enemy2, spawnPoint2.position, Quaternion.identity);
        Instantiate(enemy3, spawnPoint3.position, Quaternion.identity);
    }


}

