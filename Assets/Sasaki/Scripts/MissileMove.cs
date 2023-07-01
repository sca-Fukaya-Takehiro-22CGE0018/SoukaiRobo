using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private float speed = 3.0f;
    private float dif;

    private Vector3 PlayerPosition;
    private Vector3 MissilePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissilePosition = transform.position;
        MissilePosition.x -= speed*Time.deltaTime;
        dif = PlayerPosition.y - MissilePosition.y;
        if (dif >= 0)
        {
            MissilePosition.y += speed*Time.deltaTime;
        }
        else
        {
            MissilePosition.y -= speed*Time.deltaTime;
        }
        transform.position = MissilePosition;
    }
}
