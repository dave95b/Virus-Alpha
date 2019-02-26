using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods {

    public static float Distance(Vector3 start, Vector3 end)
    {
        return Mathf.Sqrt(Mathf.Pow(start.x - end.x, 2) + Mathf.Pow(start.y - end.y, 2) + Mathf.Pow(start.z - end.z, 2));
    }
}
