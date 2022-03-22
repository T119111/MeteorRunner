using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    static public PlayerMovement instance;
    private int index;
    public float speed = 8; //スピード
    public float speedIncresePoint = 0.2f; //コインの取得ごとに速くなる
    private float horizontalInput;　//水平方向の動き
    public bool alive = true;
    public bool shieldflag = false;
    public bool jumpflag = false;
    public bool slidingflag = false;
    CapsuleCollider c_Collider;
    float c_centerX, c_centerY, c_centerZ;
    float c_radius, c_height;
    public int current; //現在のステージ
    float jumptime = 0.0f; //ジャンプ時間間隔を空けるため実行するため
    float slidingtime = 0.0f; //スライディング時間間隔を空けるため実行するため
    public GameManager myManager;
    public GameObject gauge;
    public GameObject ShieldButton;
    public GameObject Shield;
    private GameObject[] player;
    public FixedJoystick joystick;
    private Animator anim = null;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] float jumpForce = 80f;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask groundMask;

    public void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        //移動の設定
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Start()
    {
        //現在のステージ
        current = SceneManager.GetActiveScene().buildIndex;

        //特定のキャラクターだけスピード変更
        if (index == 8)
        {
            speed = 10;
            jumpForce = 80;
        }

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

        //プレイヤーの当たり判定の設定
        c_Collider = GetComponent<CapsuleCollider>();
        if (index == 8)
        {
            c_centerX = 0.0f;
            c_centerY = 0.7f;
            c_centerZ = 0.0f;
            c_radius = 0.3f;
            c_height = 1.43f;
        }
        else
        {
            c_centerX = 0.0f;
            c_centerY = 0.85f;
            c_centerZ = 0.0f;
            c_radius = 0.35f;
            c_height = 1.75f;
        }
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
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

        //行動制限
        if(pos.x > 2.00f)　//右方向の制限
        {
            horizontalInput = 0;
            pos.x = 1.99f;
            myTransform.position = pos;
        }
        else
            horizontalInput = joystick.Horizontal;

        if (pos.x < -2.00f)　//左方向の制限
        {
            horizontalInput = 0;
            pos.x = -1.99f;
            myTransform.position = pos;
        }
        else
            horizontalInput = joystick.Horizontal;

        //ジョイスティック上入力でジャンプ
        if (verticalMove >= 0.5f)
        {
            Jump();
            jumptime += Time.deltaTime;
            //音を時間経過ごと鳴らすため
            if(jumptime >= 0.88)
            {
                jumpflag = false;
                jumptime = 0.0f;
            }
        }
        else
        {
            jumpflag = false;
            anim.SetBool("Jump", false);
        }
            
        //ジョイスティック下入力でスライディング
        if (verticalMove <= -0.5f)
        {
            Sliding();
            slidingtime += Time.deltaTime;
            //音を時間経過ごと鳴らすため
            if (slidingtime >= 1.55)
            {
                slidingflag = false;
                slidingtime = 0.0f;
            }
        }
        else
        {
            slidingflag = false;
            anim.SetBool("Sliding", false);
        }

        //スライディングしてない時の当たり判定を適用
        if(slidingflag == false)
        {
            //小柄なキャラクターの当たり判定
            if (index == 8)
            {
                c_centerX = 0.0f;
                c_centerY = 0.7f;
                c_centerZ = 0.0f;
                c_radius = 0.3f;
                c_height = 1.43f;
            }
            //一般的なキャラクターの当たり判定
            else
            {
                c_centerX = 0.0f;
                c_centerY = 0.85f;
                c_centerZ = 0.0f;
                c_radius = 0.35f;
                c_height = 1.75f;
            }
        }

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
        
        //シールドを獲得
        if (collision.gameObject.CompareTag("GetShield"))
        {
            if (!shieldflag)
                SoundManager.Instance.PlaySE(SESoundData.SE.Shield);
            shieldflag = true;
            Obstacle.i = 0;
            ShieldButton.SetActive(true);

            if(Shield == false)
                ShieldButton.SetActive(false);
        }
    }

    void Jump()
    {
        //地面に接地してるか確認
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);
        
        //ジャンプする
        if(isGrounded)
        {
            if (!jumpflag)
            {
                jumpflag = true;
                SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            }
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("Jump", true);
            
        }
    }

    void Sliding()
    {
        if (!slidingflag)
        {
            slidingflag = true;
            if(current < 7)
                SoundManager.Instance.PlaySE(SESoundData.SE.Sliding);
        }
        //スライディングアニメーション再生
        anim.SetBool("Sliding", true);

        //スライディングをしたら、当たり判定を小さくする
        if(slidingflag)
        {
            if(index == 8)
            {
                c_centerY = 0.35f;
                c_height = 0.7f;
            }
            else
            {
                c_centerY = 0.425f;
                c_height = 0.9f;
            }
        }
    }

    //シールドを使用
    public void Throw()
    {
        shieldflag = false;
        SoundManager.Instance.PlaySE(SESoundData.SE.Shield);
        Shield.SetActive(true);　//シールドを表示
        ShieldButton.SetActive(false); //ボタンを非表示
        gauge.SetActive(true); //シールドゲージを表示
    }
}
