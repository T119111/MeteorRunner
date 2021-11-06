using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public Rigidbody myRigid;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //シールドがプレイヤーを追従
        Transform yourTransform = player.transform;
        Vector3 pos = yourTransform.position;
        Transform myTransform = this.transform;
        pos.y += 2;
        myTransform.position = pos;

        //3回シールドが障害物にあったたら消える
        if(Obstacle.i > 3)
        {
            this.gameObject.SetActive(false);
        }
    }
}
