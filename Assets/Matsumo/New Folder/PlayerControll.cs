using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;// Player rb
    [SerializeField]
    private Collider2D jumpCollider;
    [SerializeField]
    float jumpForce;//Player ジャンプ力  
    [SerializeField]
    float STEP;// Player 移動速度
    [SerializeField]
    GameObject BulletPrefab1; //通常弾
    [SerializeField]
    GameObject BulletPrefab2; //強い弾 CTあり
    public float Bullet1Power = 1;
    private bool jumpflag = false; //着地判定
    private bool isShoot = true; //CT用

    private Vector2 velocity;
    private Vector3 BulletPoint; //弾の発射地点

    private EnemyControll  enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        BulletPoint = transform.Find("BulletPoint").localPosition;
        this.lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(STEP * Time.deltaTime, 0,0);
        if(Input.GetKeyDown(KeyCode.Space) && !jumpflag && !(rb.velocity.y < -0.5f))
        {
            Jump();
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * STEP, rb.velocity.y);
        if (Input.GetMouseButtonDown(0))
        {
            Fire1();
        }
        if (Input.GetMouseButtonDown(1) && isShoot)
        {
            Instantiate(BulletPrefab2, transform.position + BulletPoint, Quaternion.identity);
            StartCoroutine("FireBullet");
        }
    }

    IEnumerator FireBullet()
    {
        isShoot = false;

        yield return new WaitForSeconds(5.0f);

        isShoot = true;
    }

    void Fire1()
    {
        Instantiate(BulletPrefab1, transform.position + BulletPoint, Quaternion.identity);
    }
    

    void Jump()
    {
        jumpflag = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            jumpflag = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyBullet")
        {
            lifeManager.HideHeart();
        }
        if(collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
    }
   
}
