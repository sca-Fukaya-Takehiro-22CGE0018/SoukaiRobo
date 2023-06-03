using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
