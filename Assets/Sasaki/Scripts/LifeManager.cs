using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // ハートオブジェクトのリスト
    [SerializeField] GameObject Heart;
    public CameraShake shake;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // ハートの非表示処理
    public void HideHeart()
    {
        // 最初に表示されているハートを非表示にする
        foreach (var heart in heartObjects)
        {
            if (heart.activeSelf)
            {
                scoreManager.BonusReset();
                shake.Shake(0.2f,0.2f);
                heart.SetActive(false);
                CheckLife();
                break;
            }
        }
    }

    public void HeartRecovery()
    {
        //ハートに触れたら回復
        //リスト内のハートを逆順に表示
        heartObjects.Reverse();
        foreach(var heart in heartObjects)
        {
            if (!heart.activeSelf)
            {
                heart.SetActive(true);
                break;
            }
        }
        heartObjects.Reverse();
    }

    public void CheckLife()
    {
        //HPがなくなったらリザルト画面に移動
        if (!Heart.gameObject.activeSelf)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}