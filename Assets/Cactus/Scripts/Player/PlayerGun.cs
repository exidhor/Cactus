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

#if UNITY_ANDROID
    Vector2 _oldMousePos;
#endif

    public void Actualize(float dt)
    {
        Vector2 mousePos;
        bool shoot = false;

#if UNITY_ANDROID
        int touchCount = Input.touchCount;

        mousePos = _oldMousePos;

        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(touch.position);

            if (0.15f < viewportPosition.x && viewportPosition.x < 0.85f)
            {
                mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                shoot = touch.phase == TouchPhase.Began;

                break;
            }
        }

        _oldMousePos = mousePos;
#else
        shoot = (Input.GetKeyDown(KeyCode.Space));

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

        Vector2 endPoint = _aimingLine.Refresh(_gunRoot.position, mousePos);

        if (shoot)
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
        Vector2 outsidePointTarget;

        if (!MathHelper.LineIntersectRect(out outsidePointTarget, _gunRoot.position, endPoint, GameCamera.instance.viewport))
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boundingBox.center, _boundingBox.size);
    }
#endif
}
