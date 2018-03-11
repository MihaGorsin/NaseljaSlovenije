using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    private string Name;
    private string Date;
    private int Score;
    private string Info;

    string PHP_POST_URL = "http://pigeon.eu.org/SloMap/connection.php";

    WWW www;
    int enkrat = 0;
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            PHP_POST_FORM(Name, Date, Score, Info);
        }
        if(enkrat == 1) {
            if(www.isDone) {
                Debug.Log(www.text);
                enkrat = 0;
            }
        }
        Debug.Log(www.text);
    }



    void PHP_POST_FORM(string Name, string Date, int Score, string Info)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Name);
        form.AddField("Date", Date);
        form.AddField("Score", Score);
        form.AddField("Info", Info);

        www = new WWW(PHP_POST_URL, form);
        Debug.Log("PHP form send");
        enkrat = 1;
        Debug.Log(www.error);
        
    }
}