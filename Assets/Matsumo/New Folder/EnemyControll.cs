using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField]//弾のプレハブ
    GameObject EnemyBullet;
    [SerializeField]//射撃地点
    Transform EnemyBulletPoint;
    [SerializeField]//弾のスピード
    float bulletSpeed;
    [SerializeField]//射撃間隔
     float delay;
    [SerializeField]//連射間隔
    int burstCount = 3;
    [SerializeField]//連射の間隔
    float burstInterval = 0.02f;
    [SerializeField]//エフェクトプレハブ
    GameObject Anim;

    private GameObject player;
    public float hp = 0;//体力
    public float EnemyBulletPower = 1;//ダメージ量
    private PlayerControll playerControll;
    private GameManager gameManager;
    private Animator anim;
    public float Delay
    {
        get { return this.delay;}
        set { this.delay = value;}
    }
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        this.playerControll = FindObjectOfType<PlayerControll>();
        this.gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("SpawnBulletBurst", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(2.0f,0,0)*Time.deltaTime;
    }

    public void SpawnBulletBurst()
    {
        //pllayerの位置を探す
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            StartCoroutine(SpawnBullets(targetPosition));
        }
    }

    public IEnumerator SpawnBullets(Vector3 targetPosition)
    {
        //射撃システム
        for (int i = 0; i < burstCount; i++)
        {
            Vector3 spawnPosition = EnemyBulletPoint.position;
            GameObject bullet = Instantiate(EnemyBullet, spawnPosition, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                Vector3 direction = (targetPosition - spawnPosition).normalized;
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
            yield return new WaitForSeconds(burstInterval);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //当たり判定
        if (collider2D.gameObject.tag == "Bullet1")
        {
            Destroy(collider2D.gameObject);
            hp = hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            hp = hp -= playerControll.Bullet2Power;
        }
        if (hp <= 0)
        {
            Instantiate(Anim, transform.position, Quaternion.identity);
            gameManager.EnemyDefeat += 1;
            Destroy(this.gameObject);
            
        }
    }
}
