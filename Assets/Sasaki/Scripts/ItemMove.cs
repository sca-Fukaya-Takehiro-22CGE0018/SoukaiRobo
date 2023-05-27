using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private GameObject Enemy;
    private float speed = 5.0f;
    private Vector3 pos;
    private int a = 1;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x -= 0.004f* 1.0f;
        //math.Pow(x, 何乗か)
        pos.y = -2.0f * Mathf.Pow(pos.x+1.02f, 2) + 2.0f;

        transform.position = pos;
    }
}
