using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    public GameObject[] gauge; //シールドゲージの画像が入った親の空オブジェクト

    // Start is called before the first frame update
    void Start()
    {
        //子要素を取得
        gauge = new GameObject[transform.childCount];
        for (int j = 0; j < transform.childCount; j++)
        {
            gauge[j] = transform.GetChild(j).gameObject;
        }
        //全てのゲージ非表示
        foreach (GameObject go in gauge)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //残りのゲージを表示
        if (ForceField.num < 3)
        {
            gauge[ForceField.num].SetActive(true);
        }
    }
}
