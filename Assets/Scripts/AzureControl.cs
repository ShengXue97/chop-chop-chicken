using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using UnityEngine;
using Random = System.Random;
using System.Collections;
public class AzureControl : MonoBehaviour
{
    public CloudStorageAccount StorageAccount;
    public string value;
    public string result = "";
    // Use this for initialization
    void Start()
    {
        //BlobStorageTest ();
        StartDownload("score.txt");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartUpload(string dest, string text)
    {
        StartCoroutine(AzureUpload(dest, text));
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
            result = "true";
            //print("Uploaded");
        }
        catch (StorageException)
        {
            result = "false";
            //print ("Error uploading");
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
