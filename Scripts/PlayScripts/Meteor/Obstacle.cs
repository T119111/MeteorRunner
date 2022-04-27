using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    static public int i = 0; //シールドに当たった数を格納

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーに衝突時
        if (collision.gameObject.CompareTag("Player")) 
            PlayerMovement.instance.Die(); //ゲームオーバー

        //エイリアンに衝突時
        if (collision.gameObject.CompareTag("Alien"))
            Destroy(this.gameObject); //エイリアンに当たったら消去
    }
}
