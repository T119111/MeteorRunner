using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    float x = 0.0f;
    float y = 0.5f;
    float z = 0.0f;
    public int index = 0;
    public float speed = 0.8f;
    [SerializeField] Rigidbody rb;
    PlayerMovement playerMovement;
    public bool isCalledOnece = false; //1回だけ実行するため

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        //ランダムでエイリアンのポジションを決定する
        x = Random.Range(-1.5f, 1.5f);
        z = Random.Range(50.0f, 400.0f);

        //エイリアンのポジションをセット
        transform.localPosition = new Vector3(x, y, z);

        //移動形式を乱数で決定
        index = Random.Range(0, 2);

        //スピードをランダムで決定
        speed = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (index == 0)
        {
            //前進
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
        else
        {
            Vector3 horizontalMove = transform.right * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + horizontalMove);

            //横移動
            if (pos.x > 2.1f)
            {
                if(!isCalledOnece)
                {
                    isCalledOnece = true;
                    speed *= -1.0f;
                }
                
            }
            else if(pos.x < -2.1f)
            {
                if (!isCalledOnece)
                {
                    isCalledOnece = true;
                    speed *= -1.0f;
                }
            }
            else
            {
                isCalledOnece = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //ゲームオーバー
            playerMovement.Die();
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            //障害物がシールドに当たった時の音を再生
            SoundManager.Instance.PlaySE(SESoundData.SE.Shield);
            //シールドに当たってエイリアンが消える
            Destroy(this.gameObject);
        }
    }
}
