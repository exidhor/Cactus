using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class TriggerZoneManager : MonoSingleton<TriggerZoneManager>
{
    [SerializeField] string _colliderLayer;

    public void Actualize(float dt)
    {
        RectCollider playerCollider = Player.instance.collider;

        List<RectCollider> colliders = ColliderManager.instance.FindCollisions(playerCollider.rect, _colliderLayer);

        for(int i = 0; i < colliders.Count; i++)
        {
            colliders[i].triggerZone.OnTrigger();
        }
    }
}