﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject playerObject;
    private LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        //playerObject = GameObject.FindWithTag("Player");
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
            lifeManager.HideHeart();
            //playerに当たったら消える
            Destroy(this.gameObject);
        }
        if(collider2D.gameObject.tag == "Bullet1")
        {
            Destroy(collider2D.gameObject);
            //playerの弾に当たったら消える
            Destroy(this.gameObject);
        }
        if (collider2D.gameObject.tag == "Bullet2")
        {
            Destroy(this.gameObject);
        }
    }
}

