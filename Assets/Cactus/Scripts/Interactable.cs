using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Interactable : MonoBehaviour, ITriggerable
{
    [SerializeField] TriggerZone _triggerZone;
    [SerializeField] GameObject _event;

    bool _isTrigger;

    private void Start()
    {
        _event.SetActive(false);
        _triggerZone.Register(this);
    }

    public void Trigger()
    {
        _isTrigger = true;
        _triggerZone.Unregister(this);

        _event.SetActive(true);
    }
}
