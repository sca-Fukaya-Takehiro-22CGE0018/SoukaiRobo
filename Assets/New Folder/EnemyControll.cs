using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public float hp = 0;
    private PlayerControll playerControll;
    // Start is called before the first frame update
    void Start()
    {
       this.playerControll = FindObjectOfType<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp -= playerControll.Bullet1Power;
        }
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
