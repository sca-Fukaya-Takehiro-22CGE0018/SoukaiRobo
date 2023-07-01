using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    //パネルフラグ
    bool Panelflag = false;

    public bool panelFlag
    {
        get { return this.Panelflag;}
        set { this.Panelflag = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    void Pause()
    {
        //ESCが押されたらパネルの挙動
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Panel.activeSelf)
            {
                Panelflag = false;
                Panel.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                Panelflag = true;
                Panel.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }
}
