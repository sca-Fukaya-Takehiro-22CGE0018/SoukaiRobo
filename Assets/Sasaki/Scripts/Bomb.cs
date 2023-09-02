using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private LifeManager lifeManager;
    [SerializeField]
    GameObject Anim;
    [SerializeField]
    GameObject PlayerHitAnim;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            //ここでダメージ計算
            lifeManager.HideHeart();
            Instantiate(PlayerHitAnim, new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z), Quaternion.identity);
        }
        if (other.gameObject.tag == "Stage")
        {
            Destroy(this.gameObject);
            Instantiate(Anim, transform.position, Quaternion.identity);
        }
    }
}
