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

    private int count;
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
           StartCoroutine(nameof(EnemyFire));
           ShootTime = 5.0f;
        }
    }

    private IEnumerator EnemyFire()
    { 
        for(int i = 0;i < 3; i++)
        {
            Instantiate(EnemyBullet, transform.position + EnemyBulletPoint, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
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
