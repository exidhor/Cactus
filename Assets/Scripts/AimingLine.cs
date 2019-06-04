using System;
using System.Collections.Generic;
using UnityEngine;

public class AimingLine : MonoBehaviour
{
    [SerializeField] Transform _line;

    public void Refresh(Vector2 output, Vector2 target)
    {
        _line.position = new Vector3(output.x, output.y, _line.position.z);

        float angle = Angle(output, output + Vector2.up, target);
        _line.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public static float Angle(Vector2 from, Vector2 to)
    {
        return Mathf.DeltaAngle(Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg,
            Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg);
    }

    public static float Angle(Vector2 origin, Vector2 from, Vector2 to)
    {
        Vector2 first = from - origin;
        Vector2 second = to - origin;

        return Angle(first, second);
    }
}
