using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviour
{
    public GameObject AreYouSureUI;
    public GameObject str;
    public GameObject agi;
    public GameObject dex;
    public GameObject spawn;

    public void StrSelect()
    {
        spawn = str;
        AreYouSureUI.SetActive(true);
    }
    public void AgiSelect()
    {
        spawn = agi;
        AreYouSureUI.SetActive(true);
    }
    public void DexSelect()
    {
        spawn = dex;
        AreYouSureUI.SetActive(true);
    }
    public void Yes()
    {
        SendPlayer.instance.player = spawn;
        SceneManager.LoadScene("Monster TamTam Game");
    }
    public void No()
    {
        AreYouSureUI.SetActive(false);
    }
    
}
