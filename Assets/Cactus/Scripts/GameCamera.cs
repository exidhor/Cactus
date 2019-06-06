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
        Vector2 minViewport = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 maxViewport = Camera.main.ViewportToWorldPoint(Vector2.one);

        _viewport = new Rect(minViewport, maxViewport - minViewport);
    }
}