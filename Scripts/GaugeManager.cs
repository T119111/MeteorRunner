using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    public GameObject[] gauge;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Obstacle.i < 3)
        {
            gauge[Obstacle.i].SetActive(true);
        }
    }
}
