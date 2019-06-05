using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class TriggerZone : Collidable
{
    List<ITriggerable> _triggerables = new List<ITriggerable>();

    public void Init()
    {
        _collider.RefreshCollider();
    }

    public void Register(ITriggerable triggerable)
    {
        _triggerables.Add(triggerable);
    }

    public void Unregister(ITriggerable triggerable)
    {
        _triggerables.Remove(triggerable);
    }

    public void OnTrigger()
    {
        for(int i = 0; i < _triggerables.Count; i++)
        {
            _triggerables[i].Trigger();
        }
    }
}
