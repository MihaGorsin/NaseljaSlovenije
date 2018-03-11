﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour {

    //GameObject townToFindBackground; 
    GameObject townToFind; 
    //GameObject townsLeftBackground; 
    GameObject townsLeft; 
    GameObject infoBackground; 
    GameObject infoText;
    GameObject scoreText;
    GameObject highscore;

    // Use this for initialization
    void Start () {
        //townToFindBackground = transform.GetChild(0).gameObject;
        townToFind = transform.GetChild(1).gameObject;
        //townsLeftBackground = transform.GetChild(2).gameObject;
        townsLeft = transform.GetChild(3).gameObject;
        infoBackground = transform.GetChild(4).gameObject;
        infoText = transform.GetChild(5).gameObject;
        scoreText = transform.GetChild(7).gameObject;
        highscore = transform.GetChild(8).gameObject;
    }
	
    public void ChangeTown(string name)
    {
        townToFind.GetComponent<Text>().text = name;
    }
    public void RefreshTownsLeft(string current, string all)
    {
        townsLeft.GetComponent<Text>().text = current + " / " + all;
    }

    public void RefreshScore(string score)
    {
        scoreText.GetComponent<Text>().text = score;
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

    public void ShowHighscore()
    {
        highscore.SetActive(true);
    }
}
