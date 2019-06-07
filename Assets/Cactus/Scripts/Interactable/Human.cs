using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Human : Interactable
{
    [Header("Human Infos")]
    [SerializeField] int _damage = 1;
    [SerializeField] float _durationBeforeFire = 1f;

    [Header("Linking for Human")]
    [SerializeField] SpriteRenderer _renderer;

    float _startTime;

    protected override void OnInit()
    {
        base.OnInit();

        gameObject.SetActive(false);
    }

    protected override void OnTrigger()
    {
        base.OnTrigger();

        gameObject.SetActive(true);
        FacePlayer();
        _startTime = Time.time;
    }

    protected override void OnActualize(float dt)
    {
        base.OnActualize(dt);

        FacePlayer();
        HandleShoot();
    }

    void HandleShoot()
    {
        float nt = (Time.time - _startTime) / _durationBeforeFire;

        Color color = Color.Lerp(Color.white, Color.red, nt);
        _renderer.color = color;

        if (nt >= 1)
        {
            Player.instance.ReceiveDamage(_damage);
            _startTime = Time.time;
        }
    }

    void FacePlayer()
    {
        float playerX = Player.instance.collider.rect.center.x;
        float humanX = _collider.rect.center.x;

        Vector3 scale = transform.localScale;

        if (playerX < humanX)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }

        transform.localScale = scale;
    }
}