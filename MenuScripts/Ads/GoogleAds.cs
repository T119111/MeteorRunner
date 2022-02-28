using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine.SceneManagement;

public class GoogleAds : MonoBehaviour
{
    static public GoogleAds Instance;
    InterstitialAdGameObject interstitial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //インタースティシャル
        interstitial = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("TestInterstitialAd");

        interstitial.LoadAd();
    }

    //インタースティシャル広告を見る処理
    public void OnClickShowInterstitialButton()
    {
        interstitial.ShowIfLoaded();
    }

    //インタースティシャル広告を閉じた時の処理
    public void OnCloseButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //アプリケーションが終了時の処理
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Interstitial", 0);
    }
}