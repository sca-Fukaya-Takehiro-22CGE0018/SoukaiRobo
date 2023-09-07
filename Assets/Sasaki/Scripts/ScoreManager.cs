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
    public int addBonusScore;
    private int BossBonus;
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
        addBonusScore = 0;
        BossBonus = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BonusReset()
    {
        addBonusScore = 0;
    }

    public void EnemyScoreAdd()
    {
        EnemyScore += addEnemyScore;
        EnemyScore += addBonusScore;
        addBonusScore += 10;
    }

    public void TracEnemyScoreAdd()
    {
        TracEnemyScore += addTracEnemyScore;
        TracEnemyScore += addBonusScore;
        addBonusScore += 10;
    }

    public void BossScoreAdd()
    {
        BossBonus = addBonusScore/10;
        BossScore += addBossScore*BossBonus;
        addBonusScore = addBonusScore * 2;
        BossBonus = 0;
    }

    public void ItemScoreAdd()
    {
        ItemScore += addItemScore;
        ItemScore += addBonusScore;
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
