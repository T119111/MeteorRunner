using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public int shield;
    public int useshield;
    public TextMeshProUGUI shieldText;
    public GameObject UsedShiledButton;

    // Start is called before the first frame update
    void Start()
    {
        //シールドの使用を初期化
        PlayerPrefs.SetInt("UseShield", 0);
        //持っているシールドの数を取得
        shield = PlayerPrefs.GetInt("Shield");
    }

    // Update is called once per frame
    void Update()
    {
        shieldText.text = shield.ToString();
    }

    public void UseShieldButton()
    {
        if(shield > 0)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Button);
            shield -= 1;
            UsedShiledButton.SetActive(true);
            useshield = 1;
        }
        else
            SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    public void NotUseShieldButton()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        shield += 1;
        UsedShiledButton.SetActive(false);
        useshield = 0;
    }

    public void StartButton()
    {
        PlayerPrefs.SetInt("UseShield", useshield);
        PlayerPrefs.SetInt("Shield", shield);
    }
}
