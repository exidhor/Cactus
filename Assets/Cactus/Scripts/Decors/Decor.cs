using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[RequireComponent(typeof(RectCollider))]
public class Decor : Collidable
{
    public new RectCollider collider
    {
        get { return _collider; }
    }

    public virtual void Init(RandomGenerator random)
    {

    }
}