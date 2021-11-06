using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Rigidbody myRigid;
    public GameObject player;
    //public bool fireflag;

    // Update is called once per frame
    void Update()
    {
        Transform yourTransform = player.transform;
        Vector3 pos = yourTransform.position;
        Transform myTransform = this.transform;
        pos.y += 1;
        myTransform.position = pos;

        //3回バリアが障害物に当たったら消える
        if (Obstacle.i > 3)
        {
            this.gameObject.SetActive(false);
        }
    }
}
