using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPlayer : MonoBehaviour
{
    public static SendPlayer instance;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
