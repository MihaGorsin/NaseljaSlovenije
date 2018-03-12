using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    string PHP_POST_URL = "http://pigeon.eu.org/SloMap/connection.php";

    public WWW PostScore(string Name, string Date, float Score, string Info)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Name);
        form.AddField("Date", Date);
        form.AddField("Score", Score.ToString());
        form.AddField("Info", Info);
        WWW www = new WWW(PHP_POST_URL, form);

        return www;
    }
}