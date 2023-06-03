using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    private GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
        slider.value = gameManager.EnemyDefeat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
