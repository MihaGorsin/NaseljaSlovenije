using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBestScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(PlayerPrefs.GetFloat("score") == 0)
            PlayerPrefs.SetFloat("score", 10000f);

        GetComponent<Text>().text = "My best score is: " + PlayerPrefs.GetFloat("score").ToString() + " points";
	}
}
