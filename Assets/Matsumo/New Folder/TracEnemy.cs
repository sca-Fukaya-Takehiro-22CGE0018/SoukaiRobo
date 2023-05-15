using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracEnemy : MonoBehaviour
{
    public float hp = 0;
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    private Vector3 EnemyPosition;
    private PlayerControll playerControll;
    // Start is called before the first frame update
    void Start()
    {
        this.playerControll = FindObjectOfType<PlayerControll>();
        playerObject = GameObject.FindWithTag("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;

        EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * 0.01f;
        EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * 0.01f;
        transform.position = EnemyPosition;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
