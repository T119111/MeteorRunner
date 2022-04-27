using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public int Ranking;
    public AudioMixer BGMMixer;
    public AudioMixer SEMixer;
    public TextMeshProUGUI UserName;
    public GameObject UseRanking;
    public GameObject NotUseRanking;

    private void Start()
    {
        UserName.text = PlayerPrefs.GetString("UserName");
    }

    private void Update()
    {
        Ranking = PlayerPrefs.GetInt("Ranking");

        if(Ranking == 1)
        {
            UseRanking.SetActive(true);
            NotUseRanking.SetActive(false);
        }
        else if(Ranking == 0)
        {
            UseRanking.SetActive(false);
            NotUseRanking.SetActive(true);
        }
    }

    //音のボリューム設定(BGM)
    public void BGMSetVolume(float volume)
    {
        BGMMixer.SetFloat("volume", volume);
    }

    //音のボリューム設定(SE)
    public void SESetVolume(float volume)
    {
        SEMixer.SetFloat("volume", volume);
    }
}
