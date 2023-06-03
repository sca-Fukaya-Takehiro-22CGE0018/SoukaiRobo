using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int defeat = 0;
    private int maxEnemy = 2;
    [SerializeField]
    Slider slider;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public int EnemyDefeat
    {
        set
        {
            defeat = value;

            float progress = (float)defeat / maxEnemy;
            slider.value = progress;
        }

        get { return defeat; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
