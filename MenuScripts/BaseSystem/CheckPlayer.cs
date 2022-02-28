using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private GameObject[] PlayerList;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("Character");

        PlayerList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            PlayerList[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in PlayerList)
        {
            go.SetActive(false);
        }

        PlayerList[index].SetActive(true);
    }
}
