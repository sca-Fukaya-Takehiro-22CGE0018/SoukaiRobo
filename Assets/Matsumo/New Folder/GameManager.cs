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
    private int Wave = 0;
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
            defeat = 0;
            if (Wave == 0)
            {
                Wave = 1;
                autoStage.TankBattle();
                return;
            }
            //if (!wallBattle)
            //{
            //    wallBattle = true;
            //    autoStage.WallBattle();
            //    return;
            //}
            if (Wave == 1)
            {
                Wave = 2;
                autoStage.HelicopterBattle();
                return;
            }
        }
    }
}