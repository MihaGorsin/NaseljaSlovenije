using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject townPointPrefab;
    [SerializeField] InstructionsManager instructionManager;

    private bool clicked = false;
    private const float minimalCameraSize = 12f;
    private const float zoomPrecision = 10f;
    private float defaultCameraSize;
    private GameObject point;
    private GameObject townPoint;
    private Town currentTown;

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
        if (Input.GetMouseButtonUp(0) && !clicked) {
            Click(Input.mousePosition);
        }
        if (Input.GetMouseButton(0)) {
            MoveAroundTheMap(Input.mousePosition);
        }
    }

    void MoveAroundTheMap(Vector3 mousePosition)
    {
        mousePosition = MyMath.MouseToWorldPosition(mousePosition);
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, -500f);
        Camera.main.transform.position = newPosition;
    }

    void Click(Vector3 mousePosition)
    {
        clicked = true;
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
        clicked = false;
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
