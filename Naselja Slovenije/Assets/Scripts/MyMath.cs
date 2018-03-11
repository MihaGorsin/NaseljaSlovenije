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

    public static Vector3 ScreenToWorldPoint(Vector3 mousePosition, bool zTo400)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if(zTo400) worldPosition.z = 400f;
        return worldPosition;
    }

    public static float DistanceBetweenTowns(Vector3 p1, Vector3 p2)
    {
        float distance = Vector3.Distance(p1, p2);
        distance *= MapScale.GetScale();
        distance = Mathf.Round(distance * 100) / 100; // round 2 decimal points
        return distance;
    }

    public static float ZoomScale(float localScale, float defaultOrtographicSize, float orthographicSize)
    {
        float scale = localScale * orthographicSize;
        scale /= defaultOrtographicSize;
        return scale;
    }
}
