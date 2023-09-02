using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;    // 生成オブジェクト

    float spawn = 0;
    float generate = 2.5f;        // 生成する間隔

    private AutoStage autoStage;

    void Start()
    {
        autoStage = FindObjectOfType<AutoStage>();
    }

    void Update()
    {
        if (autoStage.isNormalStage)
        {
            spawn += Time.deltaTime;
        }

        if (spawn > generate)
        {
            float SpawnPosition = Random.Range(0.0f,3.0f);
            spawn = 0;

            // ランダムで種類と位置を決める
            int index = Random.Range(0, enemyList.Count);

            Instantiate(enemyList[index], new Vector3(autoStage.rightTop.x+2.0f, autoStage.rightTop.y-SpawnPosition, 0), Quaternion.identity);
        }
    }
}