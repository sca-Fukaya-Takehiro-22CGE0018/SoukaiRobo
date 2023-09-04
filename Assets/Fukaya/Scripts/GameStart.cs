using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationController : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        gameManager.DefeatScore = 0;
        gameManager.BossScore = 0;
        gameManager.TimeBonus = 0;
        gameManager.ItemBonus = 0;
        gameManager.Total = 0;
    }
}
