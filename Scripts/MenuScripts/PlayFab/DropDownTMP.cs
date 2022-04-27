using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownTMP : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;　//Dropdownを格納する変数
    public TextMeshProUGUI DebugText; //ランキングを表示するテキスト
    public int drop = 0;　//ステージの番号（数字）
    public string index; //ステージの番号（文字列）

    private void Start()
    {
        DebugText.GetComponent<TextMeshProUGUI>().text = "";　//情報をリセット
        PlayFabLogin.Instance.RequestLeaderBoard("1"); //ステージ1のランキングを表示
    }

    public void OnValueChange()
    {
        drop = dropdown.value + 1;
        index = drop.ToString(); //文字列に変換

        DebugText.GetComponent<TextMeshProUGUI>().text = ""; //情報をリセット
        PlayFabLogin.Instance.RequestLeaderBoard(index); //指定した番号の情報を取得
    }
}