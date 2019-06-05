using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    protected RectCollider _collider
    {
        get
        {
            if(__collider == null)
            {
                __collider = GetComponent<RectCollider>();
            }

            return __collider;
        }
    }
    
    RectCollider __collider;

    protected virtual void Awake()
    {
        if (__collider == null)
        {
            __collider = GetComponent<RectCollider>();
        }
    }
}