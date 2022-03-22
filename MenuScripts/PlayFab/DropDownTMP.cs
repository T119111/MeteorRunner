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
    public int drop = 0;
    public string index;

    private void Start()
    {
        DebugText.GetComponent<TextMeshProUGUI>().text = "";
        PlayFabLogin.Instance.RequestLeaderBoard("1");
    }

    public void OnValueChange()
    {
        drop = dropdown.value + 1;
        index = drop.ToString();

        DebugText.GetComponent<TextMeshProUGUI>().text = "";
        PlayFabLogin.Instance.RequestLeaderBoard(index);
    }
}