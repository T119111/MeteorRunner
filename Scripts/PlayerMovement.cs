using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    private GameObject[] player;
    private int index;
    private Animator anim = null;
    bool alive = true;
    public float speed = 5;
    public float speedIncresePoint = 0.2f;
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] float jumpForce = 80f;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask groundMask;
    public GameManager myManager;
    public GameObject ShieldButton;
    public GameObject Shield;
    public bool shieldflag = false;
    public bool slidingflag = false;
    CapsuleCollider c_Collider;
    float c_centerX, c_centerY, c_centerZ;
    float c_radius;
    float c_height;

    private void FixedUpdate()
    {
        if (!alive) return;

        //移動の設定
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        //移動の制限
        if (pos.x > 2.5f)
        {
            pos.x = 2.5f;
            myTransform.position = pos;
        }
        if (pos.x < -2.5f)
        {
            pos.x = -2.5f;
            myTransform.position = pos;
        }
        
    }

    private void Start()
    {
        //プレイヤーの当たり判定の設定
        c_Collider = GetComponent<CapsuleCollider>();
        c_centerX = 0.0f;
        c_centerY = 0.85f;
        c_centerZ = 0.0f;
        c_radius = 0.35f;
        c_height = 1.75f;

        //プレイするキャラクター番号を呼び出し
        index = PlayerPrefs.GetInt("Character");
        //キャラクターリストの子要素を確認
        player = new GameObject[transform.childCount];
        for(int i=0; i<transform.childCount; i++)
        {
            player[i] = transform.GetChild(i).gameObject;
        }
        //プレイしないキャラクターを非表示
        foreach (GameObject go in player)
        {
            go.SetActive(false);
        }
        //プレイするキャラクターを表示
        player[index].SetActive(true);

        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        //当たり判定を適用
        c_Collider.center = new Vector3(c_centerX, c_centerY, c_centerZ);
        c_Collider.radius = c_radius;
        c_Collider.height = c_height;
        //Debug.Log("Current CapsuleCollider Heght:" + c_Collider.height);
        //ゲームマネージャーに進んでいる距離情報を受け渡し
        GameManager.instance.IncrementDistance();
        horizontalInput = joystick.Horizontal;
        float verticalMove = joystick.Vertical;
        //horizontalInput = Input.GetAxis("Horizontal");

        /*
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        else
            anim.SetBool("Jump", false);
        */
        //ジョイスティック上入力でジャンプ
        if (verticalMove >= .5f)
            Jump();
        else
            anim.SetBool("Jump", false);
        //ジョイスティックした入力でジャンプ
        if (verticalMove <= -.5f)
            Sliding();
        else
        {
            anim.SetBool("Sliding", false);
            slidingflag = false;
        }
        //スライディングしてない時の当たり判定を適用
        if(slidingflag == false)
        {
            c_centerY = 0.88f;
            c_height = 1.8f;
        }
        /*
        if (shieldflag)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Throw();
            }
        }

        if (transform.position.y < -5)
        {
            Die();
        }
        */
        //500mに到達したらゲームクリア
        if(transform.position.z > 500)
        {
            Destroy(this.gameObject);
            myManager.GameClear();
        }

    }
    //プレイヤーが障害物に当たってしまった時の処理
    public void Die()
    {
        Destroy(this.gameObject);
        myManager.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(this.gameObject);
            myManager.GameClear();
        }
        */

        //シールドを獲得
        if (collision.gameObject.tag == "GetShield")
        {
            shieldflag = true;
            FindObjectOfType<AudioManager>().PlaySound("Shield");
            Obstacle.i = 0;
            ShieldButton.SetActive(true);

            if(Shield == false)
            {
                ShieldButton.SetActive(false);
            }
        }

    }

    void Jump()
    {
        //地面に接地してるか確認
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) , groundMask);
        
        //ジャンプする
        if(isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("Jump", true);
            FindObjectOfType<AudioManager>().PlaySound("Jump");
        }
        
    }

    void Sliding()
    {
        //スライディングアニメーション再生
        anim.SetBool("Sliding", true);
        slidingflag = true;
        //スライディングをしたら、当たり判定を小さくする
        if(slidingflag)
        {
            c_centerY = 0.44f;
            c_height = 0.9f;
        }
    }

    //シールドを使用
    public void Throw()
    {
        shieldflag = false;
        FindObjectOfType<AudioManager>().PlaySound("Shield");
        //anim.SetTrigger("Throw");
        Shield.SetActive(true);
        ShieldButton.SetActive(false);
    }
}
