using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public int stage;
    public int stageclear;
    public int score;
    static public int highscore = 0;
    public int coin;
    static public int coins = 0;
    public int distance;

    public static GameManager inst;

    public GameObject player;
    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public GameObject ClearCamera;
    public GameObject OverCamera;
    public GameObject Victory;
    public GameObject Defeat;
    public GameObject ShieldButton;
    public GameObject Shield;
    public GameObject joystick;
    public GameObject pauseButton;


    [SerializeField] public GameObject character;
    [SerializeField] Text CoinText;
    [SerializeField] Text DistanceText;
    [SerializeField] TextMeshProUGUI OverScoreText;
    [SerializeField] TextMeshProUGUI ClearScoreText;


    [SerializeField] PlayerMovement playerMovement;

    public void Awake()
    {
        instance = this;
    }

    //コインの表示
    public void IncrementScore()
    {
        coin++;
        CoinText.text = coin.ToString();
        playerMovement.speed += playerMovement.speedIncresePoint;
    }

    //走行距離の表示
    public void IncrementDistance()
    {
        distance = (int)player.transform.position.z;
        DistanceText.text = distance + "m";
    }
    
    //ゲームオーバー
    public void GameOver()
    {
        //今回手に入れたコインを保存
        coins += coin;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();

        Debug.Log("ゲームオーバー");

        //UIの制御
        GameOverUI.SetActive(true);
        OverCamera.SetActive(true);
        Defeat.SetActive(true);
        ShieldButton.SetActive(false);
        Shield.SetActive(false);
        joystick.SetActive(false);
        pauseButton.SetActive(false);

        //音楽の制御
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
        FindObjectOfType<AudioManager>().PlaySound("GameOverTheme");
        
    }

    //ゲームクリア
    public void GameClear()
    {
        //今回手に入れたコインを保存
        coins += coin;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();

        Debug.Log("ゲームクリア");

        //ステージのロック解除
        if(stage == 0)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 1)
                PlayerPrefs.SetInt("StageClear", 1);
        }
        else if(stage == 1)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 2)
                PlayerPrefs.SetInt("StageClear", 2);
        }else
        {
            PlayerPrefs.SetInt("StageClear", 3);
        }

        //UIの制御
        GameClearUI.SetActive(true);
        ClearCamera.SetActive(true);
        Victory.SetActive(true);
        ShieldButton.SetActive(false);
        Shield.SetActive(false);
        joystick.SetActive(false);
        pauseButton.SetActive(false);

        //音楽の制御
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        FindObjectOfType<AudioManager>().PlaySound("GameClear");
        FindObjectOfType<AudioManager>().PlaySound("GameClearTheme");

    }

    //もう1度プレイする
    public void Restart()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //メニュー画面に推移
    public void Menu()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        //ステージセレクト画面で選択されたステージを呼び出し
        stage = PlayerPrefs.GetInt("Stage");
        Debug.Log("stage:" + stage);

        //どのステージをクリアしているか
        stageclear = PlayerPrefs.GetInt("StageClear");

        //累積されたコインの数を呼び出し
        coins = PlayerPrefs.GetInt("Coins");

        //前回までのステージごとのHighScoreを呼び出し
        if (stage == 0)
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
        else if (stage == 1)
        {
            highscore = PlayerPrefs.GetInt("HighScore2");
        }
        else if (stage == 2)
        {
            highscore = PlayerPrefs.GetInt("HighScore3");
        }
        else
        {
            highscore = PlayerPrefs.GetInt("HighScore4");
        }
        FindObjectOfType<AudioManager>().PlaySound("CountDown");
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの計算
        score = coin * distance + distance;

        //ハイスコアの更新
        if(highscore < score)
        {
            highscore = score;
        }

        if(stage == 0)
        {
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
        }
        else if(stage == 1)
        {
            PlayerPrefs.SetInt("HighScore2", highscore);
            PlayerPrefs.Save();
        }
        else if (stage == 2)
        {
            PlayerPrefs.SetInt("HighScore3", highscore);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("HighScore4", highscore);
            PlayerPrefs.Save();
        }

        //スコアの値を表示
        OverScoreText.text = score.ToString();
        ClearScoreText.text = score.ToString();
    }
}
