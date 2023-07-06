using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    GameObject Player;
    GameObject Missile;
    private float speed = 2.0f;

    Vector3 toDirection;

    private LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Missile = GameObject.FindGameObjectWithTag("Missile");
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        this.transform.position = Vector2.MoveTowards(this.transform.position,new Vector2(Player.transform.position.x,Player.transform.position.y), speed * Time.deltaTime);

        //向きの変更
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //当たり判定
        if (other.gameObject.tag == "Player")
        {
            lifeManager.HideHeart();
            Destroy(this.gameObject);
        }
    }
}
