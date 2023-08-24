using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;// 床の動く速さ
    private AutoStage autoStage;
    // Start is called before the first frame update
    void Start()
    {
        autoStage = FindObjectOfType<AutoStage>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime,0,0);
        if (this.transform.position.x <  autoStage.leftBottom.x-10.0f)// 床を消す
        {
            Destroy(gameObject);
        }
    }
}
