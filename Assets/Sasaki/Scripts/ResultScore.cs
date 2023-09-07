using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    //結果画面
    [SerializeField] private Text EnemyScore;
    [SerializeField] private Text BossScore;
    [SerializeField] private Text ItemScore;
    [SerializeField] private Text TimeScore;
    [SerializeField] private Text TotalScore;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.EnemyScoreCalculation();
        ScoreManager.TotalScoreCalculation();
        EnemyScore.text = ScoreManager.EnemyScore.ToString("0000");
        BossScore.text = ScoreManager.BossScore.ToString("0000");
        ItemScore.text = ScoreManager.ItemScore.ToString("0000");
        TimeScore.text = ScoreManager.TimeScore.ToString("0000");
        TotalScore.text = ScoreManager.TotalScore.ToString("0000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
