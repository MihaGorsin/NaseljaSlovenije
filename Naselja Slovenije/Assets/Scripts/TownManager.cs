using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TownManager {

    private static List<Town> towns = new List<Town> {
        new Town("Ljubljana", new Vector3(-21.4f, -5.7f, 400f)),
        new Town("Maribor", new Vector3(26.8f, 24.3f, 400f)),
        new Town("Koper", new Vector3(-55.1f, -36.9f, 400f)),
        new Town("Domžale", new Vector3(-17.5f, 0.7f, 400f)),
        new Town("Kamnik", new Vector3(-17.3f, 4.7f, 400f)),
        new Town("Kranj", new Vector3(-27.1f, 5.5f, 400f))
    };
    private static int index = 0;

    public static Town currentTown = towns[index];

    public static Town GetNextTown()
    {
        index++;
        if (index >= towns.Count) {
            index = 0;
            MyMath.ShuffleList(towns);
        }
        currentTown = towns[index];
        return currentTown;
    }

    public static int NumberOfAllTowns
    {
        get {
            return towns.Count;
        }
    }

    public static int currentIndex {
        get {
            return index;
        }
    }
}

public class Town
{
    public string name;
    public Vector3 position;

    public Town(string destinationName, Vector3 locationPosition)
    {
        name = destinationName;
        position = locationPosition;
    }
}


/*
 * 
 * Google zoom scales
 * 
 * 
20 : 1128.497220
19 : 2256.994440
18 : 4513.988880
17 : 9027.977761
16 : 18055.955520
15 : 36111.911040
14 : 72223.822090
13 : 144447.644200
12 : 288895.288400
11 : 577790.576700
10 : 1155581.153000
9  : 2311162.307000
8  : 4622324.614000
7  : 9244649.227000
6  : 18489298.450000
5  : 36978596.910000
4  : 73957193.820000
3  : 147914387.600000
2  : 295828775.300000
1  : 591657550.500000


Check zoom level
http://www.wolfpil.de/v3/deep-zoom.html

*/