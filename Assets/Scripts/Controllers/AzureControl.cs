using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using UnityEngine;
using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerInfo
{
    public string Name;
    public string Email;
    public string Score;
    public string id;
}

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        //Simply return true no matter what
        return true;
    }
}

public class AzureControl : MonoBehaviour
{
    public string path;
    public CloudStorageAccount StorageAccount;
    public string value;
    public string result = "";
    private List<PlayerInfo> _playerList = new List<PlayerInfo>();
    public GameObject MyProfile;
    public GameObject VerticalCell;
    // Use this for initialization
    void Start()
    {
        GameObject[] persistents = GameObject.FindGameObjectsWithTag("Persistent");
        if (persistents.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("myname") && !PlayerPrefs.HasKey("myemail"))
        {
            MyProfile.SetActive(true);
            Camera.main.GetComponent<StartGame>().isTutorial = true;
        }
        else
        {
            Camera.main.GetComponent<StartGame>().isTutorial = false;
        }
        getLeaderboard();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "HomeScreen")
            {
                GameObject MyProfile = GameObject.FindGameObjectWithTag("MyProfile");
                if (MyProfile == null)
                {
                    SceneManager.LoadScene("MainGame");
                }
            }
            else if (scene.name == "MainGame")
            {
                GameObject CommentPanel = GameObject.FindGameObjectWithTag("CommentPanel");
                if (CommentPanel == null)
                {
                    SceneManager.LoadScene("MainGame");
                }
            }
            else if (scene.name == "TutorialGame")
            {
                SceneManager.LoadScene("TutorialGame");
            }
        }
    }

    public void getLeaderboard()
    {
        StartCoroutine(GetRequest());
    }

    public void callUpdate(string name, string email, int score)
    {
        StartCoroutine(UpdateRequest(name, email, score));
    }

    public void callFeddback(string name, string email, string feedback)
    {
        StartCoroutine(UpdateFeedback(name, email, feedback));
    }


    IEnumerator GetRequest()
    {
        GameObject LeaderboardContent = GameObject.FindGameObjectWithTag("LeaderboardContent");
        LeaderboardContent.GetComponent<RecyclableScrollerDemo>().InitData("\"\"");
        string uri = "https://chop-chop-chicken.herokuapp.com/getusers";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("aError: " + webRequest.error);
                Debug.Log("tReceived: " + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("aReceived: " + webRequest.downloadHandler.text);
                string data = webRequest.downloadHandler.text;
                if (LeaderboardContent.GetComponent<RecyclableScrollerDemo>() != null)
                {
                    LeaderboardContent.GetComponent<RecyclableScrollerDemo>().InitData(data);
                }

            }
        }
    }

    IEnumerator UpdateRequest(string name, string email, int score)
    {
        string uri = "https://chop-chop-chicken.herokuapp.com/updateusers?name=" + name + "&email=" + email + "&score=" + score.ToString();

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("bError: " + webRequest.error);
            }
            else
            {
                Debug.Log("bReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator UpdateFeedback(string name, string email, string feedback)
    {
        string uri = "https://chop-chop-chicken.herokuapp.com/updatefeedback?name=" + name + "&email=" + email + "&feedback=" + feedback;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("cError: " + webRequest.error);
            }
            else
            {
                Debug.Log("cReceived: " + webRequest.downloadHandler.text);
            }
        }
    }


}
