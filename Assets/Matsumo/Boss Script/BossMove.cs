using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    #region//インスペクター
    [Header("移動速度")]public float speed;
    [Header("重力")]public float gravity;
    [Header("体力")]public float hp;
    [Header("射撃間隔")]public float delay;
    [Header("レーザー")]public GameObject Bulletlaser;
    [Header("レーザースピード")] public float laserSpeed;
    [Header("レーザー間隔")]public float laserdelay;
    [Header("レーザー射撃ポイント")]public Transform laserPoint;
    [Header("爆破エフェクト")]public GameObject Anim;
    #endregion

    #region//プライベート変数
    private GameObject player;
    private float backTime = 0.01f;
    private float time = 0;
    private Rigidbody2D rb = null;
    private bool rightTleftF = false;
    private bool HitDamage = false;
    private Animator anim;
    #endregion

    #region//スクリプト参照
    private PlayerControll playerControll;
    private Head headCollider;
    #endregion

    public float HP
    {
        get { return this.hp;}
        set { this.hp = value;}
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.playerControll = FindObjectOfType<PlayerControll>();
        this.headCollider = FindObjectOfType<Head>();
        InvokeRepeating("Spawnlaser",delay,delay);
    }

    // Update is called once per frame
    void Update()
    {// エラーが出ていたのでコメントアウトしてあります
        // ヒットしたオブジェクトの処理
        if (headCollider.HeadDamage == false)
        {
            Debug.Log("a");
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            time += Time.deltaTime;
            if (backTime <= time)
            {
                headCollider.HeadDamage= false;
                rb.AddForce(-transform.forward);
                transform.Translate(speed*0.5f, 0, 0);
                time = 0;
            }
            Debug.Log("下がる");
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Spawnlaser()
    {
        if(player != null)
        {
            Vector3 targetPosition = player.transform.position;
            StartCoroutine(Attacklaser(targetPosition));
        }
    }

    public IEnumerator Attacklaser(Vector3 targetPosition)
    {
        Vector3 spawnPosition = laserPoint.position;
        GameObject bulletlaser = Instantiate(Bulletlaser,spawnPosition,Quaternion.identity);
        Vector3 direction = (targetPosition - spawnPosition).normalized;

        // 弾の向きを調整する
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bulletlaser.transform.rotation = rotation;
        Rigidbody2D bulletRigidbody = bulletlaser.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction * laserSpeed;
        }
        yield return new WaitForSeconds(laserdelay);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp - playerControll.Bullet1Power;
        }
        if (hp <= 0)
        {
            Instantiate(Anim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
