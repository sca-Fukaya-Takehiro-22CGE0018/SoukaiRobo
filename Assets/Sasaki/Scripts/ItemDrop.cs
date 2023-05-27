using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemDrop()
    {
        int randomHeart = Random.Range(0,3);
        //アイテムを落とすか落とさないか(確率)
        //落とさない場合
        if (randomHeart != 0) return;

        //落とす場合
        else
        {
            //ハートを生成する
        }


    }
}
