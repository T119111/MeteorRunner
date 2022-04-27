using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public GameObject fire;
    public GameObject explosion;
    float x = 0.0f;
    float y = -2.0f;
    float z = 10.0f;
    float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        //隕石の落下
        y -= speed * Time.deltaTime;

        //隕石が地面下に行ったらまた降り注ぐ
        if (y < -1.0f)
        {
            fire.SetActive(false);
            explosion.SetActive(true);
            x = Random.Range(-3.0f, 3.0f);
            y = 12.0f;
            z = Random.Range(0.0f, 100.0f);
            speed = Random.Range(3.0f, 10.0f);
        }
        else
        {
            fire.SetActive(true);
            //explosion.SetActive(false);
        }
        //ランダムに得た位置をセット
        transform.localPosition = new Vector3(x, y, z);
    }
}