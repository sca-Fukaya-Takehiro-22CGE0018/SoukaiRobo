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
    private float jumpForce; // Player �W�����v��  
    [SerializeField]
    private float movementSpeed; // Player �ړ����x
    [SerializeField]
    private float jumpTime; // �W�����v�̒���
    [SerializeField]
    private float jumpDelay; // �W�����v�̊Ԋu
    [SerializeField]
    private GameObject bulletPrefab1; // �ʏ�e
    [SerializeField]
    private GameObject bulletPrefab2; // �����e CT����
    public float Bullet1Power = 1;

    private bool isJumping = false; // �W�����v���̃t���O
    private bool isGrounded = false; // �n�ʂɐڒn���Ă��邩�̃t���O
    private bool isShootingEnabled = true; // CT�p

    private Vector2 velocity;
    private Vector3 bulletPoint; // �e�̔��˒n�_

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
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping && panelManager.panelFlag == false)
        {
            StartCoroutine(Jump());
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
    }

    IEnumerator Jump()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(jumpTime);

        rb.AddForce(Vector2.down * (jumpForce*0.5f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpDelay);

        isJumping = false;
    }

    void Fire1()
    {
        Instantiate(bulletPrefab1, transform.position + bulletPoint, Quaternion.identity);
    }

    IEnumerator EnableShooting()
    {
        isShootingEnabled = false;
        yield return new WaitForSeconds(5.0f);
        isShootingEnabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyBullet" || collider2D.gameObject.tag == "TracEnemy")
        {
            lifeManager.HideHeart();
        }
    }
}
