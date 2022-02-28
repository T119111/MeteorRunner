using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpaceShip : MonoBehaviour
{
    private float speed = 10.0f;
    [SerializeField] Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.y < 1.0f)
        {
            //前進
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //シールドに衝突した時の処理
        if (collision.gameObject.CompareTag("Shield"))
        {
            //シールドに当たってエイリアンが消える
            Destroy(this.gameObject);

            //障害物がシールドに当たった時の音を再生
            SoundManager.Instance.PlaySE(SESoundData.SE.Shield);
        }

        //プレイヤーに衝突した時の処理
        if (collision.gameObject.CompareTag("Player"))
        {
            //ゲームオーバー
            PlayerMovement.instance.Die();
        }
    }
}
