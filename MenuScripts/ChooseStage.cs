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
    public GameObject startButton;
    public GameObject lockImage;
    public GameObject startButton2;
    public GameObject lockImage2;
    public GameObject startButton3;
    public GameObject lockImage3;
    static public int index = 0;
    private int stageclear = 0;
    public int highscore2;
    public int highscore3;
    public int highscore4;

    // Start is called before the first frame update
    void Start()
    {
        //ハイスコアの情報を呼び出し
        GameManager.highscore = PlayerPrefs.GetInt("HighScore");
        highscore2 = PlayerPrefs.GetInt("HighScore2");
        highscore3 = PlayerPrefs.GetInt("HighScore3");
        highscore4 = PlayerPrefs.GetInt("HighScore4");
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

        PlayerPrefs.SetInt("Stage", index);

    }

    //左ボタンを押して、手前のステージに変更
    public void ToggleLeft()
    {
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
        StageList[index].SetActive(false);

        index++;
        if (index == StageList.Length)
        {
            index = 0;
        }

        StageList[index].SetActive(true);
    }

    //指定したステージに移動
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 + index);
    }

    //メニュー画面に戻る
    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
