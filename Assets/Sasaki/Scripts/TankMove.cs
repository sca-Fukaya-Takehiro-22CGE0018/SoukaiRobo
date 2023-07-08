using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Missile;
    [SerializeField]
    private GameObject Player;

    private float speed;
    private float MissileTimer;

    private bool onStage = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = -3.0f;
        MissileTimer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed,0,0)*Time.deltaTime;
        if (transform.position.x <= 6.0f)
        {
            onStage = true;
        }
        if (onStage)
        {
            speed = 0.0f;
        }

        MissileTimer -= Time.deltaTime;
        if (MissileTimer <= 0.0f)
        {
            MissileAttack();
            MissileTimer = 5.0f;
        }
    }

    void MissileAttack()
    {
        Debug.Log("ミサイル発射");
        Instantiate(Missile,new Vector3(transform.position.x, transform.position.y+2.7f, transform.position.z),Quaternion.identity);
    }
}
