using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Decor : Collidable
{
    public new RectCollider collider
    {
        get { return _collider; }
    }
}