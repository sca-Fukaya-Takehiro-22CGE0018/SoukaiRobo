using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    bool headDamage = false;
    private BossMove bossMove;
    private PlayerControll playerControll;
    public bool HeadDamage
    {
        get {return this.headDamage; }
        set { this.headDamage = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
       this.bossMove = FindObjectOfType<BossMove>();
       this.playerControll = FindObjectOfType<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Bullet1")
        {
            bossMove.HP = bossMove.HP - playerControll.Bullet1Power;
            headDamage = true;
            //rb.position = new Vector2(1 * Speed, -gravity);
            //rb.position = new Vector2(1,-gravity);
        }
    }
}
