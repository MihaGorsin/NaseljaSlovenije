﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject townPointPrefab;
    [SerializeField] InstructionsManager instructionManager;

    private const float minimalCameraSize = 12f;
    private const float zoomPrecision = 10f;

    private bool oneClickPerTown = false;
    private float defaultCameraSize;

    private GameObject point;
    private GameObject townPoint;
    private Town currentTown;
    private Vector3 lastMousePosition;

    // Use this for initialization
    void Start () {
        currentTown = TownManager.currentTown;
        defaultCameraSize = Camera.main.orthographicSize;

        try {
            InicializeTownsLeft();
        } catch {
            Invoke("InicializeTownsLeft", 0.1f);
        }
    }

    void InicializeTownsLeft()
    {
        instructionManager.RefreshTownsLeft((TownManager.currentIndex + 1).ToString(), TownManager.NumberOfAllTowns.ToString());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0) && !oneClickPerTown) {
            Vector3 mp = Input.mousePosition;
            if(mp.x >= 630 && mp.y >= 205 && mp.y <= 375) /* Ne naredi ničesar, zoom */;
            else Click(Input.mousePosition);
        }
        if (Input.GetMouseButton(1)) {
            MoveAroundTheMap(Input.mousePosition);
        }
        if(Input.GetMouseButtonUp(1)) lastMousePosition = Vector3.zero;
    }

    void MoveAroundTheMap(Vector3 mousePosition)
    {
        mousePosition = MyMath.MouseToWorldPosition(mousePosition);
        if(lastMousePosition == Vector3.zero) { // če je to prvi klik sploh ne premikaj zemljevida
            lastMousePosition = mousePosition;
            return;
        }

        Vector3 changePosition = lastMousePosition - mousePosition;
        changePosition /= 15f;
        Vector3 newPosition = new Vector3(changePosition.x, changePosition.y, 0);
        Camera.main.transform.position += newPosition;
        ClampCameraPosition();
    }

    void ClampCameraPosition()
    {
        Vector3 cP = Camera.main.transform.position;
        Vector3 newP = cP;
        if(cP.x >= 55) newP.x = 55;
        if(cP.x <= -55) newP.x = -55;
        if(cP.y >= 35) newP.y = 35;
        if(cP.y <= -35) newP.y = -35;
        Camera.main.transform.position = newP;
    }

    void Click(Vector3 mousePosition)
    {
        oneClickPerTown = true;
        float distance = DrawPointAndReturnMiss(mousePosition);
        instructionManager.DisplayInfoFor(distance.ToString(), 1.5f);
        Invoke("NextTown", 1.5f);
    }

    float DrawPointAndReturnMiss(Vector3 mousePosition)
    {
        point = Instantiate(pointPrefab);
        DrawCurrentTown();
        point.transform.position = MyMath.MouseToWorldPosition(mousePosition);
        DrawLine(point.transform.position, MyMath.MouseToWorldPosition(currentTown.position), 1f);
        float distanceOff = MyMath.DistanceBetweenPoints(point.transform.position, (Vector3)currentTown.position);
        return distanceOff;
    }

    void DrawCurrentTown()
    {
        townPoint = Instantiate(townPointPrefab);
        townPoint.transform.position = MyMath.MouseToWorldPosition((Vector3)currentTown.position);
    }

    void NextTown()
    {
        Destroy(point);
        Destroy(townPoint);
        ClearLine();
        currentTown = TownManager.GetNextTown();
        instructionManager.ChangeTown(currentTown.name);
        instructionManager.RefreshTownsLeft((TownManager.currentIndex+1).ToString(), TownManager.NumberOfAllTowns.ToString());
        oneClickPerTown = false;
    }

    public void ZoomIn()
    {
        Camera.main.orthographicSize -= zoomPrecision;
        if (Camera.main.orthographicSize < minimalCameraSize) {
            Camera.main.orthographicSize = minimalCameraSize;
        }
    }

    public void ZoomOut()
    {
        Camera.main.orthographicSize += zoomPrecision;
        if (Camera.main.orthographicSize > defaultCameraSize) {
            Camera.main.orthographicSize = defaultCameraSize;
        }
    }

    public void ResetPositionZoom()
    {
        Camera.main.orthographicSize = defaultCameraSize;
        Camera.main.transform.position = new Vector3(0,0, Camera.main.transform.position.z);
    }

    void DrawLine(Vector3 p1, Vector3 p2, float precision)
    {
        if(precision < 0.001f) precision = 0.001f;
        float x0 = p1.x, y0 = p1.y, 
            steps = Vector2.Distance(p1, p2) / precision, 
            rx = p1.x-p2.x, 
            ry = p1.y-p2.y, 
            x1, y1;
        for(int i=0; i<steps; i++) {
            x1 = x0 - rx * (i / steps);
            y1 = y0 - ry * (i / steps);
            GameObject p = Instantiate(pointPrefab);
            p.transform.position = new Vector3(x1, y1, p2.z);
            p.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            p.transform.SetParent(transform.GetChild(0));
        }
    }

    void ClearLine()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for(int i=2; i<children.Length; i++)
            Destroy(children[i].gameObject);
    }
}
