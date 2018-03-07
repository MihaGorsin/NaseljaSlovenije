using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject point;
    [SerializeField] GameObject townPoint;
    [SerializeField] InstructionsManager instructionManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Click(Input.mousePosition);
            Debug.Log(Input.mousePosition);
            Debug.Log(Vector2.Distance((Vector2)Input.mousePosition, TownManager.currentTown.position));
        }
    }

    void Click(Vector3 mousePosition)
    {
        GameObject newPoint = Instantiate(point);
        newPoint.transform.position = MyMath.MouseToWorldPosition(mousePosition);
        float distanceOff = MyMath.DistanceBetweenPoints(newPoint.transform.position, (Vector3)TownManager.currentTown.position);
        instructionManager.DisplayInfoFor(distanceOff.ToString(), 3f);
    }
}
