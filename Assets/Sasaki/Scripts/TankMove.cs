using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Cannonball;//砲弾
    [SerializeField]
    private GameObject Missile;//ミサイル

    private float speed;//戦車が出てくるときのスピード
    private float Hp = 100;//戦車のHP
    private float CannonballTimer;//砲弾発射までの時間
    private float MissileTimer;//ミサイル発射までの時間
    private float CannonCoolTime = 3.0f;//砲弾発射のクールタイム
    private float MissileCoolTime = 5.0f;//ミサイル発射のクールタイム

    private bool onStage = false;

    private PlayerControll playerControll;
    // Start is called before the first frame update
    void Start()
    {
        playerControll = FindObjectOfType<PlayerControll>();
        speed = -2.0f;
        CannonballTimer = CannonCoolTime;
        MissileTimer = MissileCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        //画面外から戦車が出てくる
        transform.position += new Vector3(speed,0,0)*Time.deltaTime;
        if (transform.position.x <= 6.0f)
        {
            onStage = true;
        }
        if (onStage)
        {
            speed = 0.0f;
        }

        //CannonballTimerが0になったら砲弾発射
        CannonballTimer -= Time.deltaTime;
        if (CannonballTimer <= 0.0f)
        {
            Bombardment();
            CannonballTimer = CannonCoolTime;
        }

        //MissileTimerが0になったらミサイル発射
        MissileTimer -= Time.deltaTime;
        if (MissileTimer <= 0.0f)
        {
            MissileAttack();
            MissileTimer = MissileCoolTime;
        }
    }

    //砲撃
    void Bombardment()
    {
        Debug.Log("砲弾発射");
        Instantiate(Cannonball,
            new Vector3(transform.position.x-4.5f, transform.position.y+0.6f, transform.position.z),
            Quaternion.identity);
    }

    //ミサイル攻撃
    void MissileAttack()
    {
        Debug.Log("ミサイル発射");
        Instantiate(Missile,
            new Vector3(transform.position.x, transform.position.y+2.7f, transform.position.z),
            Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Bullet1")
        {
            Hp = Hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            Hp = Hp -= playerControll.Bullet2Power;
        }
        if (Hp <= -0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("ResultScene");
        }
    }
}
