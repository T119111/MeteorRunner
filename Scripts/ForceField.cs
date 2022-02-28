using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Rigidbody myRigid;
    public GameObject player;
    public GameObject gauge;

    // Update is called once per frame
    void Update()
    {
        //シールドがプレイヤーを追従
        Transform yourTransform = player.transform;
        Vector3 pos = yourTransform.position;
        Transform myTransform = this.transform;
        pos.y += 0.8f;
        myTransform.position = pos;

        //3回バリアが障害物に当たったら消える
        if (Obstacle.i > 2)
        {
            this.gameObject.SetActive(false);
            gauge.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //エイリアンに衝突した場合
        if (collision.gameObject.CompareTag("Alien"))
        {
            Obstacle.i++;
            /*
            //シールドを非表示
            this.gameObject.SetActive(false);
            gauge.SetActive(false);
            */
        }
    }
}
