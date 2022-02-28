using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    static public int i = 0; //シールドに当たった数を格納

    private void OnCollisionEnter(Collision collision)
    {
        //シールドに衝突時
        if (collision.gameObject.CompareTag("Shield"))
        {
            //シールドに当たって障害物が消える
            Destroy(this.gameObject);
            //障害物がシールドに当たった時の音を再生
            SoundManager.Instance.PlaySE(SESoundData.SE.Shield);
            //シールドに当たった数を数える
            i++;
        }

        //プレイヤーに衝突時
        if (collision.gameObject.CompareTag("Player"))
        {
            //ゲームオーバー
            PlayerMovement.instance.Die();
        }

        //エイリアンに衝突時
        if (collision.gameObject.CompareTag("Alien"))
        {
            //エイリアンに当たったら消去
            Destroy(this.gameObject);
        }
    }
}
