using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    //�_���[�W1
    [SerializeField]
    private Rigidbody2D rb;// Player rb
    [SerializeField]
    float jumpForce;//Player �W�����v��  
    [SerializeField]
    float STEP;// Player �ړ����x
    [SerializeField]
    GameObject BulletPrefab1; //�ʏ�e
    [SerializeField]
    GameObject BulletPrefab2; //�����e CT����

    public float Bullet1Power = 1;
    private bool jumpflag = false; //���n����
    private bool isShoot = true; //CT�p

    private Vector2 velocity;
    private Vector3 BulletPoint; //�e�̔��˒n�_
    // Start is called before the first frame update
    void Start()
    {
        BulletPoint = transform.Find("BulletPoint").localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(STEP * Time.deltaTime, 0,0);
        if(Input.GetKeyDown(KeyCode.Space) && !jumpflag && !(rb.velocity.y < -0.5f))
        {
            Jump();
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * STEP, rb.velocity.y);
        if (Input.GetMouseButtonDown(0))
        {
            Fire1();
        }
        if (Input.GetMouseButtonDown(1) && isShoot)
        {
            Instantiate(BulletPrefab2, transform.position + BulletPoint, Quaternion.identity);
            StartCoroutine("FireBullet");
        }
    }

    IEnumerator FireBullet()
    {
        isShoot = false;

        yield return new WaitForSeconds(5.0f);

        isShoot = true;
    }

    void Fire1()
    {
        Instantiate(BulletPrefab1, transform.position + BulletPoint, Quaternion.identity);
    }
    

    void Jump()
    {
        jumpflag = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            jumpflag = false;
        }
        if (collision.CompareTag("Enemy"))
        {

        }
    }
}
