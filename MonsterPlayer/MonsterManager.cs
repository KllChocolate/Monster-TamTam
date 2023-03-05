using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == LayerMask.NameToLayer("Incubator"))
        {
            GetComponent<PlayerStatus>().enabled = true;
            GetComponent<DragObject>().enabled = true;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerWildWalk>().enabled = false;
            GetComponentInChildren<TextMeshPro>().enabled = true;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Arena"))
        {
            GetComponent<PlayerStatus>().enabled = false;
            GetComponent<DragObject>().enabled = false;
            GetComponent<PlayerWildWalk>().enabled = false;
            if (PlayerAttack.instance.death == false)
            {
                GetComponent<PlayerAttack>().enabled = true;
            }
            if (PlayerAttack.instance.death == true)
            {
                GetComponent<PlayerAttack>().enabled = false;
            }
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wild"))
        {
            GetComponent<PlayerStatus>().enabled = false;
            GetComponent<DragObject>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerWildWalk>().enabled = true;
            GetComponentInChildren<TextMeshPro>().enabled = false;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Net"))
        {
            GetComponent<DragObject>().enabled = true;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Nick"))
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
