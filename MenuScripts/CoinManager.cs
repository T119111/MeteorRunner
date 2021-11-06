using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coin;
    public int Lock1;
    public int Lock2;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI HighScoreText;
    public GameObject notenoughText1;
    public GameObject unlockText1;
    public GameObject lockImage1;
    public GameObject unlockButton1;
    public GameObject applyButton1;
    public GameObject notenoughText2;
    public GameObject unlockText2;
    public GameObject lockImage2;
    public GameObject unlockButton2;
    public GameObject applyButton2;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameManager.coins);
        //PlayerPrefs.SetInt("Coins", coin);
        //今まで手に入れたコイン情報を取得
        coin = PlayerPrefs.GetInt("Coins");
        //ハイスコア情報を取得
        GameManager.highscore = PlayerPrefs.GetInt("HighScore");
        //キャラクターがロックされてるか情報を取得
        Lock1 = PlayerPrefs.GetInt("Lock1");
        Lock2 = PlayerPrefs.GetInt("Lock2");

    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coin.ToString();
        HighScoreText.text = GameManager.highscore.ToString();

        //キャラクター1が解放されていたら、使えるようにする
        if (Lock1 == 1)
        {
            lockImage1.SetActive(false);
            unlockButton1.SetActive(false);
            applyButton1.SetActive(true);
        }
        //キャラクター2が解放されていたら、使えるようにする
        if (Lock2 == 1)
        {
            lockImage2.SetActive(false);
            unlockButton2.SetActive(false);
            applyButton2.SetActive(true);
        }

    }

    //キャラクター1を解放する処理
    public void UnlockButton1()
    {
        //コインが足りなかったら、テキストメッセージを表示
        if(coin < 1000)
        {
            notenoughText1.SetActive(true);
        }
        else
        {
            //キャラクター解放
            FindObjectOfType<AudioManager>().PlaySound("Unlock");
            lockImage1.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText1.SetActive(true);
            unlockButton1.SetActive(false);
            applyButton1.SetActive(true);
            PlayerPrefs.SetInt("Lock1", 1);

        }
    }

    //キャラクター2を解放する処理
    public void UnlockButton2()
    {
        //コインが足りなかったら、テキストメッセージを表示
        if (coin < 1000)
        {
            notenoughText2.SetActive(true);
        }
        else
        {
            //キャラクター解放
            FindObjectOfType<AudioManager>().PlaySound("Unlock");
            lockImage2.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText2.SetActive(true);
            unlockButton2.SetActive(false);
            applyButton2.SetActive(true);
            PlayerPrefs.SetInt("Lock2", 1);

        }
    }
}
