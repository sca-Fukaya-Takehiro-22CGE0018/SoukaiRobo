﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballMove : MonoBehaviour
{
    private LifeManager lifeManager;
    private GameObject playerObject;
    private GameObject Cannonball;
    Vector3 pPos;
    Vector3 cPos;
    Vector3 direction;
    Rigidbody2D CannonballRigidbody;

    private float speed = 10.0f;
    private float timer = 2.5f;

    [SerializeField]
    GameObject Anim;
    [SerializeField]
    GameObject PlayerHitAnim;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        playerObject = GameObject.FindWithTag("Player");
        Cannonball = GameObject.FindWithTag("Cannonball");
        CannonballRigidbody = Cannonball.GetComponent<Rigidbody2D>();
        pPos = playerObject.transform.position;
        cPos = Cannonball.transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        direction = (pPos - cPos).normalized;
        CannonballRigidbody.velocity = direction * speed;
        if (timer <= 0.0f)
        {
            Destroy(Cannonball);
            timer = 2.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lifeManager.HideHeart();
            Instantiate(PlayerHitAnim, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
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
