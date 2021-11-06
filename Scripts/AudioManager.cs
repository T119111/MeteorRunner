using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            //s.source.mixer = s.mixer;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //PlaySound("Countdown");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Sound s in sounds)
        {
            //停止ボタンが押されたら音楽を停止
            if (PauseMenu.GameIsPaused)
                s.source.volume = 0f;
            else
                s.source.volume = s.volume;
        }
        
    }

    //音を再生
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }

        s.source.Play();

    }

    //音を停止
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.Stop();

    }

    //ボタンの音
    public void Button()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
    }

}
