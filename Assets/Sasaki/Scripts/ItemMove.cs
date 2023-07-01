﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private GameObject Enemy;
    private float speed = 2.0f;
    private float onStageSpeed = 3.0f;

    private Vector3 pos;
    private float a = -1.0f;
    private float difX = 0.0f;
    private float difY = 0.0f;
    private bool onStage = false;

    private LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        pos.x = this.transform.position.x+1.0f;
        pos.y = this.transform.position.y;
        difX = this.transform.position.x;//アイテムの最初のx座標
        difY = this.transform.position.y;
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onStage)//地面に接しているとき
        {
            transform.position -= new Vector3(onStageSpeed * Time.deltaTime, 0, 0);
            return;
        }

        pos.x -= speed * Time.deltaTime;//x軸方向に進む速度
        pos.y = (a * Mathf.Pow(pos.x-difX, 2)+difY) * 2.5f;

        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Stage")
        {
            onStage = true;
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            lifeManager.HeartRecovery();
        }
    }
}
