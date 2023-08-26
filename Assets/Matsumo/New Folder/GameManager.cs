using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int defeat = 0;//撃破数
    int maxEnemy = 10;//最大撃破数
    [SerializeField] Image image = null;
    [SerializeField] GameObject panel = null;
    private bool tankBattle = false;
    private bool wallBattle = true;
    private bool helicopterBattle = false;
    private PanelManager panelManager;
    private AutoStage autoStage;

    private bool TankBattle = false;

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
        autoStage = FindObjectOfType<AutoStage>();
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
            if (!tankBattle)
            {
                tankBattle = true;
                autoStage.TankBattle();
                return;
            }
            if (!wallBattle)
            {
                wallBattle = true;
                autoStage.WallBattle();
                return;
            }
            if (!helicopterBattle)
            {
                helicopterBattle = true;
                autoStage.HelicopterBattle();
                return;
            }
        }
    }
}