using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldTMP : MonoBehaviour
{
    public GameObject Input; //入力画面
    public GameObject Confirm; //確認画面
    public GameObject OKButton; //OKボタン
    public TMP_InputField Field; //InputField
    [SerializeField] TextMeshProUGUI ConfirmText; //確認テキスト
    [SerializeField] TextMeshProUGUI Text; //テキスト
    [SerializeField] TextMeshProUGUI UserName; //ユーザ名

    public void OnEndEdit()
    {
        //InputFieldに入力された文字を取得
        string input = Field.GetComponent<TMP_InputField>().text;

        //確認テキストに格納
        ConfirmText.text = input;

        //PlayFabにユーザ名を登録
        PlayFabLogin.Instance.SetUserName(input);

        //ユーザの名前を表示
        UserName.text = input;

        PlayerPrefs.SetString("UserName", input);
    }
}