using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStage : MonoBehaviour
{
    public GameObject Cube;

    private float timer = 0;
    private float spawntime = 2.0f;// 2秒ごとに生成
    private int Max = 3;// 1/Maxの値 の確率で穴を生成
    private int Height;
    private int high; // 1番高い
    private int low = -7; // 1番低い
    private int dif = 0; // 差
    private int groundMadeCount = 0;
    private int difCount = 0;
    private bool isHallMade = false;
    [SerializeField] private float y = 0.0f;

    void Start()
    {
        Height = low; //最初の高さ
        high = low + 3;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawntime)
        {
            StageGenerate();
            timer = 0.0f;
        }
    }

    bool CanGenerateStage()
    {
        //直前に穴が作られているなら絶対すぐ作る
        if (isHallMade) return true;

        //ランダム数（0～2）を作成
        int random = Random.Range(0, Max);
        //1/3の確率で床は作られない
        if(random == 0) return false;
        Debug.Log("ランダム数は通過");

        //連続で5回までしか床は作られない
        if (groundMadeCount >= 5) return false;
        Debug.Log("5回は作ってない");

        return true;
    }

    void StageGenerate()
    {
        //生成しない
        if (!CanGenerateStage())
        {
            isHallMade = true;//穴ができたことになる
            groundMadeCount = 0;//地面連続生成カウントを０に
            return ;
        }
        
        //生成
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

        isHallMade = false;//地面ができた
        ++groundMadeCount;//一個作ったらカウントする
    }

}
