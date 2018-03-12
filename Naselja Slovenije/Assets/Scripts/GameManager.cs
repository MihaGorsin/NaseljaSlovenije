using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject townPointPrefab;
    [SerializeField] InstructionsManager instructionManager;

    private const float minimalCameraSize = 12f;
    private const float zoomPrecision = 10f;

    private bool oneClickPerTown = false;
    private bool submitedOnce = false;
    private float defaultCameraSize;
    private float score = 0f;

    private GameObject point;
    private GameObject townPoint;
    private Town currentTown;
    private Vector3 lastMousePosition;

    // Use this for initialization
    void Start () {
        currentTown = TownManager.currentTown;
        defaultCameraSize = Camera.main.orthographicSize;
        if(PlayerPrefs.GetFloat("score") == 0) PlayerPrefs.SetFloat("score", 10000f);

        try {
            InicializeInstructions();
        } catch(Exception ex) {
            Debug.Log(ex.Message);
            Invoke("InicializeInstructions", 0.1f);
        } finally{
            Invoke("InicializeInstructions", 0.5f);
        }
    }

    void InicializeInstructions()
    {
        instructionManager.RefreshTownsLeft((TownManager.currentIndex + 1).ToString(), TownManager.NumberOfAllTowns.ToString());
        instructionManager.ChangeTown(TownManager.currentTown.name);
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
        mousePosition = MyMath.ScreenToWorldPoint(mousePosition, false);
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
        Debug.Log("MP: " + Camera.main.ScreenToWorldPoint(mousePosition));
        oneClickPerTown = true;
        float distance = DrawPointAndReturnMiss(mousePosition);
        instructionManager.DisplayInfoFor(StyleDistance(distance), 1.5f);
        score += distance;
        instructionManager.RefreshScore(StyleScore(score));

        if(TownManager.currentIndex < TownManager.NumberOfAllTowns - 1)
            Invoke("NextTown", 1.5f);
        else
            Invoke("EndGame", 1.5f);
    }

    string StyleDistance(float distanceF)
    {
        string distance = distanceF.ToString(), twoDecimals = "";

        int dotIndex = distance.IndexOf('.');
        if(dotIndex < 0)
            distance += ".00";
        else {
            twoDecimals = distance.Substring(dotIndex+1);
            if(twoDecimals.Length <= 1)
                distance += "0";
        }

        distance += " km";

        return distance;
    }

    string StyleScore(float scoreF)
    {
        scoreF = Mathf.Round(scoreF * 100) / 100;

        string score = scoreF.ToString(), twoDecimals = "";

        int dotIndex = score.IndexOf('.');
        if(dotIndex < 0)
            score += ".00";
        else {
            twoDecimals = score.Substring(dotIndex + 1);
            if(twoDecimals.Length <= 1)
                score += "0";
        }

        score += " pts";

        return score;
    }

    float DrawPointAndReturnMiss(Vector3 mousePosition)
    {
        point = Instantiate(pointPrefab);
        point.transform.position = MyMath.ScreenToWorldPoint(mousePosition, true);
        point.transform.localScale = Vector3.one * MyMath.ZoomScale(point.transform.localScale.x, defaultCameraSize, Camera.main.orthographicSize);
        DrawCurrentTown();
        DrawLine(point.transform.position, currentTown.position, 1f);
        float distanceOff = MyMath.DistanceBetweenTowns(point.transform.position, currentTown.position);
        return distanceOff;
    }

    void DrawCurrentTown()
    {
        townPoint = Instantiate(townPointPrefab);
        townPoint.transform.position = currentTown.position;
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

    public void SubmitScore()
    {
        if(score < PlayerPrefs.GetFloat("score"))
            PlayerPrefs.SetFloat("score", score);

        string name = instructionManager.GetHighscoreName();
        if(name == "") {
            instructionManager.ChangeHighscorePlaceholder("What is your name?");
        }
        else {
            StartCoroutine("PostScore", name);
        }
    }

    public void ScoreSubmited()
    {
        if(submitedOnce) return;

        if(score < PlayerPrefs.GetFloat("score"))
            PlayerPrefs.SetFloat("score", score);

        string name = instructionManager.GetHighscoreName();
        if(name == "") {
            instructionManager.ChangeHighscorePlaceholder("What is your name?");
        } else {
            StartCoroutine("PostScore", name);
        }

        submitedOnce = true;
    }

    IEnumerator PostScore(string name)
    {
        WWW result = GetComponent<ServerScript>().PostScore(
            name, System.DateTime.Now.ToString(), PlayerPrefs.GetFloat("score"), ""
        );

        while(true) {
            if(result.isDone) {
                if(result.error == null) {
                    instructionManager.ChangeTown("Success.");
                } else {
                    instructionManager.ChangeTown("Fail.");
                }

                StopCoroutine("PostScore");
            }
            yield return null;
        }
    }

    void DrawLine(Vector3 p1, Vector3 p2, float precision)
    {
        if(precision < 0.001f) precision = 0.001f;
        precision = MyMath.ZoomScale(precision, defaultCameraSize, Camera.main.orthographicSize);
        float x0 = p1.x, y0 = p1.y, 
            steps = Vector2.Distance(p1, p2) / precision, 
            rx = p1.x-p2.x, 
            ry = p1.y-p2.y, 
            x1, y1;
        for(int i=0; i<steps; i++) {
            x1 = x0 - rx * (i / steps);
            y1 = y0 - ry * (i / steps);
            GameObject p = Instantiate(pointPrefab);
            p.transform.position = new Vector3(x1, y1, p1.z);
            p.transform.localScale = Vector3.one * MyMath.ZoomScale(3f, defaultCameraSize, Camera.main.orthographicSize);
            p.transform.SetParent(transform.GetChild(0));
        }
    }

    void ClearLine()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for(int i=2; i<children.Length; i++)
            Destroy(children[i].gameObject);
    }

    void EndGame()
    {
        TownManager.GetNextTown();
        Destroy(point);
        Destroy(townPoint);
        ClearLine();
        instructionManager.ShowHighscore();
    }
}
