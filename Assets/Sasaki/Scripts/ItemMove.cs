using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private GameObject Enemy;
    private float speed = 2.0f;
    private Vector3 pos;
    private float a = -2.0f;
    private bool onStage = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (onStage)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            return;
        }
        pos.x -= 0.004f;
        pos.y = a * Mathf.Pow(pos.x+1.02f, 2) + 2.0f;

        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Stage")
        {
            onStage = true;
        }
    }
}
