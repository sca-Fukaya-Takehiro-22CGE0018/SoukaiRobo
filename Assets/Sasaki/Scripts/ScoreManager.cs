using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField,Header("敵撃破テキスト")]
    Text DefeatText;
    [SerializeField, Header("Bossテキスト")]
    Text BossText;
    [SerializeField, Header("アイテムテキスト")]
    Text ItemText;
    [SerializeField, Header("クリア時間ボーナステキスト")]
    Text TimeBonusText;
    [SerializeField, Header("トータルスコアテキスト")]
    Text TotalText;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DefeatText.text = gameManager.DefeatScore.ToString();
        BossText.text = gameManager.BossScore.ToString();
        ItemText.text = gameManager.ItemBonus.ToString();
        TimeBonusText.text = gameManager.TimeBonus.ToString();
        TotalText.text = gameManager.Total.ToString();
    }
}
