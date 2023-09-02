using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoOperation : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
    }

    public void OperationScene()
    {
        SceneManager.LoadScene("OperationScene", LoadSceneMode.Single);
    }
}
