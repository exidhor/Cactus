using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform _gunRoot;
    [SerializeField] AimingLine _aimingLine;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _aimingLine.Refresh(_gunRoot.position, mousePos);
    }
}
