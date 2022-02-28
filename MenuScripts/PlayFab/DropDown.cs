using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private Dropdown dropdown;
    public GameObject DebugText;

    private void Start()
    {
        DebugText.GetComponent<Text>().text = "";
        PlayFabLogin.Instance.RequestLeaderBoard(0);
    }

    public void RankingDisplay()
    {
        //DropdownのValueが0のとき（Stage1が選択されているとき）
        if (dropdown.value == 0)
        {
            DebugText.GetComponent<Text>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(0);
        }
        //DropdownのValueが1のとき（Stage2が選択されているとき）
        else if (dropdown.value == 1)
        {
            DebugText.GetComponent<Text>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(1);
        }
        //DropdownのValueが2のとき（Stage3が選択されているとき）
        else if (dropdown.value == 2)
        {
            DebugText.GetComponent<Text>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(2);
        }
        //DropdownのValueが3のとき（Stage4が選択されているとき）
        else if (dropdown.value == 3)
        {
            DebugText.GetComponent<Text>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(3);
        }
        //DropdownのValueが4のとき（Stage5が選択されているとき）
        else if (dropdown.value == 4)
        {
            DebugText.GetComponent<Text>().text = "";
            PlayFabLogin.Instance.RequestLeaderBoard(4);
        }
    }
}