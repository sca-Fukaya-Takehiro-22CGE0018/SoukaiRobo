using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
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
        }
        if (other.gameObject.tag == "Stage")
        {
            Destroy(this.gameObject);
        }
    }
}
