using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour {

    GameObject townToFindBackground; 
    GameObject townToFind; 
    GameObject townsLeftBackground; 
    GameObject townsLeft; 
    GameObject infoBackground; 
    GameObject infoText;

	// Use this for initialization
	void Start () {
        townToFindBackground = transform.GetChild(0).gameObject;
        townToFind = transform.GetChild(1).gameObject;
        townsLeftBackground = transform.GetChild(2).gameObject;
        townsLeft = transform.GetChild(3).gameObject;
        infoBackground = transform.GetChild(4).gameObject;
        infoText = transform.GetChild(5).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeTown(string name)
    {
        townToFind.GetComponent<Text>().text = name;
    }
    
    public void DisplayInfoFor(string info, float time)
    {
        infoBackground.SetActive(true);
        infoText.SetActive(true);
        infoText.GetComponent<Text>().text = info;
        Invoke("ClearInfo", time);
    }
    public void ClearInfo()
    {
        infoBackground.SetActive(false);
        infoText.SetActive(false);
    }
}
