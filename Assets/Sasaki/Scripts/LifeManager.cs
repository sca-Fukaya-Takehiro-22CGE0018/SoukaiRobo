using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public GameObject[] lifeArray = new GameObject[3];
    private int lifePoint = 3;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && lifePoint < 3)//Input.GetMouseButton(0)‚ð“G‚ÌUŒ‚‚ðŽó‚¯‚½‚ç‚É‚·‚é
        {
            lifePoint ++;
            lifeArray[lifePoint-1].SetActive(true);
        }

        else if (Input.GetMouseButtonDown(1) && lifePoint > 0)
        {
            lifeArray[lifePoint-1].SetActive(false);
            lifePoint--;
        }
    }
}
