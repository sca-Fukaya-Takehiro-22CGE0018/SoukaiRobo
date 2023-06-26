using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    Image DefeatGauge;

    
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        float fillAmount = (float)gameManager.EnemyDefeat/gameManager.MaxEnemy;
        DefeatGauge.fillAmount = fillAmount;
    }
}
