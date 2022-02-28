using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public int inter = 0; //インタースティシャルを表示しているか
    public int current; //現在のステージ
    public int stageclear; //どのステージまでクリアしているか
    public int score; //プレイヤーのスコア
    public int coin; //現在のステージで獲得したコイン
    public int distance; //走行距離
    public int Ranking; //ランキング機能を利用するか
    static public int highscore = 0; //ハイスコア
    static public int coins = 0; //コインの累計
    public bool bestscore = false;
    public bool GameIsFinished = false; //ゲームが終わったか確認
    public bool isCalledOnece = false; //1回だけ実行するため
    public GameObject player;
    public GameObject HighScoreText;
    public GameObject ScoreText;
    public GameObject HighScoreText2;
    public GameObject ScoreText2;
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
    public GameObject SpaceShip;
    public GameObject gauge;
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

        //UIの制御
        GameOverUI.SetActive(true);
        OverCamera.SetActive(true);
        Defeat.SetActive(true);
        ShieldButton.SetActive(false);
        joystick.SetActive(false);
        pauseButton.SetActive(false);

        //オブジェクト消去
        if (Shield.activeSelf)
            Shield.SetActive(false);
        if (gauge.activeSelf)
            gauge.SetActive(false);
        SpaceShip.SetActive(false);

        //自分の最高スコアを更新した場合、ハイスコアと表示
        if (bestscore)
        {
            HighScoreText.SetActive(true);
            ScoreText.SetActive(false);
        }

        //音楽の制御
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Main);
        SoundManager.Instance.PlaySE(SESoundData.SE.GameOver);
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.GameOverTheme);

        //ゲームが終了
        GameIsFinished = true;
    }

    //ゲームクリア
    public void GameClear()
    {
        //今回手に入れたコインを保存
        coins += coin;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();

        //ステージのロック解除
        if(current == 2)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 1)
                PlayerPrefs.SetInt("StageClear", 1);
        }
        else if(current == 3)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 2)
                PlayerPrefs.SetInt("StageClear", 2);
        }
        else if(current == 4)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 3)
                PlayerPrefs.SetInt("StageClear", 3);
        }
        else if (current == 5)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 4)
                PlayerPrefs.SetInt("StageClear", 4);
        }
        else if (current == 6)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 5)
                PlayerPrefs.SetInt("StageClear", 5);
        }
        else if (current == 7)
        {
            //すでに先のステージをクリアしている場合上書きしないようにする
            if (stageclear < 6)
                PlayerPrefs.SetInt("StageClear", 6);
        }
        else if(current == 8)
        {
            PlayerPrefs.SetInt("AllClear", 1);
        }

        //UIの制御
        GameClearUI.SetActive(true);
        ClearCamera.SetActive(true);
        Victory.SetActive(true);
        ShieldButton.SetActive(false);
        joystick.SetActive(false);
        pauseButton.SetActive(false);

        //オブジェクトの消去
        if (Shield.activeSelf)
            Shield.SetActive(false);
        if (gauge.activeSelf)
            gauge.SetActive(false);
        SpaceShip.SetActive(false);

        if (score > highscore)
        {
            HighScoreText2.SetActive(true);
            ScoreText2.SetActive(false);
        }

        //音楽の制御
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Main);
        SoundManager.Instance.PlaySE(SESoundData.SE.GameClear);
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.GameClearTheme);

        //ゲームが終了
        GameIsFinished = true;
    }

    //もう1度プレイする
    public void Restart()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //メニュー画面に遷移
    public void Menu()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        SceneManager.LoadScene("Menu");
    }

    //次のステージに遷移
    public void NextStage()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //1回目ならインタースティシャル広告を流す
        if (inter == 0)
        {
            GoogleAds.Instance.OnClickShowInterstitialButton();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        inter++;
        PlayerPrefs.SetInt("Interstitial", inter);
    }

    // Start is called before the first frame update
    void Start()
    {
        //ランキングを利用するか
        Ranking = PlayerPrefs.GetInt("Ranking");

        //インタスティシャルを表示しているか
        inter = PlayerPrefs.GetInt("Interstitial");
        //現在のステージ
        current = SceneManager.GetActiveScene().buildIndex;

        //どのステージをクリアしているか
        stageclear = PlayerPrefs.GetInt("StageClear");

        //累積されたコインの数を呼び出し
        coins = PlayerPrefs.GetInt("Coins");

        //前回までのステージごとのHighScoreを呼び出し
        if (current == 2)
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
        else if (current == 3)
        {
            highscore = PlayerPrefs.GetInt("HighScore2");
        }
        else if (current == 4)
        {
            highscore = PlayerPrefs.GetInt("HighScore3");
        }
        else if (current == 5)
        {
            highscore = PlayerPrefs.GetInt("HighScore4");
        }
        else if (current == 6)
        {
            highscore = PlayerPrefs.GetInt("HighScore5");
        }
        else if (current == 7)
        {
            highscore = PlayerPrefs.GetInt("HighScore6");
        }
        else if (current == 8)
        {
            highscore = PlayerPrefs.GetInt("HighScore7");
        }

        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Title);
        SoundManager.Instance.PlaySE(SESoundData.SE.CountDown);
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの計算
        score = coin * distance + distance;

        //ハイスコアの更新
        if(highscore < score)
        {
            bestscore = true;
            highscore = score;
        }

        if (GameIsFinished)
        {
            if (current == 2)
            {
                PlayerPrefs.SetInt("HighScore", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 3)
            {
                PlayerPrefs.SetInt("HighScore2", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 4)
            {
                PlayerPrefs.SetInt("HighScore3", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 5)
            {
                PlayerPrefs.SetInt("HighScore4", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 6)
            {
                PlayerPrefs.SetInt("HighScore5", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 7)
            {
                PlayerPrefs.SetInt("HighScore6", highscore);
                PlayerPrefs.Save();
            }
            else if (current == 8)
            {
                PlayerPrefs.SetInt("HighScore7", highscore);
                PlayerPrefs.Save();
            }
        }

        //スコアの値を表示
        OverScoreText.text = score.ToString();
        ClearScoreText.text = score.ToString();

        //ゲームが終了したらスコア送信
        if (GameIsFinished)
        {
            if (!isCalledOnece)
            {
                isCalledOnece = true;
                if (Ranking == 1)
                {
                    SubmitScore(score);
                }
            }
        }
    }

    //ステージごとにスコア送信
    void SubmitScore(int playerScore)
    {
        if (current == 2)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage1",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }

        if(current == 3)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage2",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }

        if (current == 4)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage3",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }

        if (current == 5)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage4",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }

        if (current == 6)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage5",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }
        if (current == 7)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage6",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }
        if (current == 8)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage7",
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }
    }   
}
