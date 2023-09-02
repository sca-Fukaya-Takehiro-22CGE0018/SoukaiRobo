using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitle : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
