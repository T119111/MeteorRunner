using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private GameObject[] PlayerList; //キャラクターの画像が格納されたリスト
    private int index; //キャラクターの番号

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("Character");　//選択されたキャラクター

        PlayerList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            PlayerList[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in PlayerList)
        {
            go.SetActive(false); //すべての画像を非アクティブ
        }

        PlayerList[index].SetActive(true); //選択されたキャラクターだけ表示
    }
}
