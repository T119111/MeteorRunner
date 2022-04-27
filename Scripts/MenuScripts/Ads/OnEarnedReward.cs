using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

public class OnEarnedReward : MonoBehaviour
{
    //リワード広告を見終わった時の処理
    public void OnUserEarnedReward()
    {
        //今まで手に入れたコイン情報を取得
        int coin = PlayerPrefs.GetInt("Coins");
        //報酬(300コイン)を与える
        coin += 300;
        PlayerPrefs.SetInt("Coins", coin);
        //ゲームクリアとリワードの効果音が同じ
        SoundManager.Instance.PlaySE(SESoundData.SE.GameClear);
    }
}
