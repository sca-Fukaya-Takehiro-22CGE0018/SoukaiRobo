using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStage : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Aerial;
    public GameObject Tank;
    public GameObject Helicopter;

    private float timer = 2.0f;
    private float BossSpawntimer = 0.0f;
    private float spawntime = 0.0f;
    private float tankSpawnTime = 5.0f;
    public float SpawnPositionX;//床などの生成場所のx座標
    private int Max = 3;// 1/Maxの値 の確率で穴を生成
    private int GenerateLimit = 5;//ステージ連続生成の上限
    public float Height;//床の高さ
    private float beforeHeight;//直前の床の高さ
    private float A_Height;//空中床の高さ
    private float high; // 1番高い
    private float low; // 1番低い
    private int dif = 0; // 差
    private int groundMadeCount = 0;
    private int AerialfloorMadeCount = 0;
    private int difCount = 0;
    private int EnemyCount = 0;
    private bool isHallMade = false;//直前に穴が作られた
    private bool makeAerialfloor = false;
    public bool isNormalStage = true;
    private bool isTankBossBattle = false;
    private bool isWallBossBattle = false;
    private bool isHelicopterBossBattle = false;

    int BossCount = 0;
    bool DestroyTank = false;
    bool DestroyWall = false;
    private float y = 0.0f;
    bool debug = false;

    private EnemySpawn enemySpawn;
    private OffScreenAttack offScreenAttack;

    public Vector3 rightTop;
    public Vector3 leftBottom;

    void Start()
    {
        A_Height = low;
        enemySpawn = FindObjectOfType<EnemySpawn>();
        offScreenAttack = FindObjectOfType<OffScreenAttack>();
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        high = leftBottom.y+1.0f;
        low = leftBottom.y-2.0f;
        Height = low; //最初の高さ
        SpawnPositionX = rightTop.x + 5.0f;
    }

    void Update()
    {
        Debug.Log(rightTop);
        Debug.Log(leftBottom);
        timer -= Time.deltaTime;

        if(timer < spawntime)
        {
            StageGenerate();
        }
    }

    bool CanGenerateStage()
    {
        //debugのときはstageを平らにする
        if (debug) return true;

        //ボス戦(戦車)だったらボス専用のステージを作る
        if(isTankBossBattle) return true;

        //ランダム数（0～2）を作成
        int G_random = Random.Range(0, Max);

        //空中床が生成されなかったら必ず床を生成
        if(isHallMade) return true;

        //1/3の確率で床は作られない
        if(G_random == 0) return false;

        //連続で5回までしか床は作られない
        if (groundMadeCount >= GenerateLimit) return false;

        return true;
    }


    void StageGenerate()
    {
        //生成しない
        if (!CanGenerateStage())
        {
            //壁戦のとき(デバッグ用)
            if (isWallBossBattle)
            {
                BossCount++;
                if (BossCount >= 4)
                {
                    DestroyWall = true;
                    Debug.Log("ボスがやられた");
                    isWallBossBattle = false;
                    GenerateLimit = 5;
                    BossCount = 0;
                }
            }

            //壁戦のとき
            if (isWallBossBattle)
            {
                //必ず空中床を2個生成
                AerialFloorGenerate();
                groundMadeCount = 0;
                return;
            }

            //通常時・戦闘ヘリのとき
            if (isNormalStage || isHelicopterBossBattle)
            {
                isHallMade = true;//穴ができたことになる
                groundMadeCount = 0;//地面連続生成カウントを０に
            }

            //空中床を生成するか
            int A_random = Random.Range(0,2);

            //4回連続で作られなかったら必ず空中床を生成
            if (AerialfloorMadeCount >= 4)
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

            timer = 2.0f;
            ++AerialfloorMadeCount;
            return ;
        }

        //生成する
        //戦車戦のとき
        if (isTankBossBattle || debug)
        {
            Instantiate(Cube, new Vector3(SpawnPositionX, leftBottom.y-2.0f, 0), Quaternion.identity);//ステージの高さを一定にして生成
            timer = 2.0f;
            return;
        }

        //壁戦のとき
        if (isWallBossBattle)
        {
            //AerialFloorGenerate()で処理
        }

        //通常時・ヘリ戦のとき
        if (isNormalStage || isHelicopterBossBattle)
        {
            timer = 2.0f;
            dif = Random.Range(-2, 3);
            beforeHeight = Height;
            Height = Height - dif;
            if (Height < low)//ステージの高さが最低より低くなる時
            {
                Height = low;
                difCount++;
            }
            if (Height > high)//ステージの高さが最大より高くなる時
            {
                Height = high;
            }
            Instantiate(Cube, new Vector3(SpawnPositionX, Height, 0), Quaternion.identity);
        }

        //敵出現の調整
        if (EnemyCount == 2)
        {
            if (beforeHeight <= Height)
            {
                enemySpawn.SpawnEnemy();
                EnemyCount = 0;
            }
        }
        ++EnemyCount;

        A_Height = Height;
        isHallMade = false;//地面ができた
        ++groundMadeCount;//一個作ったらカウントする
    }

    void AerialFloorGenerate() //空中床の生成
    {
        //戦車戦のとき
        if (isTankBossBattle)
        {
            //落とし穴、空中床は生成されない
        }

        //壁戦のとき
        if (isWallBossBattle)
        {
            //壁戦専用
            timer = 6.0f;
            float Adif = Random.Range(-1.0f, 1.0f);
            Instantiate(Aerial, new Vector3(SpawnPositionX+2.0f, leftBottom.y+2.0f, 0), Quaternion.identity);
            Instantiate(Aerial, new Vector3(SpawnPositionX+6.0f, leftBottom.y+2.0f + Adif,0), Quaternion.identity);

            isHallMade = true;
            AerialfloorMadeCount = 0;
            return;
        }

        //通常時・戦闘ヘリのとき
        if (isNormalStage || isHelicopterBossBattle)
        {
            //直前の床の高さより少し高い高さで生成
            Instantiate(Aerial, new Vector3(SpawnPositionX+2.0f, leftBottom.y+2.0f, 0), Quaternion.identity);
        }

        //最後に穴ができるから
        isHallMade = true;
        AerialfloorMadeCount = 0;
    }

    //通常時
    public void NormalStage()
    {
        //bossを倒したら
        //Boss戦のboolをすべてfalse
        //isNormalStageをtrueに戻す
        isTankBossBattle = false;
        isWallBossBattle = false;
        isHelicopterBossBattle = false;
        isNormalStage = true;
        offScreenAttack.SwitchCount();
        //OSAttack.SetActive(true);
    }

    //戦車戦
    public void TankBattle()
    {
        offScreenAttack.SwitchCount();
        isTankBossBattle = true;
        isNormalStage =false;
        Invoke(nameof(TankSpawn),8.0f);
    }

    //戦車出現
    private void TankSpawn()
    {
        Instantiate(Tank, new Vector3(SpawnPositionX+2.0f, rightTop.y-2.5f, 0), Quaternion.identity);
    }

    //壁戦
    public void WallBattle()
    {
        //OSAttack.SetActive(false);
        offScreenAttack.SwitchCount();
        isWallBossBattle = true;
        isNormalStage = false;
        Invoke(nameof(WallSpawn),5.0f);
    }

    //壁出現
    private void WallSpawn()
    {
        //Instantiate(Wall, new Vector3(SpawnPositionX, 0,0),Quaternion.identity);
    }

    //ヘリ戦
    public void HelicopterBattle()
    {
        high = leftBottom.y-1.0f;
        //OSAttack.SetActive(false);
        offScreenAttack.SwitchCount();
        isHelicopterBossBattle = true;
        isNormalStage = false;
        Invoke(nameof(HelicopterSpawn),5.0f);
    }

    //ヘリ出現
    private void HelicopterSpawn()
    {
        Instantiate(Helicopter, new Vector3(SpawnPositionX, rightTop.y-2.5f,0),Quaternion.identity);
    }
}
