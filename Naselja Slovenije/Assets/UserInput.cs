using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {
    //TownManager naselja;
    // Use this for initialization
    void Start () {
        //naselja = new TownManager();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log(Input.mousePosition);
            Debug.Log(Vector2.Distance((Vector2) Input.mousePosition, TownManager.selectedTown.location));
        }
	}
}
