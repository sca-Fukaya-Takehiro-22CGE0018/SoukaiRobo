using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    /// <summary>
    /// キャラの行動インスペクター
    /// </summary>
    [SerializeField]
    Rigidbody2D rb; // Player rb
    [SerializeField]
    float movementSpeed; // Player 移動速度
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
    //gravity 3→4 //////

    [SerializeField]
    float jumpSpeed;//ジャンプする速度
    [SerializeField]
    float jumpHeight;//高さ制限
    [SerializeField]
    float jumpLimitTime;//ジャンプ制限時間
    [SerializeField]
    AnimationCurve dashCurve;
    [SerializeField]
    AnimationCurve jumpCurve;
    /// <summary>
    /// プライベート変数
    /// </summary>
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;//ジャンプ時間
    private float dashTime;
    private float beforeKey;
    private bool isGround = false;
    private bool isJump = false;//ジャンプ判定
    private bool isShootingEnabled = true; // CT用
    private Vector2 velocity;
    private Vector3 bulletPoint; // 弾の発射地点
    private float lifeTimer = 1.0f; //画面外の時の〇秒ごとにHPが減る
    private EnemyControll enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    private PanelManager panelManager;


    //仮のジャンプ処理のための変数//////
    [SerializeField]
    private float jumpForce = 8000.0f;
    private int jumpCount = 0;
    // Gravity Scaleを0→1に変更しています
    

    // Start is called before the first frame update
    void Start()
    {
        bulletPoint = transform.Find("BulletPoint").localPosition;
        lifeManager = FindObjectOfType<LifeManager>();
        panelManager = FindObjectOfType<PanelManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1);
            dashTime += Time.deltaTime;
            xSpeed = movementSpeed;
        }
        else if (horizontalKey < 0)
        {
            
            dashTime += Time.deltaTime;
            xSpeed = -movementSpeed;
        }
        else
        {
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }
        //前回の入力からダッシュの反転を判断して速度を変える
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        //アニメーションカーブを速度に適用 New
        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        rb.velocity = new Vector2(xSpeed, ySpeed);
        // 弾発射処理
        if (Input.GetMouseButtonDown(0) /*&& panelManager.panelFlag == false*/)
        {
            Fire1();
        }
        if (Input.GetMouseButtonDown(1) && isShootingEnabled)
        {
            Instantiate(bulletPrefab2, transform.position + bulletPoint, Quaternion.identity);
            StartCoroutine(EnableShooting());
        }

        //落下判定
        if (transform.position.y <= -8.0f)
        {
            lifeManager.HideHeart();
        }

        //画面外判定
        if (transform.position.x <= -8.0f)
        {
            lifeTimer -=  Time.deltaTime;
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
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
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
