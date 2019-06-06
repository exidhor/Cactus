using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Interactable : Collidable, ITriggerable
{
    [Header("Infos")]
    [SerializeField] int _life = 1;
    [SerializeField] int _damage = 1;
    [SerializeField] float _durationBeforeFire = 1f;

    [Header("Linking")]
    [SerializeField] TriggerZone _triggerZone;
    [SerializeField] GameObject _event;
    [SerializeField] SpriteRenderer _renderer;

    float _startTime;
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
        if (!GameCamera.instance.viewport.Contains(transform.position)) return;

        _isTrigger = true;
        _triggerZone.Unregister(this);
        _collider.SetValid(true);

        _event.SetActive(true);

        _startTime = Time.time;
    }

    public void ReceiveDamage(int damage)
    {
        _life -= damage;

        if (_life < 0) _life = 0;

        if(_life == 0)
        {
            _collider.SetValid(false);
            _event.SetActive(false);
        }
    }

    public void Actualize(float dt)
    {
        if (!GameCamera.instance.viewport.Contains(transform.position)) return;

        float nt = (Time.time - _startTime) / _durationBeforeFire;

        Color color = Color.Lerp(Color.white, Color.red, nt);
        _renderer.color = color;

        if(nt >= 1)
        {
            Player.instance.ReceiveDamage(_damage);
            _startTime = Time.time;
        }
    }
}
