using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// キャラの行動インスペクター
    /// </summary>
    [SerializeField]
    Rigidbody2D rb; // Player rb
    [SerializeField]
    float movementSpeed; // Player 移動速度
    public float speed = 5f;
    private float playerSpeed;

    Rigidbody2D rigidbody2D;

    [SerializeField]
    GameObject bulletPrefab1; // 通常弾
    [SerializeField]
    GameObject bulletPrefab2; // 強い弾 CTあり
    public float Bullet2Power = 3;
    public float Bullet1Power = 1;
    /// <summary>
    /// ジャンプ関係
    /// </summary>
    [SerializeField]
    GroundCheck ground;//接触判定
    [SerializeField]
    float gravity;//重力

    /// <summary>
    /// プライベート変数
    /// </summary>
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;//ジャンプ時間
    private float dashTime;
    private float beforeKey;
    private float AttackInterval = 0.3f;
    private float AttackCoolTime;
    private bool isGround = false;
    private bool isJump = false;//ジャンプ判定
    private bool isShootingEnabled = true; // CT用
    private bool AutoAttack = true;
    private Vector2 velocity;
    private Vector3 bulletPoint; // 弾の発射地点
    private float lifeTimer = 1.0f; //画面外の時の〇秒ごとにHPが減る
    private EnemyControll enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    private PanelManager panelManager;
    private AutoStage autoStage;


    //仮のジャンプ処理のための変数//////
    [SerializeField]
    private float jumpForce = 8000.0f;
    private int jumpCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        bulletPoint = transform.Find("BulletPoint").localPosition;
        lifeManager = FindObjectOfType<LifeManager>();
        panelManager = FindObjectOfType<PanelManager>();
        autoStage = FindObjectOfType<AutoStage>();
        rb = GetComponent<Rigidbody2D>();
        AttackCoolTime = AttackInterval;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 左移動
        if(Input.GetKey(KeyCode.A))playerSpeed = -speed;
        // 右移動
        else if(Input.GetKey(KeyCode.D))playerSpeed = speed;
        // 何も押さないと止まる
        else playerSpeed = 0;

        rigidbody2D.velocity = new Vector2(playerSpeed,rigidbody2D.velocity.y);

        //仮のジャンプ処理 Sasaki//////
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            this.rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }

        //接地判定を得る
        isGround = ground.IsGround();
        // 移動処理
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        float verticalKey = Input.GetAxis("Vertical");

        //仮のジャンプ処理のため
        /*
        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上方向キーを押しているか
            bool pushUpKey = verticalKey > 0;
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        */

        // 弾発射処理
        if (Input.GetMouseButton(0) && AutoAttack)
        {
            AttackCoolTime -= Time.deltaTime;
            if (AttackCoolTime <= 0.0f)
            {
                Fire1();
                AttackCoolTime = AttackInterval;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !AutoAttack)
        {
            Fire1();
        }
        if (Input.GetMouseButtonDown(1) && isShootingEnabled)
        {
            Instantiate(bulletPrefab2, transform.position + bulletPoint, Quaternion.identity);
            StartCoroutine(EnableShooting());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (AutoAttack)
            {
                AutoAttack =false;
            }
            else
            {
                AutoAttack = true;
            }
        }

        //落下判定
        if (this.transform.position.y <= autoStage.leftBottom.y-3.0f)
        {
            lifeManager.HideHeart();
        }

        //画面外判定
        if (this.transform.position.x <= autoStage.leftBottom.x-0.6f)
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0.0f)
            {
                lifeManager.HideHeart();
                lifeTimer = 1.0f;
            }
        }
        else
        {
            lifeTimer = 1.0f;
        }

    }

    void Fire1()
    {
        Instantiate(bulletPrefab1, transform.position + bulletPoint, Quaternion.identity);
    }

    IEnumerator EnableShooting()
    {
        //強い球の射撃インターバル
        isShootingEnabled = false;
        yield return new WaitForSeconds(5.0f);
        isShootingEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //当たり判定
        if (collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
        //if(collider2D.gameObject.tag == "Heart")
        //{
        //    Debug.Log("触れた");
        //    lifeManager.HeartRecovery();
        //}
    }

    //仮のジャンプ処理のため追加//////
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
    }
}

