using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    //移動関係
    private float cos;
    private float HorizontalWidth = 14.0f;//横移動の幅
    private float HorizontalHeight = 3.5f;//横移動の時の高さ
    private float VerticalWidth = 6.0f;//垂直移動のX座標
    private float VerticalHeight = 2.0f;//垂直移動の高さ(幅)
    private float height = 1.0f;//垂直移動の高さ調整
    private float speed = 2.0f;//移動速度
    private int MoveCount = 0;
    private int ChangeCount = 4;//移動の向き変更
    private int MaxCount = 10;
    private bool lotateCheck = false;//回転するかの確認
    private bool VerticalMove = false;
    private bool HorizontalMove = false;
    private bool CountCheck = false;

    //攻撃関係
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    private GameObject HelicopterBullet;
    private GameObject player;
    private float BombTimer;//爆弾投下の時間
    private float MinBombTime = 0.1f;
    private float MaxBombTime = 0.3f;
    private float XshotPosition = 2.0f;
    private float YshotPosition = 2.0f;
    private float BulletSpeed = 6.0f;
    private int AttackCount = 6;
    private bool AttackCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        VerticalMove = true;
        BombTimer = Random.Range(MinBombTime,MaxBombTime);
    }

    // Update is called once per frame
    void Update()
    {
        cos = Mathf.Cos(Time.time * speed);


        //Countでヘリコプターの移動を変えている
        if (MoveCount == ChangeCount)
        {
            VerticalMove = false;
            HorizontalMove = true;
            VerticalHeight = 8.0f;
            height = 8.5f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (MoveCount == ChangeCount+1)
        {
            //VerticalHeight = 2.0f;
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
            VerticalHeight = 8.0f;
            height = 9.0f;
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
            transform.position = new Vector3(cos * HorizontalWidth, HorizontalHeight, 0);

            if (transform.position.x >= -4.0f && transform.position.x <= 4.0f)
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
            transform.position = new Vector3(VerticalWidth, cos * VerticalHeight + height,0);
        }


        //cosの値で出現のタイミングを調整
        if (cos <= -0.9f || cos >= 0.9f)
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


        //回転
        if (transform.position.x <= -13.0f || transform.position.x >= 13.0f)
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
        Instantiate(Bomb,this.transform.position,Quaternion.identity);
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
}
