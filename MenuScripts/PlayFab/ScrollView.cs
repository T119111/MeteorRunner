using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollView : MonoBehaviour
{
    public TextMeshProUGUI DebugText;

    private void Awake()
    {
        Application.logMessageReceived += LoggedCb;
    }

    public void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        if (DebugText != null)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text += logstr;
            DebugText.GetComponent<TextMeshProUGUI>().text += "\n";
        }
    }
}
