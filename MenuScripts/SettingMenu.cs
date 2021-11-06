using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    //音のボリューム設定
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

}
