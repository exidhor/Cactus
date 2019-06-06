using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class PlayerGun : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] int _damage = 1;

    [Header("Linking")]
    [SerializeField] Transform _gunRoot;
    [SerializeField] AimingLine _aimingLine;

#if UNITY_EDITOR
    Rect _boundingBox;
#endif

    public void Actualize(float dt)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 endPoint = _aimingLine.Refresh(_gunRoot.position, mousePos);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RectCollider collider;

            if(IsCollided(out collider, endPoint))
            {
                if(collider.interactable != null)
                {
                    collider.interactable.ReceiveDamage(_damage);
                }
            }
        }
    }

    bool IsCollided(out RectCollider collider, Vector2 endPoint)
    {
        Vector2 minViewport = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 maxViewport = Camera.main.ViewportToWorldPoint(Vector2.one);

        Rect viewport = new Rect(minViewport, maxViewport - minViewport);

        Vector2 outsidePointTarget;

        if (!MathHelper.LineIntersectRect(out outsidePointTarget, _gunRoot.position, endPoint, viewport))
        {
            Debug.LogWarning("No collision with the viewport and the aiming line");
        }

        Rect boundingBox = MathHelper.ConstructRect(outsidePointTarget, _gunRoot.position);

#if UNITY_EDITOR
        _boundingBox = boundingBox;
#endif

        List<RectCollider> found = ColliderManager.instance.FindCollisions(boundingBox, "interactable");

        Vector2 intersection;
        for (int i = 0; i < found.Count; i++)
        {
            if (MathHelper.LineIntersectRect(out intersection, _gunRoot.position, endPoint, found[i].rect))
            {
                collider = found[i];
                return true;
            }
        }

        collider = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boundingBox.center, _boundingBox.size);
    }
}
