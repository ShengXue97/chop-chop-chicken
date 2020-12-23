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
        string uri = "http://dreamlo.com/lb/5fe2bf960af26915282d8434/quote";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                GetComponent<RecyclableScrollerDemo>().InitData(webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator UpdateRequest(string name, string email, int score)
    {
        string uri = "http://dreamlo.com/lb/dnjsX4qPwUWMxQSder0MWgkXS3uYYPRkyT6qpg_FZU4A/add/" + name + "/" + score.ToString() + "/0/" + email;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
