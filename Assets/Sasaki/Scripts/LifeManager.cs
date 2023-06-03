using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // ハートオブジェクトのリスト
    [SerializeField] GameObject Heart;

    // ハートの非表示処理
    public void HideHeart()
    {
        // 最初に表示されているハートを非表示にする
        foreach (var heart in heartObjects)
        {
            if (heart.activeSelf)
            {
                heart.SetActive(false);
                CheckLife();
                break;
            }
        }
    }

    public void CheckLife()
    {
        if (!Heart.gameObject.activeSelf)
        {
            //SceneManager.LoadScene("ResultScene");
        }
    }
}