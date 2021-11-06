using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    private GameObject[] CharacterList;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        //設定したキャタクター番号を呼び出し
        index = PlayerPrefs.GetInt("Character");
        //キャラクターリストの子要素を確認
        CharacterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            CharacterList[i] = transform.GetChild(i).gameObject;
        }
        //指定されていないキャラクターを非表示
        foreach (GameObject go in CharacterList)
        {
            go.SetActive(false);
        }
        //指定されたキャラクターを表示
        CharacterList[index].SetActive(true);
    }

    //左ボタンを押したら、手前のキャラを表示
    public void ToggleLeft()
    {
        CharacterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = CharacterList.Length - 1;
        }

        CharacterList[index].SetActive(true);
    }

    //右ボタンを押したら、次のキャラを表示
    public void ToggleRight()
    {
        CharacterList[index].SetActive(false);

        index++;
        if (index == CharacterList.Length)
        {
            index = 0;
        }

        CharacterList[index].SetActive(true);
    }

    //Applyボタンを押して、キャラクターを適用
    public void ApplyButton()
    {
        PlayerPrefs.SetInt("Character", index);
    }

}