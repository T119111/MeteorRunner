using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ailen : MonoBehaviour
{
    //public GameObject player;
    //public GameObject rocket;
    float a_x = 0.0f;
    float a_y = 0.0f;
    float a_z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //ランダムでエイリアンのポジションを決定する
        a_x = Random.Range(-2.0f, 2.0f);
        a_z = Random.Range(150.0f, 200.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Transform yourTransform = player.transform;
        Vector3 pos = yourTransform.position;

        //プレイヤーの位置に応じてロケット発射
        if(pos.z > 100)
        {
            rocket.SetActive(true);
        }
        */

        //エイリアンのポジションをセット
        transform.localPosition = new Vector3(a_x, a_y, a_z);
    }
}
