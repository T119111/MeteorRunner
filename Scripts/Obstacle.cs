using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;
    static public int i = 0;
    //static public bool explosionflag = false;

    // Start is called before the first frame update
    public void Start()
    {
        i = 0;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //ゲームオーバー
            playerMovement.Die();
        }

        if (collision.gameObject.tag == "Shield")
        {
            //障害物がシールドに当たった時の音を再生
            FindObjectOfType<AudioManager>().PlaySound("Shield");
            //シールドに当たった数を数える
            i++;
            //シールドに当たって障害物が消える
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Ground")
        { 
            //explosionflag = true;
            //Debug.Log("衝突");
        }
    }
}
