using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    static public int num = 0;
    public Rigidbody myRigid;
    public GameObject player;
    public GameObject gauge;

    void Start()
    {
        num = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Transform yourTransform = player.transform; //プレイヤーの位置情報
        Vector3 pos = yourTransform.position; //プレイヤーの座標を取得
        Transform myTransform = this.transform; //シールドの位置情報
        pos.y += 0.8f; //プレイヤーのy座標より少し高めに設定
        myTransform.position = pos; //シールドはy座標以外プレイヤーと同じ座標

        //3回シールドが障害物に当たったら消える
        if (num > 2)
        {
            this.gameObject.SetActive(false); //シールドオブジェクトを非アクティブ
            gauge.SetActive(false); //シールドゲージのUIを非アクティブ
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //エイリアンまたは隕石に衝突した場合
        if (collision.gameObject.CompareTag("Alien") ||
            collision.gameObject.CompareTag("Obstacle"))
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Shield); //音を再生
            Destroy(collision.gameObject); //衝突した障害物を消去
            num++; //回数を数える
        }  
    }
}
