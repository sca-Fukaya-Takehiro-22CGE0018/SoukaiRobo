using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // �n�[�g�I�u�W�F�N�g�̃��X�g

    // �n�[�g�̔�\������
    public void HideHeart()
    {
        // �ŏ��ɕ\������Ă���n�[�g���\���ɂ���
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