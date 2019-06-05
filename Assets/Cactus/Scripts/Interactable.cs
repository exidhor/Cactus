using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Interactable : Collidable, ITriggerable
{
    [SerializeField] TriggerZone _triggerZone;
    [SerializeField] GameObject _event;

    bool _isTrigger;

    public void Init()
    {
        _isTrigger = false;

        _event.SetActive(false);
        _triggerZone.Init();
        _triggerZone.Register(this);
        _collider.SetValid(false);
        _collider.RefreshCollider();
    }

    public void Trigger()
    {
        _isTrigger = true;
        _triggerZone.Unregister(this);
        _collider.SetValid(true);

        _event.SetActive(true);
    }
}
