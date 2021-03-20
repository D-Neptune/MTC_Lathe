using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionEvent : MonoBehaviour
{
    public static CollisionEvent current;
    void Awake()
    {
        current = this;        
    }

    public event Action stockMaterialCollision;
    public void StockMaterialCollision()
    {
        if(stockMaterialCollision != null)
        {
            stockMaterialCollision();
        }
    }

}
