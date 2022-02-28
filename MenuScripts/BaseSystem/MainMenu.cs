using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ClearScreen;
    public int AllClear;
    public int i = 0;
    
    private void Start()
    {
        //FindObjectOfType<AudioManager>().PlaySound("MenuTheme");
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        i = PlayerPrefs.GetInt("Clear?");
    }

    private void Update()
    {
        AllClear = PlayerPrefs.GetInt("AllClear");
        if(AllClear == 1 && i == 0)
        {
            //FindObjectOfType<AudioManager>().StopSound("MenuTheme");
            SoundManager.Instance.StopBGM(BGMSoundData.BGM.Title);
            ClearScreen.SetActive(true);
            //FindObjectOfType<AudioManager>().PlaySound("AllClear");
            SoundManager.Instance.PlaySE(SESoundData.SE.AllClear);
            i++;
            PlayerPrefs.SetInt("Clear?", i);
            
        }
    }

    public void PlayGame()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseClearButton()
    {
        ClearScreen.SetActive(false);
        //FindObjectOfType<AudioManager>().PlaySound("MenuTheme");
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    public void ButtonSound()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    public void PlayMenuBGM()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
    }

    public void StopMenuBGM()
    {
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Title);
    }
}
