using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    GameObject Player;
    GameObject Missile;
    private float speed = 2.0f;
    private bool canScaleChange = false;

    Vector3 toDirection;

    private LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Missile = GameObject.FindGameObjectWithTag("Missile");
        lifeManager = FindObjectOfType<LifeManager>();
        transform.localScale = new Vector3(0.04f,0.04f,1.0f);
        StartCoroutine(ScaleChange());
    }

    private IEnumerator ScaleChange()
    {
        yield return new WaitForSeconds(1.0f);
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
            transform.localScale += new Vector3(0.06f, 0.06f, 0.0f) * Time.deltaTime;
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
            lifeManager.HideHeart();
        }
        if (other.gameObject.tag == "Bullet1")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Bullet2")
        {
            Destroy(this.gameObject);
        }
    }
}
