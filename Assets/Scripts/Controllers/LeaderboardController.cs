using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;


public class LeaderboardController : MonoBehaviour
{
    public const string MatchEmailPattern =
        @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public const string MatchAlphaNumeric = "^[a-zA-Z0-9 ]*$";

    public GameObject errorPanel;
    public UnityEngine.UI.Text errorText;
    public string name;
    public string email;

    public UnityEngine.UI.InputField tempName;
    public UnityEngine.UI.InputField tempEmail;
    public int highscore;

    public UnityEngine.UI.Text welcomeText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("myname"))
        {
            name = PlayerPrefs.GetString("myname");
            welcomeText.text = "Welcome back, " + name;
        }
        if (PlayerPrefs.HasKey("myemail"))
        {
            email = PlayerPrefs.GetString("myemail");
        }

    }


    public void updateDetails()
    {
        if (tempName.text == "")
        {
            errorPanel.SetActive(true);
            errorText.text = "Name cannot be empty!";
        }
        else if (!Regex.IsMatch(tempName.text, MatchAlphaNumeric))
        {
            errorPanel.SetActive(true);
            errorText.text = "Name must be alphanumeric!";
        }
        else if (tempName.text.Length > 25)
        {

            errorPanel.SetActive(true);
            errorText.text = "Name must be at most 25 characters!";
        }
        else if (!Regex.IsMatch(tempEmail.text, MatchEmailPattern) && tempEmail.text != "")
        {
            errorPanel.SetActive(true);
            errorText.text = "Please enter a valid email address!";
        }
        else
        {
            PlayerPrefs.SetString("myname", tempName.text);
            PlayerPrefs.SetString("myemail", tempEmail.text);
            name = tempName.text;
            email = tempEmail.text;
            welcomeText.text = "Welcome back, " + tempName.text;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
