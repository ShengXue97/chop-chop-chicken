using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public string name;
    public string email;
    public UnityEngine.UI.Text tempName;
    public UnityEngine.UI.Text tempEmail;
    public int highscore;

    public TextMeshProUGUI welcomeText;
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
        PlayerPrefs.SetString("myname", tempName.text);
        PlayerPrefs.SetString("myemail", tempEmail.text);
        name = tempName.text;
        email = tempEmail.text;
        welcomeText.text = "Welcome back, " + tempName.text;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
