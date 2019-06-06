using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Interactable : Collidable, ITriggerable
{
    [Header("Interactable Infos")]
    [SerializeField] int _life = 1;
     
    [Header("Linking for Interactable")]
    [SerializeField] TriggerZone _triggerZone;
    
    bool _isTrigger;

    private void OnEnable()
    {
        InteractableManager.instance.Register(this);
    }

    private void OnDisable()
    {
        if(InteractableManager.internalInstance != null)
        {
            InteractableManager.instance.Unregister(this);
        }
    }

    protected virtual void OnTrigger() { }
    protected virtual void OnInit() { }
    protected virtual void OnActualize(float dt) { }

    public void Init()
    {
        _isTrigger = false;

        _triggerZone.Init();
        _triggerZone.Register(this);
        _collider.SetValid(false);
        _collider.RefreshCollider();

        OnInit();
    }

    public void Trigger()
    {
        if (!GameCamera.instance.viewport.Contains(transform.position)) return;

        _isTrigger = true;
        _triggerZone.Unregister(this);
        _collider.SetValid(true);

        OnTrigger();
    }

    public void ReceiveDamage(int damage)
    {
        _life -= damage;

        if (_life < 0) _life = 0;

        if (_life == 0)
        {
            _collider.SetValid(false);
            gameObject.SetActive(false);
        }
    }

    public void Actualize(float dt)
    {
        if (!GameCamera.instance.viewport.Overlaps(_collider.rect)) return;

        OnActualize(dt);
    }
}
