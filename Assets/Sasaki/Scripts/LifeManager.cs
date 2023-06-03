using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // �n�[�g�I�u�W�F�N�g�̃��X�g
    [SerializeField] GameObject Heart;

    // �n�[�g�̔�\������
    public void HideHeart()
    {
        // �ŏ��ɕ\������Ă���n�[�g���\���ɂ���
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