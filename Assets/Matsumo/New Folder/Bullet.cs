using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;
    private AutoStage autoStage;
    private float limit;
    // Start is called before the first frame update
    void Start()
    {
        autoStage = FindObjectOfType<AutoStage>();
        limit = autoStage.rightTop.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
        if (this.transform.position.x >= limit)
        {
            Destroy(this.gameObject);
        }
    }
}
