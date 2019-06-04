﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    [SerializeField] Transform _player;

    Vector3 _offset;

    private void Awake()
    {
        _offset = _player.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _player.position - _offset;
    }
}