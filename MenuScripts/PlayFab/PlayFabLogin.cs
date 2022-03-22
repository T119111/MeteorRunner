using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PlayFabのログイン処理を行うクラス
/// </summary>
public class PlayFabLogin : MonoBehaviour
{

    //アカウントを作成するか
    private bool _shouldCreateAccount;
    //ログイン時に使うID
    private string _customID;
    public static PlayFabLogin Instance;
    public int firstgame = 0;
    public int firstDisplay = 0;
    public GameObject MainMenu;
    public GameObject Input;
    public GameObject NameConfirm;
    public GameObject UserConfirm;
    public GameObject NomalText;
    public GameObject ErrorText;

    public void Awake()
    {
        Instance = this;
    }

    //=================================================================================
    //ログイン処理
    //=================================================================================

    public void Start()
    {
        Login();

        //初めてのプレイであればユーザ名の入力画面を表示
        firstgame = PlayerPrefs.GetInt("FirstGame");
        firstDisplay = PlayerPrefs.GetInt("FirstDisplay");
        if (firstgame == 0 && firstDisplay == 0)
        {
            MainMenu.SetActive(false);
            Input.SetActive(true);
            UserConfirm.SetActive(true);
            PlayerPrefs.SetInt("FirstDisplay", 1);
        }
        else
        {
            MainMenu.SetActive(true);
            Input.SetActive(false);

        }
    }

    //ログイン実行
    private void Login()
    {
        _customID = LoadCustomID();
        var request = new LoginWithCustomIDRequest { CustomId = _customID, CreateAccount = _shouldCreateAccount };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    //ログイン成功
    private void OnLoginSuccess(LoginResult result)
    {
        //アカウントを作成しようとしたのに、IDが既に使われていて、出来なかった場合
        if (_shouldCreateAccount && !result.NewlyCreated)
        {
            Debug.LogWarning($"CustomId : {_customID} は既に使われています。");
            Login();//ログインしなおし
            return;
        }

        //アカウント作成時にIDを保存
        if (result.NewlyCreated)
        {
            SaveCustomID();
        }
        //Debug.Log($"PlayFabのログインに成功\nPlayFabId : {result.PlayFabId}, CustomId : {_customID}\nアカウントを作成したか : {result.NewlyCreated}");

        //アカウントが作成してある
        if (!result.NewlyCreated)
        {
            PlayerPrefs.SetInt("FirstGame", 1);
        }
    }

    //ログイン失敗
    private void OnLoginFailure(PlayFabError error)
    {
        //Debug.LogError($"PlayFabのログインに失敗\n{error.GenerateErrorReport()}");
    }

    //=================================================================================
    //カスタムIDの取得
    //=================================================================================

    //IDを保存する時のKEY
    private static readonly string CUSTOM_ID_SAVE_KEY = "CUSTOM_ID_SAVE_KEY";

    //IDを取得
    private string LoadCustomID()
    {
        //IDを取得
        string id = PlayerPrefs.GetString(CUSTOM_ID_SAVE_KEY);

        //保存されていなければ新規生成
        _shouldCreateAccount = string.IsNullOrEmpty(id);
        return _shouldCreateAccount ? GenerateCustomID() : id;
    }

    //IDの保存
    private void SaveCustomID()
    {
        PlayerPrefs.SetString(CUSTOM_ID_SAVE_KEY, _customID);
    }

    //=================================================================================
    //カスタムIDの生成
    //=================================================================================

    //IDに使用する文字
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";

    //IDを生成する
    private string GenerateCustomID()
    {
        int idLength = 32;//IDの長さ
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();

        //ランダムにIDを生成
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }

        return stringBuilder.ToString();
    }

    //ユーザ名登録
    public void SetUserName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserTitleDisplayNameResult result)
        {
            NameConfirm.SetActive(true);
            Input.SetActive(false);
            NomalText.SetActive(true);
            ErrorText.SetActive(false);
        }

        void OnError(PlayFabError error)
        {
            NameConfirm.SetActive(false);
            Input.SetActive(true);
            NomalText.SetActive(false);
            ErrorText.SetActive(true);
        }
    }

    //ランキング取得
    public void RequestLeaderBoard(string index)
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {

                StatisticName = "HighScoreStage" + index,
                StartPosition = 0,
                MaxResultsCount = 10
            },
            result =>
            {
                result.Leaderboard.ForEach(
                    x => Debug.Log(string.Format(": {0}\n: {1}", x.DisplayName, x.StatValue))
                    );
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }
}