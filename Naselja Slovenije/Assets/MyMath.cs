﻿using System.Collections;
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
}
