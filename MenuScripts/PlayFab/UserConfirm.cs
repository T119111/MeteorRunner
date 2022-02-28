using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserConfirm : MonoBehaviour
{
    public void AgreeButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        PlayerPrefs.SetInt("Ranking", 1);
        this.gameObject.SetActive(false);
    }

    public void NotAgreeButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        PlayerPrefs.SetInt("Ranking", 0);
        this.gameObject.SetActive(false);
    }
}
