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
    }
}
