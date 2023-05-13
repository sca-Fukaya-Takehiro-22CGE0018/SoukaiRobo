using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialFloorSpawn : MonoBehaviour
{
    public GameObject Aerial;

    private float timer = 0;
    private float spowntime = 10.0f;// 10秒ごとに生成
    private int Max = 5;// 1/Maxの値 の確率で穴を生成
    private int random = 0;
    private int count = 0;
    private bool Ground = true;


    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        random = Random.Range(1, Max + 1);
        if ((timer > spowntime && random % Max != 0) || (timer > spowntime && Ground == false) || count == 10)// 生成する
        {
            //StageGenerate();
            timer = 0;
            Ground = true;
            count = 0;
        }
        else if (timer > spowntime && random % Max == 0 && Ground == true)// 生成しない
        {
            timer = 0;
            Ground = false;
            count++;
        }
    }

    void StageGenerate()
    {
        Instantiate(Aerial, new Vector3(10, -2, 0), Quaternion.identity);
    }

}
