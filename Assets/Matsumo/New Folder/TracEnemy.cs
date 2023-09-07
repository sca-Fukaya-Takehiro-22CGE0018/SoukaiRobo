using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TracEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject Anim;

    public float hp = 1;//体力
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    private Vector3 EnemyPosition;
    private PlayerControll playerControll;
    private PanelManager panelManager;
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private Animator anim;

    [SerializeField] private GameObject Heart;
    // Start is called before the first frame update
    void Start()
    {
        this.playerControll = FindObjectOfType<PlayerControll>();
        this.panelManager = FindObjectOfType<PanelManager>();
        this.gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerObject = GameObject.FindWithTag("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //フラグがfalseの時は動く
        if(panelManager.panelFlag == false)
        {
            PlayerPosition = playerObject.transform.position;
            EnemyPosition = transform.position;
            EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * 0.5f * Time.deltaTime;
            EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * 0.5f * Time.deltaTime;
            transform.position = EnemyPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //当たり判定
        if (collider2D.gameObject.tag == "Bullet1")
        {
            Destroy(collider2D.gameObject);
            hp = hp -= playerControll.Bullet1Power;
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
        if(collider2D.gameObject.tag == "Bullet2")
        {
            hp = hp -= playerControll.Bullet2Power;
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
        if (collider2D.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
        if (hp <= 0)
        {
            Instantiate(Anim, transform.position, Quaternion.identity);
            scoreManager.TracEnemyScoreAdd();

            gameManager.EnemyDefeat++;
            //確率でアイテムドロップ
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Instantiate(Heart,
                    new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.5f, this.transform.position.z),
                    Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
