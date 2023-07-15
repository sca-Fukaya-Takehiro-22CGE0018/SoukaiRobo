using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    /// <summary>
    /// �L�����̍s���C���X�y�N�^�[
    /// </summary>
    [SerializeField]
    Rigidbody2D rb; // Player rb
    [SerializeField]
    float movementSpeed; // Player �ړ����x
    [SerializeField]
    GameObject bulletPrefab1; // �ʏ�e
    [SerializeField]
    GameObject bulletPrefab2; // �����e CT����
    public float Bullet2Power = 3; 
    public float Bullet1Power = 1;
    /// <summary>
    /// �W�����v�֌W
    /// </summary>
    [SerializeField]
    GroundCheck ground;//�ڐG����
    [SerializeField]
    float gravity;//�d��
    //gravity 3��4 //////

    [SerializeField]
    float jumpSpeed;//�W�����v���鑬�x
    [SerializeField]
    float jumpHeight;//��������
    [SerializeField]
    float jumpLimitTime;//�W�����v��������
    [SerializeField]
    AnimationCurve dashCurve;
    [SerializeField]
    AnimationCurve jumpCurve;
    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;//�W�����v����
    private float dashTime;
    private float beforeKey;
    private bool isGround = false;
    private bool isJump = false;//�W�����v����
    private bool isShootingEnabled = true; // CT�p
    private Vector2 velocity;
    private Vector3 bulletPoint; // �e�̔��˒n�_
    private float lifeTimer = 1.0f; //��ʊO�̎��́Z�b���Ƃ�HP������
    private EnemyControll enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    private PanelManager panelManager;


    //���̃W�����v�����̂��߂̕ϐ�//////
    [SerializeField]
    private float jumpForce = 8000.0f;
    private int jumpCount = 0;
    // Gravity Scale��0��1�ɕύX���Ă��܂�
    

    // Start is called before the first frame update
    void Start()
    {
        bulletPoint = transform.Find("BulletPoint").localPosition;
        lifeManager = FindObjectOfType<LifeManager>();
        panelManager = FindObjectOfType<PanelManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���̃W�����v���� Sasaki//////
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            this.rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }

        //�ڒn����𓾂�
        isGround = ground.IsGround();
        // �ړ�����
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        float verticalKey = Input.GetAxis("Vertical");
        /*
        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //�W�����v�����ʒu���L�^����
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //������L�[�������Ă��邩
            bool pushUpKey = verticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���ĂȂ���
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        */
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1);
            dashTime += Time.deltaTime;
            xSpeed = movementSpeed;
        }
        else if (horizontalKey < 0)
        {
            
            dashTime += Time.deltaTime;
            xSpeed = -movementSpeed;
        }
        else
        {
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }
        //�O��̓��͂���_�b�V���̔��]�𔻒f���đ��x��ς���
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        //�A�j���[�V�����J�[�u�𑬓x�ɓK�p New
        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        rb.velocity = new Vector2(xSpeed, ySpeed);
        // �e���ˏ���
        if (Input.GetMouseButtonDown(0) /*&& panelManager.panelFlag == false*/)
        {
            Fire1();
        }
        if (Input.GetMouseButtonDown(1) && isShootingEnabled)
        {
            Instantiate(bulletPrefab2, transform.position + bulletPoint, Quaternion.identity);
            StartCoroutine(EnableShooting());
        }

        //��������
        if (transform.position.y <= -8.0f)
        {
            lifeManager.HideHeart();
        }

        //��ʊO����
        if (transform.position.x <= -8.0f)
        {
            lifeTimer -=  Time.deltaTime;
            if (lifeTimer <= 0.0f)
            {
                lifeManager.HideHeart();
                lifeTimer = 1.0f;
            }
        }
        else
        {
            lifeTimer = 1.0f;
        }
        
    }

    void Fire1()
    {
        Instantiate(bulletPrefab1, transform.position + bulletPoint, Quaternion.identity);
    }

    IEnumerator EnableShooting()
    {
        //�������̎ˌ��C���^�[�o��
        isShootingEnabled = false;
        yield return new WaitForSeconds(5.0f);
        isShootingEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //�����蔻��
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
        //if(collider2D.gameObject.tag == "Heart")
        //{
        //    Debug.Log("�G�ꂽ");
        //    lifeManager.HeartRecovery();
        //}
    }

    //���̃W�����v�����̂��ߒǉ�//////
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
    }
}
