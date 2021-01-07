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
using UnityEngine.UI;
using System.Security.Cryptography;
using System;
using System.Text;
using TMPro;

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
    public TMP_InputField TextPanel_Name;
    public TMP_InputField TextPanel_Email;
    public GameObject OnScreenKeyboard;
    public GameObject loading;
    public UnityEngine.UI.Text loadingText;
    public GameObject play;
    public string path;
    public CloudStorageAccount StorageAccount;
    public string value;
    public string result = "";
    private List<PlayerInfo> _playerList = new List<PlayerInfo>();
    public GameObject MyProfile;
    public TextMeshProUGUI profileText1;
    public TextMeshProUGUI profileText2;
    public GameObject VerticalCell;

    public List<string> questionList = new List<string>();

    public List<string> answer1List = new List<string>();

    public List<string> answer2List = new List<string>();

    public List<string> correctList = new List<string>();

    public string link = "https://chop-chop-chicken-no-azure.herokuapp.com";

    // public string link = "https://chop-chop-chicken.herokuapp.com";

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
        callProfileText();
        callQuestions();

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
                GameObject FinishPanel = GameObject.FindGameObjectWithTag("FinishPanel");
                if (CommentPanel == null && FinishPanel != null && FinishPanel.activeSelf)
                {
                    SceneManager.LoadScene("MainGame");
                }
            }
            else if (scene.name == "TutorialGame")
            {
                GameObject FinishPanel = GameObject.FindGameObjectWithTag("FinishPanel");
                if (FinishPanel != null)
                {
                    SceneManager.LoadScene("TutorialGame");
                }
            }
        }

        // if (TextPanel_Name != null)
        if (TextPanel_Name != null && (Application.isMobilePlatform || SystemInfo.deviceModel.Contains("iPad")))
        {
            if (TextPanel_Name.isFocused)
            {
                OnScreenKeyboard.SetActive(true);
                OnScreenKeyboard.GetComponent<KeyboardScript>().setTextField(TextPanel_Name);
            }
            else if (TextPanel_Email.isFocused)
            {
                OnScreenKeyboard.SetActive(true);
                OnScreenKeyboard.GetComponent<KeyboardScript>().setTextField(TextPanel_Email);
            }
        }
    }

    public void getLeaderboard()
    {
        StartCoroutine(GetRequest());
    }

    public void callProfileText()
    {
        StartCoroutine(GetProfileText());
    }

    public void callQuestions()
    {
        StartCoroutine(GetQuestions());
    }

    public void callUpdate(string name, string email, int score)
    {
        StartCoroutine(UpdateRequest(name, email, score));
    }

    public void callFeddback(string name, string email, string feedback)
    {
        StartCoroutine(UpdateFeedback(name, email, feedback));
    }

    IEnumerator GetQuestions()
    {
        string uri = link + "/getquestions";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("aError: " + webRequest.error);
                Debug.Log("tReceived: " + webRequest.downloadHandler.text);
                loadingText.text = "Error occured. Please refresh.";
            }
            else
            {
                // Debug.Log("aReceived: " + webRequest.downloadHandler.text);
                string data = webRequest.downloadHandler.text;
                data = data.Substring(1, data.Length - 3);
                byte[] decodedBytes = Convert.FromBase64String(data);
                string decodedText = Encoding.UTF8.GetString(decodedBytes);

                string[] dataSplit = decodedText.Split('|');
                for (int i = 0; i < dataSplit.Length; i++)
                {
                    string[] detailSplit = dataSplit[i].Split(';');
                    for (int j = 0; j < detailSplit.Length; j++)
                    {
                        string detail = detailSplit[j];
                        if (j == 0)
                        {
                            //question list
                            questionList.Add(detail);
                        }
                        else if (j == 1)
                        {
                            //question list
                            answer1List.Add(detail);
                        }
                        else if (j == 2)
                        {
                            //question list
                            answer2List.Add(detail);
                        }
                        else if (j == 3)
                        {
                            //question list
                            correctList.Add(detail);
                        }

                    }
                }

                loading.SetActive(false);
                play.SetActive(true);

            }
        }
    }

    IEnumerator GetProfileText()
    {
        string uri = link + "/getprofile";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.certificateHandler = new BypassCertificate();

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("aError: " + webRequest.error);
                Debug.Log("tReceived: " + webRequest.downloadHandler.text);
                if (profileText1 != null && profileText2 != null)
                {
                    profileText1.text = "Leave us your email address if you'd like to have a chance to receive a surprise from us!";
                    profileText2.text = "Use the arrow keys to move around and answer the questions correctly to score more! Chop! Chop! Now!";
                }
            }
            else
            {
                // Debug.Log("aReceived: " + webRequest.downloadHandler.text);
                string data = webRequest.downloadHandler.text;
                if (data == "\"\"\n" || data == "\"\"")
                {
                    if (profileText1 != null && profileText2 != null)
                    {
                        profileText1.text = "Leave us your email address if you'd like to have a chance to receive a surprise from us!";
                        profileText2.text = "Use the arrow keys to move around and answer the questions correctly to score more! Chop! Chop! Now!";
                    }
                }
                else
                {
                    data = data.Substring(1, data.Length - 3);
                    byte[] decodedBytes = Convert.FromBase64String(data);
                    string decodedText = Encoding.UTF8.GetString(decodedBytes);

                    string[] profileSplit = decodedText.Split(';');
                    if (profileText1 != null && profileText2 != null)
                    {
                        profileText1.text = profileSplit[0];
                        profileText2.text = profileSplit[1];
                    }
                }

            }
        }
    }
    IEnumerator GetRequest()
    {
        GameObject LeaderboardContent = GameObject.FindGameObjectWithTag("LeaderboardContent");
        LeaderboardContent.GetComponent<RecyclableScrollerDemo>().InitData("\"\"");
        string uri = link + "/getusers";
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
                // Debug.Log("aReceived: " + webRequest.downloadHandler.text);
                string data = webRequest.downloadHandler.text;
                data = data.Substring(1, data.Length - 3);
                byte[] decodedBytes = Convert.FromBase64String(data);
                string decodedText = Encoding.UTF8.GetString(decodedBytes);

                if (LeaderboardContent != null)
                {
                    LeaderboardContent.GetComponent<RecyclableScrollerDemo>().InitData(decodedText);
                }

            }
        }
    }

    IEnumerator UpdateRequest(string name, string email, int score)
    {
        string epoch = getEpochTime();
        if (email == "")
        {
            email = "-";
        }

        string message = name + ";" + email + ";" + score + ";" + epoch;

        string hmac = CreateToken(message, "u!1j^aSm5MdF9w@%peXcYY").Replace("+", "-");

        string uri = link + "/updateusers?name=" + name + "&email=" + email + "&score=" + score.ToString() + "&epoch=" + epoch + "&hmac=" + hmac;

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
        string uri = link + "/updatefeedback?name=" + name + "&email=" + email + "&feedback=" + feedback;

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

    private string CreateToken(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    private string getEpochTime()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int secondsSinceEpoch = (int)t.TotalSeconds;
        return secondsSinceEpoch.ToString();
    }


}
