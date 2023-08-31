using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField]//エフェクトプレハブ
    GameObject Anim;
    private GameObject player;
    private Animator anim;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
