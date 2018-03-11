using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScale : MonoBehaviour {

    [SerializeField] float distanceLjKp;
    private static float staticDistanceLjKp;

    private static Vector3 p1;
    private static Vector3 p2;

    private void Start()
    {
        p1 = transform.GetChild(0).GetComponent<Transform>().position;
        p2 = transform.GetChild(1).GetComponent<Transform>().position;
        staticDistanceLjKp = distanceLjKp;
    }

    public static float GetScale()
    {
        float distance = Vector3.Distance(p1, p2);
        float scale = staticDistanceLjKp / distance; // km/e
        return scale;
    }
}
