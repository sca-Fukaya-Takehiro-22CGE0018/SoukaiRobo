using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    public void GameScene()
    {
        gameManager.EnemyDefeat = 0;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    
    public void TitleScene()
    {
        gameManager.EnemyDefeat = 0;
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
