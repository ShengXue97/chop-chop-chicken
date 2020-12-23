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
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("myname") && !PlayerPrefs.HasKey("myemail"))
        {
            MyProfile.SetActive(true);
        }
        StartCoroutine(GetRequest());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callUpdate(string name, string email, int score)
    {
        StartCoroutine(UpdateRequest(name, email, score));
    }

    IEnumerator GetRequest()
    {
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
                GetComponent<RecyclableScrollerDemo>().InitData(webRequest.downloadHandler.text);
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
}
