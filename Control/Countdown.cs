using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int countFrom = 120;
    private int countDownValue;
    public Text textBox;
    public GameObject timeout;

    private void Start()
    {
        countDownValue = countFrom;
        UpdateText();
        InvokeRepeating("CountDown", 1, 1);
        Time.timeScale= 1;
    }

    private void CountDown()
    {
        if (countDownValue > 0)
        {
            countDownValue--;
            UpdateText();
        }
        else
        {
            CancelInvoke();
            timeout.SetActive(true);
            StartCoroutine(backSecnes());
            Time.timeScale = 0;
        }
    }

    private void UpdateText()
    {
        textBox.text = countDownValue.ToString();
    }

    public IEnumerator backSecnes()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Monster TamTam Game");
    }
}
