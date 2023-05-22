using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyBullet;
    [SerializeField]
    Transform EnemyBulletPoint;
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    float delay;
    [SerializeField]
    int burstCount = 3;
    [SerializeField]
    float burstInterval = 0.02f;

    [SerializeField] Transform playerTransform;
    public float hp = 0;
    public float EnemyBulletPower = 1;
    private PlayerControll playerControll;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        this.playerControll = FindObjectOfType<PlayerControll>();
        InvokeRepeating("SpawnBulletBurst", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnBulletBurst()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position;
            StartCoroutine(SpawnBullets(targetPosition));
        }
    }

    private IEnumerator SpawnBullets(Vector3 targetPosition)
    {
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
        if (collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp -= playerControll.Bullet1Power;
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
