using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyBullet;
    [SerializeField]
    float ShootTime = 5.0f;

    public float hp = 0;
    public float EnemyBulletPower = 1;

    private PlayerControll playerControll;

    private Vector3 EnemyBulletPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.playerControll = FindObjectOfType<PlayerControll>();
        EnemyBulletPoint = transform.Find("EnemyBulletPoint").localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ShootTime -= Time.deltaTime;
        if (ShootTime <= 0)
        {
            EnemyFire();
            ShootTime = 5.0f;
        }
    }

    void EnemyFire()
    {
        Instantiate(EnemyBullet, transform.position + EnemyBulletPoint, Quaternion.identity);
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
