using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> heartObjects; // �n�[�g�I�u�W�F�N�g�̃��X�g
    [SerializeField] GameObject Heart;
    public CameraShake shake;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // �n�[�g�̔�\������
    public void HideHeart()
    {
        // �ŏ��ɕ\������Ă���n�[�g���\���ɂ���
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
        //�n�[�g�ɐG�ꂽ���
        //���X�g���̃n�[�g���t���ɕ\��
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
        //HP���Ȃ��Ȃ����烊�U���g��ʂɈړ�
        if (!Heart.gameObject.activeSelf)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}