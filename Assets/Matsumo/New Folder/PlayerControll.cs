using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb; // Player rb
    [SerializeField]
    private Collider2D jumpCollider;
    [SerializeField]
    private float movementSpeed; // Player 移動速度
    [SerializeField]
    private float jumpForce = 5f; // ジャンプ力
    [SerializeField]
    private float jumpInterval = 0.5f; // ジャンプの間隔
    [SerializeField]
    private GameObject bulletPrefab1; // 通常弾
    [SerializeField]
    private GameObject bulletPrefab2; // 強い弾 CTあり
    public float Bullet1Power = 1;

    private bool isJumping = true; // ジャンプ中のフラグ
    private bool isFalling = false; // 下降中のフラグ
    private bool isGrounded = false; // 地面に接地しているかのフラグ
    private bool isShootingEnabled = true; // CT用

    private Vector2 velocity;
    private Vector3 bulletPoint; // 弾の発射地点
    private float jumpTimer = 0f; // ジャンプタイマー
    private float lifeTimer = 1.0f; //画面外の時の〇秒ごとにHPが減る

    private EnemyControll enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    private PanelManager panelManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletPoint = transform.Find("BulletPoint").localPosition;
        lifeManager = FindObjectOfType<LifeManager>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプタイマーを更新
        jumpTimer += Time.deltaTime;

        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isJumping && panelManager.panelFlag == false)
        {
            Jump();
        }

        // ジャンプタイマーが指定の間隔を超えたらジャンプ可能にする
        if (jumpTimer >= jumpInterval)
        {
            isJumping = true;
        }
        // 移動処理
        float movementInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementInput * movementSpeed, rb.velocity.y);

        // 弾発射処理
        if (Input.GetMouseButtonDown(0) && panelManager.panelFlag == false)
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

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = false;
        jumpTimer = 0f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ステージとの接触判定
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = true;
            isFalling = false;
        }
        if (collision.gameObject.tag == "Heart")
        {
            Debug.Log("触れた");
            lifeManager.HeartRecovery();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //衝突終了時
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = false;
            isFalling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //当たり判定
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
    }
}
