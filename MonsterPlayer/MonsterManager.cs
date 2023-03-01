using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == LayerMask.NameToLayer("Incubator"))
        {
            GetComponent<PlayerStatus>().enabled = true;
            GetComponent<DragObject>().enabled = true;
            GetComponent<PlayerAttack>().enabled = false;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Arena"))
        {
            GetComponent<PlayerStatus>().enabled = false;
            GetComponent<DragObject>().enabled = false;
            if (PlayerAttack.instance.death == false)
            {
                GetComponent<PlayerAttack>().enabled = true;
            }
            if (PlayerAttack.instance.death == true)
            {
                GetComponent<PlayerAttack>().enabled = false;
            }
        }
    }
}
