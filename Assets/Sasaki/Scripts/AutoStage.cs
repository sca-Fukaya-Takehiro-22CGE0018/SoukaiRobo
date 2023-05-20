using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStage : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Aerial;

    private float timer = 0;
    private float spawntime = 2.0f;// 2秒ごとに生成
    private int Max = 3;// 1/Maxの値 の確率で穴を生成
    private int Height;
    private int high; // 1番高い
    private int low = -7; // 1番低い
    private int dif = 0; // 差
    private int groundMadeCount = 0;
    private int AerialCount = 0; //空中床が連続で作られる数
    private int A_GenerateCount = 0;//空中床が作られる確率の上限
    private int difCount = 0;
    private bool isHallMade = false;
    private bool isAerialFloorMade = false;
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
            AerialFloorGenerate();
            timer = 0.0f;
        }
    }

    bool CanGenerateStage()
    {
        //ランダム数（0～2）を作成
        int G_random = Random.Range(0, Max);

        //直前に穴が作られているなら絶対すぐ作る
        //if (isHallMade) return true;

        //直前に空中床があったら必ず穴を生成
        if(isAerialFloorMade) return false;

        //空中床ができたら必ず穴を作る
        if (isAerialFloorMade) return false;

        //直前に穴があり空中床を生成
        if(isHallMade)
        if(G_random == 0)
            {
                AerialFloorGenerate();//空中床を作る
                return false;
            }
        else return true;//空中床がない場合、必ず床を生成

        //1/3の確率で床は作られない
        if(G_random == 0) return false;
        Debug.Log("ランダム数は通過");

        //連続で5回までしか床は作られない
        if (groundMadeCount >= 5) return false;
        Debug.Log("5回は作ってない");

        return true;
    }

    bool CanAerialFloorGenerate()
    {
        //直前に穴があるか
        if (!isHallMade) return false;

        //直前に空中床があるか
        if (isAerialFloorMade) return false;

        //5回連続で生成されなかったら必ず生成する
        if (A_GenerateCount >= 5) return true;

        //連続で2回までしか作られない
        if (AerialCount >= 2) return false;

        //1/5で一つ作る
        int A_random = Random.Range(0, 5);
        if(A_random != 0) return false;


        return true;
    }

    void StageGenerate()
    {
        //生成しない
        if (!CanGenerateStage())
        {
            isHallMade = true;//穴ができたことになる
            groundMadeCount = 0;//地面連続生成カウントを０に
            isAerialFloorMade = false;
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
        AerialCount = 0;
    }

    void AerialFloorGenerate()
    {
        //生成しない
        if (!CanAerialFloorGenerate())
        {
            isAerialFloorMade = false;
            ++A_GenerateCount;
            return;
        }

        //空中床のscale,座標の設定
        Instantiate(Aerial,new Vector3(14,-2,0) , Quaternion.identity);
        A_GenerateCount = 0;
        ++AerialCount;
        isAerialFloorMade = true;//空中床ができたことになる

    }
}
