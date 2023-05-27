using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // ハートオブジェクトのリスト

    // ハートの非表示処理
    public void HideHeart()
    {
        // 最初に表示されているハートを非表示にする
        foreach (var heart in heartObjects)
        {
            if (heart.activeSelf)
            {
                heart.SetActive(false);
                break;
            }
        }
    }
}