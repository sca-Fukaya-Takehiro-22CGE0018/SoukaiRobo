using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;// 床の動く速さ
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime,0,0);
        if (this.transform.position.x <  -10)// 床を消す
        {
            Destroy(gameObject);
        }
    }
}
