using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject playerObject;
    private LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //当たり判定
        if(collider2D.gameObject.tag == "Player")
        {
            //playerに当たったら消える
            Destroy(this.gameObject);
            lifeManager.HideHeart();
        }
        if(collider2D.gameObject.tag == "Bullet1" || collider2D.gameObject.tag == "Bullet2")
        {
            //playerの弾に当たったら消える
            Destroy(this.gameObject);
            GameObject obj1 = GameObject.Find("Bullet1");
            Destroy(obj1);
            GameObject obj2 = GameObject.Find("Bullet2");
            Destroy(obj2);
        }
    }
}

