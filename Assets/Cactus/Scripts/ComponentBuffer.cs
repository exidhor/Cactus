using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentBuffer : MonoBehaviour
{
    public TriggerZone triggerZone
    {
        get { return _triggerZone; }
    }

    public Interactable interactable
    {
        get { return _interactable; }
    }

    public Player player
    {
        get { return _player; }
    }

    public Decor decor
    {
        get { return _decor; }
    }

    [Header("Buffer")]
    [SerializeField] TriggerZone _triggerZone;
    [SerializeField] Interactable _interactable;
    [SerializeField] Player _player;
    [SerializeField] Decor _decor;

}