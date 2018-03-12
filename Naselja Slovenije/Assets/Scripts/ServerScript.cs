using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    string postURL = "http://pigeon.eu.org/SloMap/post.php";
    string highscoreURL = "http://pigeon.eu.org/SloMap/getHighscore.php";

    public WWW PostScore(string Name, string Date, float Score, string Info)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Name);
        form.AddField("Date", Date);
        form.AddField("Score", Score.ToString());
        form.AddField("Info", Info);
        WWW www = new WWW(postURL, form);

        return www;
    }

    public WWW GetHighscore()
    {
        return new WWW(highscoreURL);
    }
}