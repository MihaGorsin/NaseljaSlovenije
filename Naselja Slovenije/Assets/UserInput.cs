using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {
    [SerializeField] private float scale;
    // Use this for initialization
    void Start () {
        //naselja = new TownManager();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log( (Vector2) Camera.main.ScreenToWorldPoint ( Input.mousePosition ));
            Debug.Log(Vector2.Distance((Vector2) Input.mousePosition, TownManager.selectedTown.location) / scale);
        }
	}
}
