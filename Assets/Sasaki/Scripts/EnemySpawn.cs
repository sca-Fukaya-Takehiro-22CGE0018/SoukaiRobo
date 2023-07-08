﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    private AutoStage autoStage;
    // Start is called before the first frame update
    void Start()
    {
        autoStage = FindObjectOfType<AutoStage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Instantiate(Enemy,new Vector3(15,autoStage.Height+5,0),Quaternion.identity);
        Debug.Log("敵出現");
    }
}
