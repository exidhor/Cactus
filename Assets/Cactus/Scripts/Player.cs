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
}
