using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //ゲームシーンでスコア計算
    public static int EnemyScore;
    public static int TracEnemyScore;
    public static int BossScore;
    public static int ItemScore;
    public static int TimeScore;
    public static int TotalScore;
    private int addEnemyScore = 15;
    private int addTracEnemyScore = 15;
    private int addBossScore = 300;
    private int addItemScore = 10;
    private int addTimeScore = 10;

    private CountDownTimer countDownTimer;
    // Start is called before the first frame update
    void Start()
    {
        countDownTimer = FindObjectOfType<CountDownTimer>();
        EnemyScore = 0;
        TracEnemyScore = 0;
        BossScore = 0;
        ItemScore = 0;
        TimeScore = 0;
        TotalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyScoreAdd()
    {
        EnemyScore += addEnemyScore;
    }

    public void TracEnemyScoreAdd()
    {
        TracEnemyScore += addTracEnemyScore;
    }

    public void BossScoreAdd()
    {
       BossScore += addBossScore;
    }

    public void ItemScoreAdd()
    {
        ItemScore += addItemScore;
    }

    public void TimeScoreCalculation()
    {
        TimeScore = (int)countDownTimer.CountDownSeconds * addTimeScore;
    }

    public static void EnemyScoreCalculation()
    {
        EnemyScore += TracEnemyScore;
    }

    public static void TotalScoreCalculation()
    {
        TotalScore = EnemyScore+BossScore+ItemScore+TimeScore;
    }
}
