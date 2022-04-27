using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        */

        //コインがシールドと重なった時消える
        if (other.gameObject.tag == "GetShield")
        {
            Destroy(this.gameObject);
        }

        //プレイヤーが当たった時の処理
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        GameManager.instance.IncrementScore();
        Destroy(this.gameObject);
        //FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
        SoundManager.Instance.PlaySE(SESoundData.SE.Coin);
    }

    // Update is called once per frame
    void Update()
    {
        //コインを回転
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);

        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if(pos.z > 499)
        {
            Destroy(this.gameObject);
        }
    }
}
