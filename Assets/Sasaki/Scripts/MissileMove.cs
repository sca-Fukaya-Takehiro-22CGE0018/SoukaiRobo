using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    GameObject Player;
    GameObject Missile;
    private float speed = 6.0f;
    private bool canScaleChange = false;

    Vector3 toDirection;

    private LifeManager lifeManager;

    [SerializeField]
    GameObject Anim;
    [SerializeField]
    GameObject PlayerHitAnim;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Missile = GameObject.FindGameObjectWithTag("Missile");
        lifeManager = FindObjectOfType<LifeManager>();
        transform.localScale = new Vector3(0.04f,0.04f,1.0f);
        StartCoroutine(ScaleChange());
        anim = GetComponent<Animator>();
    }

    private IEnumerator ScaleChange()
    {
        yield return new WaitForSeconds(0.5f);
        canScaleChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        this.transform.position = Vector2.MoveTowards(this.transform.position,new Vector2(Player.transform.position.x,Player.transform.position.y), speed * Time.deltaTime);

        //徐々に拡大
        if (canScaleChange)
        {
            transform.localScale += new Vector3(0.12f, 0.12f, 0.12f) * Time.deltaTime;
        }
        if (transform.localScale.x >= 0.2f)
        {
            canScaleChange = false;
        }

        //向きの変更
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //当たり判定
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Instantiate(PlayerHitAnim, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            lifeManager.HideHeart();
        }
        if (other.gameObject.tag == "Bullet1")
        {
            Destroy(other.gameObject);
            Instantiate(Anim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Bullet2")
        {
            Destroy(this.gameObject);
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
    }
}
