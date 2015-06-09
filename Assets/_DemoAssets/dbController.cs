using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dbController : MonoBehaviour {
    public string secretKey = "UnityDbTestKey";
    public string name;
    public int score;
    public string[] top10Scores;
    public string saveScoreURL;
    public string loadScoreURL;
    public Text textObject;

	// Use this for initialization
	void Start () {
        Debug.Log("name = " + name + " score = " + score.ToString());
        Debug.Log("SaveScoreURL:" + saveScoreURL);
        Debug.Log("LoadScoreURL:" + loadScoreURL);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveScores()
    {
        Debug.Log("SaveScores");
        StartCoroutine(SaveScoresRoutine());
    }

    public void LoadScores()
    {
        Debug.Log("LoadScores");
        StartCoroutine(LoadScoresRoutine());
    }

    IEnumerator SaveScoresRoutine()
    {
        string requestURL = saveScoreURL + "newName=" + WWW.EscapeURL(name) + "&newScore=" + score;
        Debug.Log("requestURL = " + requestURL);


        WWW webRequest = new WWW(requestURL);

		yield return webRequest;
		
		if (webRequest.error != null) {
			Debug.Log ("Save request error: " + webRequest.error);
		} else {
			Debug.Log(webRequest.ToString());
		}
    }

    IEnumerator LoadScoresRoutine()
    {
        textObject.text = "Loading Scores ...";

        WWW webRequest = new WWW(loadScoreURL);

        yield return webRequest;

        if (webRequest.error != null)
        {
            Debug.Log("Load request error: " + webRequest.error);
        }
        else
        {
			Debug.Log(webRequest.text);
            //display score on GUI
            textObject.text = webRequest.text;
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
