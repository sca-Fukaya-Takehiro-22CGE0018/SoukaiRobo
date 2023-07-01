using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField]//eΜvnu
    GameObject EnemyBullet;
    [SerializeField]//Λn_
    Transform EnemyBulletPoint;
    [SerializeField]//eΜXs[h
    float bulletSpeed;
    [SerializeField]//ΛΤu
     float delay;
    [SerializeField]//AΛΤu
    int burstCount = 3;
    [SerializeField]//AΛΜΤu
    float burstInterval = 0.02f;

    private GameObject player;
    public float hp = 0;//ΜΝ
    public float EnemyBulletPower = 1;//_[WΚ
    private PlayerControll playerControll;
    private GameManager gameManager;

    public float Delay
    {
        get { return this.delay;}
        set { this.delay = value;}
    }
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    }

    public void SpawnBulletBurst()
    {
        //pllayerΜΚuπT·
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            StartCoroutine(SpawnBullets(targetPosition));
        }
    }

    public IEnumerator SpawnBullets(Vector3 targetPosition)
    {
        //ΛVXe
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
        //½θ»θ
        if (collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            hp = hp -= playerControll.Bullet2Power;
        }
        if (hp <= 0)
        {
            gameManager.EnemyDefeat += 1;
            Destroy(this.gameObject);
        }
    }
}
