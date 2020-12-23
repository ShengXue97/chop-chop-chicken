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

public class PlayerInfo
{
    public string Name;
    public string Email;
    public string Score;
    public string id;
}
public class AzureControl : MonoBehaviour
{
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

        //BlobStorageTest ();
        //StartDownload("score.txt");
        StartCoroutine(Download());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callUpdate(string name, string email, string score)
    {
        StartCoroutine(updateUser(name, email, score));
    }

    IEnumerator updateUser(string name, string email, string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginName", name);
        form.AddField("loginEmail", email);
        form.AddField("loginScore", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/chop/UpdateUsers.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error);
            }
            else
            {
                print("Form update complete!");
                print(www.downloadHandler.text);
            }
        }
    }

    IEnumerator Download()
    {
        Debug.Log("hii");
        WWWForm form = new WWWForm();
        form.AddField("loginName", "1");
        form.AddField("loginEmail", "1");
        form.AddField("loginScore", "1");

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/chop/GetUsers.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print("damn error..." + www.error);
            }
            else
            {
                print("Form upload complete!");
                print(www.downloadHandler.text);
                //GetComponent<RecyclableScrollerDemo>().InitData(www.downloadHandler.text);
            }
        }
    }

    public void StartUpload(string dest, string text)
    {
        StartCoroutine(AzureUpload(dest, text));
    }

    public void StartUpdate(string dest, int score, string name, string email)
    {
        StartCoroutine(AzureUpdate(dest, score, name, email));
    }

    public void StartDownload(string dest)
    {
        StartCoroutine(AzureDownload(dest));
    }

    private IEnumerator AzureDownload(string dest)
    {
        result = "";

        // Download a blob to your file system

        StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

        CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


        CloudBlobContainer container = blobClient.GetContainerReference("ccc");

        CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);
        //print ("Downloading" + dest);

        try
        {
            string text = blockBlob.DownloadText();
            result = text;
            print("Downloaded: " + text);
            GetComponent<RecyclableScrollerDemo>().InitData(text);

        }
        catch (StorageException ex)
        {
            print("Error downloading");

        }
        yield break;
    }

    private IEnumerator AzureUpdate(string dest, int score, string name, string email)
    {
        result = "";

        // Download a blob to your file system

        StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

        CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


        CloudBlobContainer container = blobClient.GetContainerReference("ccc");

        CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);
        //print ("Downloading" + dest);

        try
        {
            string text = blockBlob.DownloadText();
            result = text;
            print("Downloaded: " + text);

            string[] users = text.Split(';');
            bool foundUser = false;
            if (_playerList != null) _playerList.Clear();

            for (int i = 0; i < users.Length; i++)
            {
                string user = users[i];
                string[] details = user.Split(',');

                PlayerInfo obj = new PlayerInfo();
                obj.Name = details[0];
                obj.Email = details[1];
                obj.Score = details[2];
                //Debug.Log(obj.Name + ";" + name + ";" + obj.Email + ";" + email);

                if (obj.Name == name && obj.Email == email)
                {
                    //Update player score
                    int oldScore = int.Parse(obj.Score);
                    if (score > oldScore)
                    {
                        obj.Score = score.ToString();
                    }
                    foundUser = true;
                }

                obj.id = "";
                _playerList.Add(obj);
            }

            if (!foundUser)
            {
                foundUser = true;
                PlayerInfo obj = new PlayerInfo();
                obj.Name = name;
                obj.Email = email;
                obj.Score = score.ToString();
                _playerList.Add(obj);
            }

            string finalData = "";

            for (int i = 0; i < _playerList.Count; i++)
            {
                PlayerInfo player = _playerList[i];
                string playerName = player.Name;
                string playerEmail = player.Email;
                string playerScore = player.Score;
                //Debug.Log(playerName + ";" + playerEmail + ";" + playerScore);

                if (i == _playerList.Count - 1)
                {
                    finalData += playerName + "," + playerEmail + "," + playerScore;
                }
                else
                {
                    finalData += playerName + "," + playerEmail + "," + playerScore + ";";
                }
            }

            StartUpload(dest, finalData);

        }
        catch (StorageException ex)
        {
            print("Error downloading");

        }
        yield break;
    }
    private IEnumerator AzureUpload(string dest, string text)
    {
        result = "";

        // Create a blob client for interacting with the blob service.
        StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

        CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


        CloudBlobContainer container = blobClient.GetContainerReference("ccc");

        CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);
        //print ("Uploading" + dest + ";" + text);
        try
        {
            blockBlob.UploadText(text);
            print("Uploaded");
        }

        catch (StorageException)
        {
            print("Error uploading");
        }

        yield break;
    }



    private string ConvertLastModifiedToSecocnds(string lastmodified)
    {
        string oneline = lastmodified.Split('\n')[0];
        string onlytime = oneline.Split(' ')[1];
        var timesplit = onlytime.Split(':');

        int hour = int.Parse(timesplit[0]);
        int minute = int.Parse(timesplit[1]);
        int second = int.Parse(timesplit[2]);

        string onlydate = oneline.Split(' ')[0];
        var datesplit = onlydate.Split('/');

        int day = int.Parse(datesplit[0]);
        int month = int.Parse(datesplit[1]);
        int year = int.Parse(datesplit[2]);

        string finalSeconds = (second + (minute * 60) + (hour * 3600) + (day * 86400) + (month * 2.628e+6) + (year * 3.154e+7)).ToString();
        return finalSeconds;
    }
}
