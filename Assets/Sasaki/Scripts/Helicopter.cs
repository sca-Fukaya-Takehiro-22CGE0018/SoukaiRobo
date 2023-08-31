using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helicopter : MonoBehaviour
{
    //移動関係
    private float cos;
    private float HorizontalWidth = 16.0f;//横移動の幅
    private float HorizontalHeight = 3.5f;//横移動の時の高さ
    private float VerticalWidth = 6.0f;//垂直移動のX座標
    private float VerticalHeight = 2.0f;//垂直移動の高さ(幅)
    private float height = 1.0f;//垂直移動の高さ調整
    private float speed = 2.0f;//移動速度
    private float Hp = 70;//ヘリのHP
    private int MoveCount = 2;
    private int ChangeCount = 2;//移動の向き変更
    private int MaxCount = 6;
    private bool lotateCheck = false;//回転するかの確認
    private bool VerticalMove = false;
    private bool HorizontalMove = false;
    private bool CountCheck = false;

    //攻撃関係
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    private GameObject Battery;
    [SerializeField]
    private GameObject HelicopterBullet;
    private GameObject player;
    private float BombTimer;//爆弾投下の時間
    private float MinBombTime = 0.1f;
    private float MaxBombTime = 0.3f;
    private float XshotPosition = 2.0f;
    private float YshotPosition = 2.0f;
    private float BulletSpeed = 6.0f;
    private int AttackCount = 4;
    private bool AttackCheck = true;

    private PlayerControll playerControll;
    private AutoStage autoStage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        VerticalMove = false;
        HorizontalMove = true;
        BombTimer = Random.Range(MinBombTime,MaxBombTime);
        playerControll = FindObjectOfType<PlayerControll>();
        autoStage = FindObjectOfType<AutoStage>();
    }

    // Update is called once per frame
    void Update()
    {
        cos = Mathf.Cos(Time.time * speed);

        //HPが0になったらplayerに突撃
        if (Hp <= 0.0f)
        {
            return;
        }

        //Countでヘリコプターの移動を変えている
        if (MoveCount == ChangeCount)
        {
            VerticalMove = false;
            HorizontalMove = true;
            VerticalHeight = autoStage.rightTop.y;
            height = autoStage.rightTop.y;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (MoveCount == ChangeCount+1)
        {
            VerticalHeight = 1.0f;
            height = 2.0f;
        }
        if (MoveCount == AttackCount)
        {
            if (AttackCheck)
            {
                AttackCheck = false;
                BulletsAttack();
            }
        }
        if (MoveCount == MaxCount-1)
        {
            Debug.Log("Change");
            VerticalHeight = autoStage.rightTop.y;
            height = autoStage.rightTop.y;
            if (cos >= 0.9f)
            {
                if (CountCheck)
                {
                    ++MoveCount;
                    CountCheck = false;
                }
            }
        }
        if (MoveCount == MaxCount)
        {
            MoveCount = 0;
            VerticalMove = true;
            HorizontalMove = false;
            AttackCheck = true;
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }


        //水平移動
        if (VerticalMove)
        {
            if (cos >= 0.9f)
            {
                if (CountCheck)
                {
                    CountCheck = false;
                    ++MoveCount;
                }
            }
            else
            {
                CountCheck = true;
            }

            transform.position = new Vector3(cos * (autoStage.rightTop.x+1.0f)*2, autoStage.rightTop.y-2.5f, 0);

            if (transform.position.x >= autoStage.leftBottom.x && transform.position.x <= autoStage.rightTop.x)
            {
                BombTimer -= Time.deltaTime;
            }
            if (BombTimer <= 0.0f)
            {
                BombTimer = Random.Range(MinBombTime, MaxBombTime);
                BombAttack();
            }
        }

        //垂直移動
        if (HorizontalMove)
        {
            if (cos <= -0.9f)
            {
                if (CountCheck)
                {
                    CountCheck = false;
                    ++MoveCount;
                }
            }
            else
            {
                CountCheck = true;
            }
            transform.position = new Vector3(VerticalWidth, cos * VerticalHeight + height,0);
        }

        //cosの値で出現のタイミングを調整
        


        //回転
        if (transform.position.x <= autoStage.leftBottom.x-3.5f || transform.position.x >= autoStage.rightTop.x+3.5f)
        {
            if (lotateCheck)
            {
                TurnAround();
            }
        }
        else
        {
            lotateCheck = true;
        }
    }

    //回転の関数
    void TurnAround()
    {
        lotateCheck = false;
        transform.Rotate(0, 180, 0);
    }

    void BombAttack()
    {
        int BatteryDrop = Random.Range(0,20);
        if (BatteryDrop == 1)
        {
            Instantiate(Battery, this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Bomb, this.transform.position, Quaternion.identity);
        }
    }

    void BulletsAttack()
    {
        Vector3 targetPosition = player.transform.position;
        StartCoroutine(BulletsShot(targetPosition));
    }

    IEnumerator BulletsShot(Vector3 targetPosition)
    {
        for (int i = 0; i < 5; ++i)
        {
            Vector3 shotPosition = new Vector3(XshotPosition,YshotPosition,0);
            GameObject Bullet = Instantiate(HelicopterBullet,shotPosition,Quaternion.identity);
            Rigidbody2D BulletRigidbody = Bullet.GetComponent<Rigidbody2D>();
            if (BulletRigidbody != null)
            {
                Vector3 dir = (targetPosition - shotPosition).normalized;
                BulletRigidbody.velocity = dir * BulletSpeed;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Bullet1")
        {
            Destroy(collider2D.gameObject);
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
