using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownTMP : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private TMP_Dropdown dropdown;
    public TextMeshProUGUI DebugText;

    private void Start()
    {
        DebugText.GetComponent<TextMeshProUGUI>().text = "";
        PlayFabLogin.Instance.RequestLeaderBoard(0);
    }

    public void OnValueChange()
    {
        //DropdownのValueが0のとき（Stage1が選択されているとき）
        if (dropdown.value == 0)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(0);
        }
        //DropdownのValueが1のとき（Stage2が選択されているとき）
        else if (dropdown.value == 1)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(1);
        }
        //DropdownのValueが2のとき（Stage3が選択されているとき）
        else if (dropdown.value == 2)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(2);
        }
        //DropdownのValueが3のとき（Stage4が選択されているとき）
        else if (dropdown.value == 3)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(3);
        }
        //DropdownのValueが4のとき（Stage5が選択されているとき）
        else if (dropdown.value == 4)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(4);
        }
        else if (dropdown.value == 5)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(5);
        }
        else if (dropdown.value == 6)
        {
            DebugText.GetComponent<TextMeshProUGUI>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(6);
        }
    }
}