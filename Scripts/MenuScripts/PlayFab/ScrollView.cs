using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollView : MonoBehaviour
{
    public TextMeshProUGUI DebugText; //ランキングを表示するテキスト

    private void Awake()
    {
        Application.logMessageReceived += LoggedCb; //ログの文字を受け取る
    }

    public void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        if (DebugText != null)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text += logstr; //ログの文字を格納
            DebugText.GetComponent<TextMeshProUGUI>().text += "\n"; //改行
        }
    }
}
