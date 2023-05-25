using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStage : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Aerial;

    private float timer = 2.0f;
    //private float spawntime = 2.0f;// 2秒ごとに生成
    private float spawntime = 0.0f;
    private int Max = 3;// 1/Maxの値 の確率で穴を生成
    private int Height;//床の高さ
    private int A_Height;//空中床の高さ
    private int high; // 1番高い
    private int low = -7; // 1番低い
    private int dif = 0; // 差
    private int groundMadeCount = 0;
    private int aerialfloorMadeCount = 0;
    private int difCount = 0;
    private bool isHallMade = false;//直前に穴が作られた
    private bool makeAerialfloor = false;
    [SerializeField] private float y = 0.0f;

    void Start()
    {
        Height = low; //最初の高さ
        high = low + 3;
        A_Height = low;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < spawntime)
        {
            StageGenerate();
        }
    }

    bool CanGenerateStage()
    {
        //ランダム数（0～2）を作成
        int G_random = Random.Range(0, Max);

        //空中床が生成されなかったら必ず床を生成
        if(isHallMade) return true;

        //1/3の確率で床は作られない
        if(G_random == 0) return false;
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

            //空中床を生成するか
            int A_random = Random.Range(0,2);

            //4回連続で作られなかったら必ず空中床を生成
            if (aerialfloorMadeCount >= 4)
            {
                Debug.Log("空中床を生成");
                AerialFloorGenerate();
                timer = 4.0f;
                return;
            }

            //1/2の確立で空中床を生成する
            if(A_random == 0)
            {
                Debug.Log("空中床を生成");
                AerialFloorGenerate();
                timer = 4.0f;
                return;
            }

            //生成しないとき
            timer = 2.0f;
            ++aerialfloorMadeCount;
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
        A_Height = Height;

        isHallMade = false;//地面ができた
        ++groundMadeCount;//一個作ったらカウントする

        timer = 2.0f;
    }

    void AerialFloorGenerate()
    {
        Debug.Log(Height);
        //空中床を生成する設定

        //直前の床の高さより少し高い高さで生成
        Instantiate(Aerial,new Vector3(16,A_Height+4,0) , Quaternion.identity);

        //最後に穴ができるから
        isHallMade = true;
        aerialfloorMadeCount = 0;
    }
}
