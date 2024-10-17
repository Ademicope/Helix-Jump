using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    const string privateCode = "Yg78X-U1NU6GXGRO19a7GQZIWLs3vzQUCIOGc2GsJYJg";
    const string publicCode = "67111e008f40bb122c7c4777";
    const string webURL = "http://dreamlo.com/lb/Yg78X-U1NU6GXGRO19a7GQZIWLs3vzQUCIOGc2GsJYJg";

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload successfull");
        }
        else
        {
            print("Error Uploading: " + www.error);
        }
    }
}
