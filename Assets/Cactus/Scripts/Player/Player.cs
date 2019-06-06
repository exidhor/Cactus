using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class Player : MonoSingleton<Player>
{
    public new RectCollider collider
    {
        get { return _collider; }
    }

    public bool isAlive
    {
        get { return _life > 0; }
    }

    [Header("Infos")]
    [SerializeField] int _life = 1;

    [Header("Linking")]
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerGun _gun;
    [SerializeField] FollowThePlayer _followThePlayer;
    [SerializeField] RectCollider _collider;

    public void Actualize(float dt)
    {
        _movement.Actualize(dt);
        _gun.Actualize(dt);
        _followThePlayer.Actualize(dt);

        _collider.RefreshCollider();
    }

    public void ReceiveDamage(int damage)
    {
        _life -= damage;

        if (_life < 0) _life = 0;
    }
}
