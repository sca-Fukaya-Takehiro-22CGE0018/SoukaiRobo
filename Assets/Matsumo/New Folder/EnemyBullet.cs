using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Ray2D ray2D;
    RaycastHit2D hit2D;
    Vector3 direction;
    float distance = 5;
    [SerializeField] float Speed;
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-(Speed * Time.deltaTime),0, 0);
        Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}

