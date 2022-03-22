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
    public string stage; //cuurentをstring型へ変換するため
    public int stageclear; //どのステージまでクリアしているか
    public int score; //プレイヤーのスコア
    public int coin; //現在のステージで獲得したコイン
    public int distance; //走行距離
    public int Ranking; //ランキング機能を利用するか
    static public int highscore = 0; //ハイスコア
    static public int coins = 0; //コインの累計
    public bool bestscore = false; //今までの最高スコアだった場合UIに表示するため
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

    // Start is called before the first frame update
    void Start()
    {
        //ランキングを利用するか
        Ranking = PlayerPrefs.GetInt("Ranking");

        //インタスティシャルを表示しているか
        inter = PlayerPrefs.GetInt("Interstitial");

        //現在のステージ（ビルドシーンの番号-1）
        current = SceneManager.GetActiveScene().buildIndex - 1; //current = 1 (stage1)

        stage = current.ToString(); //string型に変換

        //どのステージをクリアしているか
        stageclear = PlayerPrefs.GetInt("StageClear");

        //累積されたコインの数を呼び出し
        coins = PlayerPrefs.GetInt("Coins");

        //ステージごとのハイスコアを呼び出し
        highscore = PlayerPrefs.GetInt("HighScore" + stage);

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

        //スコアの値を表示
        OverScoreText.text = score.ToString();
        ClearScoreText.text = score.ToString();

        //ゲームが終了したらスコア送信
        if (GameIsFinished)
        {
            if (!isCalledOnece)
            {
                isCalledOnece = true;
                PlayerPrefs.SetInt("HighScore" + stage, highscore);　//ハイスコアを保存
                PlayerPrefs.SetInt("UseShield", 0);　//シールドの使用を初期化
                if (Ranking == 1) //ランキングを使用する場合
                    SubmitScore(score); //スコアを送信
            }
        }
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

        //まだクリアしていない場合、ステージのロック解除
        if (stageclear < current)
            PlayerPrefs.SetInt("StageClear", current);

        if (current == 7)
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
            GoogleAds.Instance.OnClickShowInterstitialButton();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        inter++;
        PlayerPrefs.SetInt("Interstitial", inter); //1回流した
    }

    //ステージごとにスコア送信
    void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {

                    new StatisticUpdate
                    {
                        StatisticName = "HighScoreStage" + stage,
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("HighScoreStage" + stage + "スコア送信");
                GameIsFinished = false;
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }   
}
