using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime = 3;
    public Text countdownDisplay;
    public GameObject joystick;

    void Start()
    {
        StartCoroutine(CountdownToStart());
        //FindObjectOfType<AudioManager>().PlaySound("CountDown");
    }

    IEnumerator CountdownToStart()
    {
        //3秒カウントダウン
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(0.99f);
            countdownTime--;
        }
        countdownDisplay.text = "GO!";
        joystick.SetActive(true);
        //FindObjectOfType<AudioManager>().PlaySound("MainTheme");
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Main);
        yield return new WaitForSeconds(0.99f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
