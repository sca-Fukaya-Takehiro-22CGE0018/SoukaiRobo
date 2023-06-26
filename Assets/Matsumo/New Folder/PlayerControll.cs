using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb; // Player rb
    [SerializeField]
    private Collider2D jumpCollider;
    [SerializeField]
    private float movementSpeed; // Player �ړ����x
    [SerializeField]
    private float jumpForce = 5f; // �W�����v��
    [SerializeField]
    private float jumpInterval = 0.5f; // �W�����v�̊Ԋu
    [SerializeField]
    private GameObject bulletPrefab1; // �ʏ�e
    [SerializeField]
    private GameObject bulletPrefab2; // �����e CT����
    public float Bullet1Power = 1;

    private bool isJumping = true; // �W�����v���̃t���O
    private bool isFalling = false; // ���~���̃t���O
    private bool isGrounded = false; // �n�ʂɐڒn���Ă��邩�̃t���O
    private bool isShootingEnabled = true; // CT�p

    private Vector2 velocity;
    private Vector3 bulletPoint; // �e�̔��˒n�_
    private float jumpTimer = 0f; // �W�����v�^�C�}�[
    private float lifeTimer = 1.0f; //��ʊO�̎��́Z�b���Ƃ�HP������

    private EnemyControll enemyControll;
    private TracEnemy tracEnemy;
    private LifeManager lifeManager;
    private PanelManager panelManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletPoint = transform.Find("BulletPoint").localPosition;
        lifeManager = FindObjectOfType<LifeManager>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // �W�����v�^�C�}�[���X�V
        jumpTimer += Time.deltaTime;

        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isJumping && panelManager.panelFlag == false)
        {
            Jump();
        }

        // �W�����v�^�C�}�[���w��̊Ԋu�𒴂�����W�����v�\�ɂ���
        if (jumpTimer >= jumpInterval)
        {
            isJumping = true;
        }
        // �ړ�����
        float movementInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementInput * movementSpeed, rb.velocity.y);

        // �e���ˏ���
        if (Input.GetMouseButtonDown(0) && panelManager.panelFlag == false)
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

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = false;
        jumpTimer = 0f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�X�e�[�W�Ƃ̐ڐG����
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = true;
            isFalling = false;
        }
        if (collision.gameObject.tag == "Heart")
        {
            Debug.Log("�G�ꂽ");
            lifeManager.HeartRecovery();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ՓˏI����
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = false;
            isFalling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //�����蔻��
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
    }
}
