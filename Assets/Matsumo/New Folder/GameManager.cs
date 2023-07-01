using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int defeat = 0;//撃破数
    private int maxEnemy = 2;//最大撃破数
    [SerializeField]
    Slider slider;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public int EnemyDefeat
    {
        get { return defeat; }
        set { defeat = value;}
    }
    public int MaxEnemy
    {
        get { return maxEnemy;}
        set { maxEnemy = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        if(maxEnemy == defeat)
        {
            SceneManager.LoadScene("");
        }
        defeat = 0;
    }
}
