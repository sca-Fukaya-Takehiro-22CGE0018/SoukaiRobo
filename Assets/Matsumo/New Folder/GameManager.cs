using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int defeat = 0;//撃破数
    private int maxEnemy = 2;//最大撃破数
    private PanelManager panelManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int EnemyDefeat
    {
        get { return defeat; }
        set { defeat = value; }
    }
    public int MaxEnemy
    {
        get { return maxEnemy; }
        set { maxEnemy = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        panelManager = GetComponent<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Load();
    }

    public void Load()
    {
        if (defeat == maxEnemy)
        {
            Debug.Log(defeat);
            Debug.Log("aa");
            defeat = 0;
            SceneManager.LoadScene(2);
        }
    }
}