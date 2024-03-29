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
     float delay = 2.5f;
    [SerializeField]//AΛΤu
    int burstCount = 3;
    [SerializeField]//AΛΜΤu
    float burstInterval = 0.3f;
    [SerializeField]//GtFNgvnu
    GameObject Anim;

    private GameObject player;
    public float hp = 1;//ΜΝ
    public float EnemyBulletPower = 1;//_[WΚ
    private bool canAttack = true;//UΕ«ι©
    private PlayerControll playerControll;
    private GameManager gameManager;
    private AutoStage autoStage;
    private ScoreManager scoreManager;
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
        autoStage = FindObjectOfType<AutoStage>();
        scoreManager = FindObjectOfType<ScoreManager>();
        InvokeRepeating("SpawnBulletBurst", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(2.0f,0,0)*Time.deltaTime;
        if (this.transform.position.x <= player.transform.position.x)
        {
            if (canAttack)
            {
                Turn();
                canAttack = false;
            }
            burstCount = 0;
        }
        if (autoStage.leftBottom.x >= this.transform.position.x)
        {
            Destroy(this.gameObject);
        }
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
    private void Turn()
    {
        transform.Rotate(0, 180, 0);
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //½θ»θ
        if (collider2D.gameObject.tag == "Bullet1")
        {
            Destroy(collider2D.gameObject);
            hp = hp -= playerControll.Bullet1Power;
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            hp = hp -= playerControll.Bullet2Power;
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
        if (hp <= 0)
        {
            Instantiate(Anim, transform.position, Quaternion.identity);
            scoreManager.EnemyScoreAdd();
            gameManager.EnemyDefeat += 1;
            Destroy(this.gameObject);
        }
    }
}
