using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChooseStage : MonoBehaviour
{
    private GameObject[] StageList;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HighScoreText2;
    public TextMeshProUGUI HighScoreText3;
    public TextMeshProUGUI HighScoreText4;
    public TextMeshProUGUI HighScoreText5;
    public TextMeshProUGUI HighScoreText6;
    public TextMeshProUGUI HighScoreText7;
    public GameObject startButton;
    public GameObject lockImage;
    public GameObject startButton2;
    public GameObject lockImage2;
    public GameObject startButton3;
    public GameObject lockImage3;
    public GameObject startButton4;
    public GameObject lockImage4;
    public GameObject startButton5;
    public GameObject lockImage5;
    public GameObject startButton6;
    public GameObject lockImage6;
    static public int index = 0;
    private int stageclear = 0;
    public int highscore2;
    public int highscore3;
    public int highscore4;
    public int highscore5;
    public int highscore6;
    public int highscore7;

    // Start is called before the first frame update
    void Start()
    {
        //ハイスコアの情報を呼び出し
        GameManager.highscore = PlayerPrefs.GetInt("HighScore");
        highscore2 = PlayerPrefs.GetInt("HighScore2");
        highscore3 = PlayerPrefs.GetInt("HighScore3");
        highscore4 = PlayerPrefs.GetInt("HighScore4");
        highscore5 = PlayerPrefs.GetInt("HighScore5");
        highscore6 = PlayerPrefs.GetInt("HighScore6");
        highscore7 = PlayerPrefs.GetInt("HighScore7");

        //ステージの情報を呼び出し
        index = PlayerPrefs.GetInt("Stage");

        //クリア情報を呼び出し
        stageclear = PlayerPrefs.GetInt("StageClear");

        StageList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            StageList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in StageList)
        {
            go.SetActive(false);
        }

        if (StageList[index])
        {
            StageList[index].SetActive(true);
        }

        PlayerPrefs.SetInt("Stage", index);
    }

    // Update is called once per frame
    void Update()
    {
        //ハイスコアテキストを表示
        HighScoreText.text = GameManager.highscore.ToString();
        HighScoreText2.text = highscore2.ToString();
        HighScoreText3.text = highscore3.ToString();
        HighScoreText4.text = highscore4.ToString();
        HighScoreText5.text = highscore5.ToString();
        HighScoreText6.text = highscore6.ToString();
        HighScoreText7.text = highscore7.ToString();

        //クリア状況に応じて、プレイできるステージを表示
        if (stageclear >= 1)
        {
            startButton.SetActive(true);
            lockImage.SetActive(false);
        }
        if (stageclear >= 2)
        {
            startButton2.SetActive(true);
            lockImage2.SetActive(false);
        }
        if (stageclear >= 3)
        {
            startButton3.SetActive(true);
            lockImage3.SetActive(false);
        }
        if (stageclear >= 4)
        {
            startButton4.SetActive(true);
            lockImage4.SetActive(false);
        }
        if (stageclear >= 5)
        {
            startButton5.SetActive(true);
            lockImage5.SetActive(false);
        }
        if (stageclear >= 6)
        {
            startButton6.SetActive(true);
            lockImage6.SetActive(false);
        }

        PlayerPrefs.SetInt("Stage", index);

    }

    //左ボタンを押して、手前のステージに変更
    public void ToggleLeft()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        StageList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = StageList.Length - 1;
        }

        StageList[index].SetActive(true);
    }

    //右ボタンを押して、次のステージに変更
    public void ToggleRight()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        StageList[index].SetActive(false);

        index++;
        if (index == StageList.Length)
        {
            index = 0;
        }

        StageList[index].SetActive(true);
    }

    //メニュー画面に戻る
    public void BackButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
