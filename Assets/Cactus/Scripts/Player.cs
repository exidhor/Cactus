using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class Player : MonoSingleton<Player>
{
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerGun _gun;
    [SerializeField] FollowThePlayer _followThePlayer;

    public void Actualize(float dt)
    {
        _movement.Actualize(dt);
        _gun.Actualize(dt);
        _followThePlayer.Actualize(dt);
    }
}
