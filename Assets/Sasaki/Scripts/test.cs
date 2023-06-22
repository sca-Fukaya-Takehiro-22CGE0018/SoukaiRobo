using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public float hp = 0;
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    private Vector3 EnemyPosition;
    private PlayerControll playerControll;
    private PanelManager panelManager;
    private GameManager gameManager;
    private float timer = 2.0f;
    
    [SerializeField] private GameObject Heart;
    // Start is called before the first frame update
    void Start()
    {
        /*
        this.playerControll = FindObjectOfType<PlayerControll>();
        this.panelManager = FindObjectOfType<PanelManager>();
        this.gameManager = FindObjectOfType<GameManager>();
        playerObject = GameObject.FindWithTag("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        */
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            int random = Random.Range(0, 5);
            if (random == 0)
            {
                Instantiate(Heart,
                    new Vector3(this.transform.position.x-0.5f, this.transform.position.y-0.5f, this.transform.position.z),
                    Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        /*
        if (panelManager.panelFlag == false)
        {
            PlayerPosition = playerObject.transform.position;
            EnemyPosition = transform.position;
            EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * 0.001f;
            EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * 0.001f;
            transform.position = EnemyPosition;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        /*
        if (collider2D.gameObject.tag == "Bullet1")
        {
            hp = hp -= playerControll.Bullet1Power;
        }
        if (collider2D.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        */
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
