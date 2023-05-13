using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStage : MonoBehaviour
{
    public GameObject Cube;

    private float timer = 0;
    private float spowntime = 2.0f;// 2秒ごとに生成
    private int Max = 3;// 1/Maxの値 の確率で穴を生成
    private int random = 0;
    private int Height;
    private int high; // 1番高い
    private int low = -7; // 1番低い
    private int dif = 0; // 差
    private int count = 0;
    private int difCount = 0;
    private bool Ground = true;
    [SerializeField] private float y = 0.0f;

    void Start()
    {
        Height = low; //最初の高さ
        high = low + 3;
    }

    void Update()
    {
        timer += Time.deltaTime;
        random = Random.Range(1, Max + 1);
        if ((timer > spowntime && random%Max != 0) || (timer > spowntime && Ground == false) )// 生成する
        {
            StageGenerate();
            count++;
            timer = 0;
            Ground = true;
        }
        else if (timer > spowntime && (random%Max == 0 || count >= 5)&& Ground == true) // 生成しない
        {
            timer = 0;
            Ground = false;
            count = 0;
        }
    }

    void StageGenerate()
    {
        
        dif = Random.Range(-2, 3);
        Height = Height - dif;
        if (Height < low)
        {
            Height = low;
            difCount++;
        }
        if (Height > high)
        {
            Height = high;
        }
        Instantiate(Cube,new Vector3(14, Height, 0),Quaternion.identity);
    }

}
