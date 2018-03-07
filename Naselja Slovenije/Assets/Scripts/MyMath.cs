using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath {

    public static void ShuffleList<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Vector3 MouseToWorldPosition(Vector3 mousePosition)
    {
        Vector3 worldPosition = mousePosition / 14f;
        worldPosition.z = -50f;
        return worldPosition;
    }

    public static float DistanceBetweenPoints(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1,p2);
    }
}
