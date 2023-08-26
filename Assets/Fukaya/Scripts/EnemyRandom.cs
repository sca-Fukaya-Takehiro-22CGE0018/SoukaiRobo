using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;    // 生成オブジェクト
    [SerializeField] Transform pos;                 // 生成位置
    [SerializeField] Transform pos2;                // 生成位置
    float minX, maxX, minY, maxY;                   // 生成範囲

    float spawn = 0;
    [SerializeField] int generate = 4;        // 生成する間隔

    private AutoStage autoStage;

    void Start()
    {
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
        autoStage = FindObjectOfType<AutoStage>();
    }

    void Update()
    {
        spawn += Time.deltaTime;

        if (spawn > generate)
        {
            spawn = 0;

            // ランダムで種類と位置を決める
            int index = Random.Range(0, enemyList.Count);
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            Instantiate(enemyList[index], new Vector3(autoStage.rightTop.x+2.0f, autoStage.rightTop.y, 0), Quaternion.identity);
        }
    }
}