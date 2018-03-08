using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject townPointPrefab;
    [SerializeField] InstructionsManager instructionManager;

    private bool clicked = false;
    private GameObject point;
    private GameObject townPoint;
    private Town currentTown;

    // Use this for initialization
    void Start () {
        currentTown = TownManager.currentTown;
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
        clicked = false;
    }
}
