using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class AimingLine : MonoBehaviour
{
    [SerializeField] Transform _line;

#if UNITY_EDITOR
    Vector2 _endPoint;
    Vector2 _output;
#endif

    /// <summary>
    /// Return the end point
    /// </summary>
    /// <param name="output"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector2 Refresh(Vector2 output, Vector2 target)
    {
        _line.position = new Vector3(output.x, output.y, _line.position.z);

        float angle = Angle(output, output + Vector2.up, target);
        _line.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Vector2 endPoint = Vector2.up * 50;
        endPoint.Rotate(angle);
        endPoint += output;

#if UNITY_EDITOR
        _output = output;
        _endPoint = endPoint;
#endif

        return endPoint;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;

        Rect rect = MathHelper.ConstructRect(_output, _endPoint);
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
