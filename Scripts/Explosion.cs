using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject meteor;
    ParticleSystem p;

    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //隕石の位置情報取得
        Transform yourTransform = meteor.transform;
        Vector3 pos = yourTransform.position;
        Transform myTransform = this.transform;
        myTransform.position = pos;

        //隕石が地面に着いた時爆発
        if (pos.y < 0.0f)
        {
            p.Play();
        }   

    }
}
