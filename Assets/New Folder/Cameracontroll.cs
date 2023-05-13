using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroll : MonoBehaviour
{
    private float px;
    private float py;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        px = GameObject.Find("Player").transform.position.x;
        py = GameObject.Find("Player").transform.position.y;
        this.transform.position = new Vector3(px, py+1, -20f);
    }
}
