using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    #region//インスペクター
    [Header("移動速度")]public float speed;
    [Header("重力")]public float gravity;
    [Header("体力")]public float hp;
    #endregion

    #region//プライベート変数
    private float backTime = 0.01f;
    private float time = 0;
    private Rigidbody2D rb = null;
    private bool rightTleftF = false;
    private bool HitDamage = false;
    #endregion

    #region//スクリプト参照
    private PlayerControll playerControll;
    private Head headCollider;
    #endregion

    public float HP
    {
        get { return this.hp;}
        set { this.hp = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.playerControll = FindObjectOfType<PlayerControll>();
        this.headCollider = FindObjectOfType<Head>();
    }

    // Update is called once per frame
    void Update()
    {/* エラーが出ていたのでコメントアウトしてあります
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,transform);
        // ヒットしたオブジェクトの処理
        if (hit.collider != null)
        {
            // レイキャストが何かしらのオブジェクトにヒットした場合の処理
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }
        else
        {
            // レイキャストが何にもヒットしなかった場合の処理
            Debug.Log("No hit");
        }
        if (headCollider.HeadDamage == false)
        {
            Debug.Log("a");
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            time += Time.deltaTime;
            if (backTime <= time)
            {
                headCollider.HeadDamage= false;
                rb.AddForce(-transform.forward);
                transform.Translate(speed*0.5f, 0, 0);
                time = 0;
            }
            Debug.Log("下がる");
        }*/
    }

    private void FixedUpdate()
    {
        
       
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp - playerControll.Bullet1Power;
        }
    }
}
