using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ClearScreen; //クリア画面
    public int AllClear; //全てのステージをクリアしたか
    public int i = 0;
    
    private void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        i = PlayerPrefs.GetInt("Clear?");
    }

    private void Update()
    {
        AllClear = PlayerPrefs.GetInt("AllClear");
        if(AllClear == 1 && i == 0)
        {
            SoundManager.Instance.StopBGM(BGMSoundData.BGM.Title);
            ClearScreen.SetActive(true);
            SoundManager.Instance.PlaySE(SESoundData.SE.AllClear);
            i++;
            PlayerPrefs.SetInt("Clear?", i);
            
        }
    }

    //ゲームのプレイボタン
    public void PlayGame()
    {
        //音を再生
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //次のシーンに移動
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //クリア画面を閉じるボタン
    public void CloseClearButton()
    {
        //画面を閉じる
        ClearScreen.SetActive(false);
        //音を再生
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    //ボタンの音を再生
    public void ButtonSound()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    //タイトル画面の音楽
    public void PlayMenuBGM()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
    }

    //音楽を止める
    public void StopMenuBGM()
    {
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Title);
    }
}
