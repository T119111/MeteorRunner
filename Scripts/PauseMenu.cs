using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (GameIsPaused)
        {
            //ゲーム停止
            Pause();
        }else
        {
            //再開する
            Resume();
        }
    }

    //プレイヤーが停止ボタンを押した時フラグを立てる
    public void PauseButton()
    {
        GameIsPaused = true;
    }

    //再開ボタンを押したらフラグを下ろす
    public void ResumeButton()
    {
        GameIsPaused = false;
    }

    //メニューに戻る
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
        GameIsPaused = false;
    }

    //再開
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    //停止
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
