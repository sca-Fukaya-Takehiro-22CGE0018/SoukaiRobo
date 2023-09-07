using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    GameObject Anim;
    [SerializeField]
    GameObject BulletAnim;
    //�ړ��֌W
    private float cos;
    private float VerticalWidth = 6.0f;//�����ړ���X���W
    private float VerticalHeight = 2.0f;//�����ړ��̍���(��)
    private float height = 1.0f;//�����ړ��̍�������
    private float speed = 2.0f;//�ړ����x
    private float Hp = 70;//�w����HP
    private int MoveCount = 2;
    private int ChangeCount = 2;//�ړ��̌����ύX
    private int MaxCount = 6;
    private bool lotateCheck = false;//��]���邩�̊m�F
    private bool VerticalMove = false;//�����ړ�
    private bool HorizontalMove = false;//�����ړ�
    private bool CountCheck = false;

    //�U���֌W
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    private GameObject Battery;
    [SerializeField]
    private GameObject HelicopterBullet;
    private GameObject player;
    private float BombTimer;//���e�����̎���
    private float MinBombTime = 0.1f;
    private float MaxBombTime = 0.3f;
    private float XshotPosition = 2.0f;
    private float YshotPosition = 2.0f;
    private float BulletSpeed = 6.0f;
    private int AttackCount = 4;
    private bool AttackCheck = true;

    private Collider2D co2D;
    private PlayerControll playerControll;
    private AutoStage autoStage;
    private ScoreManager scoreManager;
    private Animator anim;
    private Vector3 AnimPosition;
    // Start is called before the first frame update
    void Start()
    {
        co2D = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        VerticalMove = true;
        HorizontalMove = false;
        BombTimer = Random.Range(MinBombTime,MaxBombTime);
        playerControll = FindObjectOfType<PlayerControll>();
        autoStage = FindObjectOfType<AutoStage>();
        scoreManager = FindObjectOfType<ScoreManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cos = Mathf.Cos(Time.time * speed);

        //Count�Ńw���R�v�^�[�̈ړ���ς��Ă���
        if (MoveCount == ChangeCount)
        {
            VerticalMove = true;
            HorizontalMove = false;
            co2D.isTrigger = false;
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
            VerticalMove = false;
            HorizontalMove = true;
            co2D.isTrigger = true;
            AttackCheck = true;
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }


        //�����ړ�
        if (HorizontalMove)
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

            transform.position = new Vector3(cos * (autoStage.rightTop.x+1.0f)*2, autoStage.rightTop.y-0.5f, 0);

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

        //�����ړ�
        if (VerticalMove)
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

        //cos�̒l�ŏo���̃^�C�~���O�𒲐�
        


        //��]
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

    //��]�̊֐�
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
            Instantiate(BulletAnim, collider2D.gameObject.transform.position, Quaternion.identity);
            Hp = Hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            Hp = Hp -= playerControll.Bullet2Power;
        }
        if (Hp <= -0)
        {
            scoreManager.BossScoreAdd();
            autoStage.GameClear();
            AnimPosition = new Vector3(transform.position.x-1.0f, autoStage.rightTop.y -2.0f, 0);
            Instantiate(Anim, AnimPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
