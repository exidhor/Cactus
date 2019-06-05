using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class RectCollider : ComponentBuffer, IQTClearable
{
    public Rect rect
    {
        get { return _rect; }
    }

    public string layer
    {
        get { return _layer; }
    }

    public bool isDynamic
    {
        get { return _isDynamic; }
    }

    bool IQTClearable.isEnable
    {
        get { return _isValid; }
    }
    
    [Header("Infos")]
    [SerializeField] bool _isDynamic;
    [SerializeField] string _layer;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

#if UNITY_EDITOR
    [Header("Gizmos")]
    [SerializeField] Color _gizmosColor = Color.blue;
#endif

    bool _isValid = true;
    Rect _rect = new Rect();

    void OnEnable()
    {
        RefreshCollider();
    }
    
    public Rect RefreshCollider()
    {
        _rect = GetCollider();
        ColliderManager.instance.Register(this, _layer);

        return _rect;
    }

    public Rect GetCollider()
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;

        return new Rect(center.x - size.x / 2,
                        center.y - size.y / 2,
                        size.x,
                        size.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}