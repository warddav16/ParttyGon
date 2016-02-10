using UnityEngine;
using System.Collections;

public static class VectorExtensions
{
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir; 
        return dir + pivot;
    }

    public static Vector3 Clamp(Vector3 clamp, Vector3 min, Vector3 max)
    {
        clamp.x = Mathf.Clamp(clamp.x, min.x, max.x);
        clamp.y = Mathf.Clamp(clamp.y, min.y, max.y);
        clamp.z = Mathf.Clamp(clamp.z, min.z, max.z);
        return clamp;
    }
}
