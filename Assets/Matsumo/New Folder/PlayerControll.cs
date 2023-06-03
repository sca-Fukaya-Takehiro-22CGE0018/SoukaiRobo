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
    private float jumpForce; // Player ジャンプ力  
    [SerializeField]
    private float movementSpeed; // Player 移動速度
    [SerializeField]
    private float jumpTime; // ジャンプの長さ
    [SerializeField]
    private float jumpDelay; // ジャンプの間隔
    [SerializeField]
    private GameObject bulletPrefab1; // 通常弾
    [SerializeField]
    private GameObject bulletPrefab2; // 強い弾 CTあり
    public float Bullet1Power = 1;

    private bool isJumping = false; // ジャンプ中のフラグ
    private bool isGrounded = false; // 地面に接地しているかのフラグ
    private bool isShootingEnabled = true; // CT用

    private Vector2 velocity;
    private Vector3 bulletPoint; // 弾の発射地点

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
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping && panelManager.panelFlag == false)
        {
            StartCoroutine(Jump());
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
    }

    IEnumerator Jump()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(jumpTime);

        rb.AddForce(Vector2.down * (jumpForce*0.5f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpDelay);

        isJumping = false;
    }

    void Fire1()
    {
        Instantiate(bulletPrefab1, transform.position + bulletPoint, Quaternion.identity);
    }

    IEnumerator EnableShooting()
    {
        isShootingEnabled = false;
        yield return new WaitForSeconds(5.0f);
        isShootingEnabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
    }
}
