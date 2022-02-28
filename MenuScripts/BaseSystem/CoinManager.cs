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
    public int Lock3;
    public int Lock4;
    public int Lock5;
    public int Lock6;
    public int AllClear;
    public int already = 0;
    public TextMeshProUGUI coinText;
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
    public GameObject notenoughText3;
    public GameObject unlockText3;
    public GameObject lockImage3;
    public GameObject unlockButton3;
    public GameObject applyButton3;
    public GameObject notenoughText4;
    public GameObject unlockText4;
    public GameObject lockImage4;
    public GameObject unlockButton4;
    public GameObject applyButton4;
    public GameObject notenoughText5;
    public GameObject unlockText5;
    public GameObject lockImage5;
    public GameObject unlockButton5;
    public GameObject applyButton5;
    public GameObject beforecomment;
    public GameObject aftercomment;
    public GameObject notenoughText6;
    public GameObject unlockText6;
    public GameObject lockImage6;
    public GameObject unlockButton6;
    public GameObject applyButton6;
    public GameObject beforecomment2;
    public GameObject aftercomment2;



    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameManager.coins);
        //PlayerPrefs.SetInt("Coins", coin);
        //今まで手に入れたコイン情報を取得
        coin = PlayerPrefs.GetInt("Coins");

        //ハイスコア情報を取得
        GameManager.highscore = PlayerPrefs.GetInt("HighScore");

        Lock6 = 0;
        //キャラクターがロックされてるか情報を取得
        Lock1 = PlayerPrefs.GetInt("Lock1");
        Lock2 = PlayerPrefs.GetInt("Lock2");
        Lock3 = PlayerPrefs.GetInt("Lock3");
        Lock4 = PlayerPrefs.GetInt("Lock4");
        Lock5 = PlayerPrefs.GetInt("Lock5");
        already = PlayerPrefs.GetInt("Already");
        //クリアしているか
        AllClear = PlayerPrefs.GetInt("AllClear");

    }

    // Update is called once per frame
    void Update()
    {
        coin = PlayerPrefs.GetInt("Coins");
        coinText.text = coin.ToString();
        //HighScoreText.text = GameManager.highscore.ToString();

        //最後のキャラクターを解放できる条件を格納
        Lock6 = Lock1 + Lock2 + Lock3 + Lock4 + Lock5;

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
        //キャラクター3が解放されていたら、使えるようにする
        if (Lock3 == 1)
        {
            lockImage3.SetActive(false);
            unlockButton3.SetActive(false);
            applyButton3.SetActive(true);
        }
        //キャラクター4が解放されていたら、使えるようにする
        if (Lock4 == 1)
        {
            lockImage4.SetActive(false);
            unlockButton4.SetActive(false);
            applyButton4.SetActive(true);
        }
        //全ステージクリアして解放していたら、使えるようにする
        if (Lock5 == 1)
        {
            lockImage5.SetActive(false);
            unlockButton5.SetActive(false);
            applyButton5.SetActive(true);
        }
        //クリア後にアンロックを促すコメントに変更
        if(AllClear == 1)
        {
            beforecomment.SetActive(false);
            aftercomment.SetActive(true);
        }
        //キャラクター6を解放していたら、使えるようにする
        if (Lock6 == 5 && already == 1)
        {
            lockImage6.SetActive(false);
            unlockButton6.SetActive(false);
            applyButton6.SetActive(true);
        }
        //全キャラ解放後にアンロックを促すコメントに変更
        if (Lock6 == 5)
        {
            beforecomment2.SetActive(false);
            aftercomment2.SetActive(true);
            notenoughText6.SetActive(false);
        }
    }

    //キャラクター1を解放する処理
    public void UnlockButton1()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //コインが足りなかったら、テキストメッセージを表示
        if (coin < 1000)
        {
            notenoughText1.SetActive(true);
        }
        else
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage1.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText1.SetActive(true);
            unlockButton1.SetActive(false);
            applyButton1.SetActive(true);
            Lock1 = 1;
            PlayerPrefs.SetInt("Lock1", Lock1);
        }
    }

    //キャラクター2を解放する処理
    public void UnlockButton2()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //コインが足りなかったら、テキストメッセージを表示
        if (coin < 1000)
        {
            notenoughText2.SetActive(true);
        }
        else
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage2.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText2.SetActive(true);
            unlockButton2.SetActive(false);
            applyButton2.SetActive(true);
            Lock2 = 1;
            PlayerPrefs.SetInt("Lock2", Lock2);
        }
    }

    //キャラクター3を解放する処理
    public void UnlockButton3()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //コインが足りなかったら、テキストメッセージを表示
        if (coin < 1000)
        {
            notenoughText3.SetActive(true);
        }
        else
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage3.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText3.SetActive(true);
            unlockButton3.SetActive(false);
            applyButton3.SetActive(true);
            Lock3 = 1;
            PlayerPrefs.SetInt("Lock3", Lock3);
            
        }
    }

    //キャラクター4を解放する処理
    public void UnlockButton4()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //コインが足りなかったら、テキストメッセージを表示
        if (coin < 1000)
        {
            notenoughText4.SetActive(true);
        }
        else
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage4.SetActive(false);
            coin -= 1000;
            PlayerPrefs.SetInt("Coins", coin);
            unlockText4.SetActive(true);
            unlockButton4.SetActive(false);
            applyButton4.SetActive(true);
            Lock4 = 1;
            PlayerPrefs.SetInt("Lock4", Lock4);
            
        }
    }

    //キャラクター5を解放する処理
    public void AllClearUnlockButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        if (AllClear == 0)
        {
            notenoughText5.SetActive(true);
        }
        else
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage5.SetActive(false);
            unlockText5.SetActive(true);
            unlockButton5.SetActive(false);
            applyButton5.SetActive(true);
            Lock5 = 1;
            PlayerPrefs.SetInt("Lock5", Lock5);
            
        }
    }

    //キャラクター6を解放する処理
    public void AllCharaUnlockButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        if (Lock6 == 5)
        {
            //キャラクター解放
            //FindObjectOfType<AudioManager>().PlaySound("Unlock");
            SoundManager.Instance.PlaySE(SESoundData.SE.Unlock);
            lockImage6.SetActive(false);
            unlockText6.SetActive(true);
            unlockButton6.SetActive(false);
            applyButton6.SetActive(true);
            //1で解放済み
            already = 1;
            PlayerPrefs.SetInt("Already", already);
        }
        else
        {
            notenoughText6.SetActive(true);
        }
    }
}
