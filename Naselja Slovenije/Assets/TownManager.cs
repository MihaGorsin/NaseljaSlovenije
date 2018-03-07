using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TownManager {

    private static List<Town> towns = new List<Town> {
        new Town("Ljubljana", new Vector2(484f, 245f)),
        new Town("Maribor", new Vector2(400f, 400f)),
        new Town("Koper", new Vector2(100f, 40f)),
        new Town("Domžale", new Vector2(56f, 145f)),
        new Town("Radomlje", new Vector2(123f, 548f)),
        new Town("Preserje", new Vector2(788f, 59f))
    };
    private static int index = 0;

    public static Town selectedTown = towns[index];

    public static Town GetNextTown()
    {
        index++;
        if (index >= towns.Count) {
            index = 0;
            MyMath.ShuffleList(towns);
        }
        selectedTown = towns[index];
        return selectedTown;
    }

    public static int NumberOfAllTowns
    {
        get {
            return towns.Count;
        }
    }
}

public class Town
{
    public string name;
    public Vector2 location;

    public Town(string destinationName, Vector2 locationPosition)
    {
        name = destinationName;
        location = locationPosition;
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