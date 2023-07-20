using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    //�ړ��֌W
    private float cos;
    private float HorizontalWidth = 14.0f;//���ړ��̕�
    private float HorizontalHeight = 3.5f;//���ړ��̎��̍���
    private float VerticalWidth = 6.0f;//�����ړ���X���W
    private float VerticalHeight = 2.0f;//�����ړ��̍���(��)
    private float height = 1.0f;//�����ړ��̍�������
    private float speed = 2.0f;//�ړ����x
    private int MoveCount = 0;
    private int ChangeCount = 4;//�ړ��̌����ύX
    private int MaxCount = 10;
    private bool lotateCheck = false;//��]���邩�̊m�F
    private bool VerticalMove = false;
    private bool HorizontalMove = false;
    private bool CountCheck = false;

    //�U���֌W
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    private GameObject HelicopterBullet;
    private GameObject player;
    private float BombTimer;//���e�����̎���
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


        //Count�Ńw���R�v�^�[�̈ړ���ς��Ă���
        if (MoveCount == ChangeCount)
        {
            VerticalMove = false;
            HorizontalMove = true;
            VerticalHeight = 8.0f;
            height = 7.0f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (MoveCount == ChangeCount+1)
        {
            VerticalHeight = 2.0f;
            height = 1.0f;
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
            height = 7.0f;
        }
        if (MoveCount == MaxCount)
        {
            MoveCount = 0;
            VerticalMove = true;
            HorizontalMove = false;
            AttackCheck = true;
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }


        //�����ړ�
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

        //�����ړ�
        if (HorizontalMove)
        {
            transform.position = new Vector3(VerticalWidth, cos * VerticalHeight + height,0);
        }


        //cos�̒l�ŏo���̃^�C�~���O�𒲐�
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


        //��]
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

    //��]�̊֐�
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
