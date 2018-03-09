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
        instructionManager.RefreshTownsLeft((TownManager.currentIndex + 1).ToString(), TownManager.NumberOfAllTowns.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !clicked) {
            Click(Input.mousePosition);
        }
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
}
