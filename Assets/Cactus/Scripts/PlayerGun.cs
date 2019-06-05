using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform _gunRoot;
    [SerializeField] AimingLine _aimingLine;

    public void Actualize(float dt)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 endPoint = _aimingLine.Refresh(_gunRoot.position, mousePos);

        Vector2 minViewport = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 maxViewport = Camera.main.ViewportToWorldPoint(Vector2.one);

        Rect viewport = new Rect(minViewport, maxViewport - minViewport);

        Vector2 outsidePointTarget;

        if(!MathHelper.LineIntersectRect(out outsidePointTarget, _gunRoot.position, endPoint, viewport))
        {
            Debug.LogWarning("No collision with the viewport and the aiming line");
        }

        Rect boundingBox = MathHelper.ConstructRect(outsidePointTarget, _gunRoot.position);

        // to finish
    }


}
