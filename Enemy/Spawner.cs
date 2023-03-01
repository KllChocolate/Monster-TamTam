using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("enemy")]
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public Transform enemySpawnPoint;
    public Transform enemySpawnPoint2;
    public Transform enemySpawnPoint3;


    void Start()
    {
        Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
        Instantiate(enemy2, enemySpawnPoint2.position, Quaternion.identity);
        Instantiate(enemy3, enemySpawnPoint3.position, Quaternion.identity);
    }


}

