using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class GameCamera : MonoSingleton<GameCamera>
{
    public Rect viewport
    {
        get { return _viewport; }
    }

    Rect _viewport;

    private void Awake()
    {
        RefreshViewport();
    }

    public void Actualize(float dt)
    {
        RefreshViewport();
    }

    void RefreshViewport()
    {
#if UNITY_ANDROID
        Vector2 minPoint = new Vector2(0.15f, 0f);
        Vector2 maxPoint = new Vector2(0.85f, 1f);
#else
        Vector2 minPoint = Vector2.zero;
        Vector2 maxPoint = Vector2.one;
#endif

        Vector2 minViewport = Camera.main.ViewportToWorldPoint(minPoint);
        Vector2 maxViewport = Camera.main.ViewportToWorldPoint(maxPoint);

        _viewport = new Rect(minViewport, maxViewport - minViewport);
    }
}