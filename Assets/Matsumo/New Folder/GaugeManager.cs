using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    RectTransform rt;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        this.gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float scaleX = gameManager.EnemyDefeat/(float)gameManager.MaxEnemy;
        rt.localScale = new Vector2(scaleX,1);
    }
}
