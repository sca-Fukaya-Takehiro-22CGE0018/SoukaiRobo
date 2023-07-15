using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private float sin;
    private float HorizontalWidth = 14.0f;//横移動の幅
    private float VerticalWidth = 1.5f;//垂直移動の幅

    private float Height = 3.5f;//高さ調整
    private float Width = 6.5f;
    private float speed = 2.0f;//移動速度

    private int Move = 0;
    private int Rotate = 0;
    private float MoveTime = 6.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //移動の向き変更
        MoveTime -= Time.deltaTime;
        if (MoveTime <= 0.0f)
        {
            MoveTime = 6.0f;
            if (Move == 0)
            {
                Height = 1.0f;
                Move = 1;
            }
            else if(Move == 1)
            {
                Height = 3.5f;
                Move = 0;
            }
        }

        //移動
        sin = Mathf.Sin(Time.time * speed);
        if (Move == 0)
        {
            transform.position = new Vector3(sin * HorizontalWidth, Height, 0);
        }
        if (Move == 1)
        {
            transform.position = new Vector3(Width, sin * VerticalWidth + Height, 0);
        }

        //回転
        if (transform.position.x <= -13.0f)
        {
            if (Rotate == 0)
            {
                Rotate = 1;
                transform.Rotate(0, 180, 0);
            }
        }
        if (Rotate == 1)
        {
            if (transform.position.x >= 13.0f)
            {
                Rotate = 0;
                transform.Rotate(0, 180.0f, 0);
            }
        }


    }
}
