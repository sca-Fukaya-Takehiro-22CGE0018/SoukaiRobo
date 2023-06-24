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
    private bool onStage = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = -3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed,0,0)*Time.deltaTime;
        if (transform.position.x <= 5.0f)
        {
            onStage = true;
        }
        if (onStage)
        {
            speed = 0.0f;
        }
    }

    void MissileAttack()
    {

    }
}
