using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquake : MonoBehaviour
{
    [SerializeField] GameObject eye = null;
    private PlayerControll playerControll;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControll = collision.gameObject.GetComponent<PlayerControll>();
            eye.SetActive(true);
            playerControll.enabled = false;
            Invoke("StartPlayer",0.5f);
        }
    }

    void StartPlayer()
    {
        playerControll.enabled = true;
        eye.SetActive(false);
    }
}
