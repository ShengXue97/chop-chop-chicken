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

        getData();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getData()
    {
        using (StreamWriter w = File.AppendText("scores.txt")) ;

        string path = "scores.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();
        reader.Close();
        print("Received: " + text);

        GetComponent<RecyclableScrollerDemo>().InitData(text);
    }

    public void updateData(string name, string email, int score)
    {
        string path = "scores.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();
        reader.Close();
        print("Received: " + text);
        bool foundUser = false;

        if (text != "")
        {
            string[] users = text.Split(';');

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

        // Write some text to the test.txt file
        File.WriteAllText(path, finalData);
    }
}
