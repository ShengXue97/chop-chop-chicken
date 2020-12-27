using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public bool isTutorial = false;
    // Start is called before the first frame update
    public void startGame()
    {
        if (isTutorial)
        {
            SceneManager.LoadScene("TutorialGame");
        }
        else
        {
            SceneManager.LoadScene("MainGame");
        }
    }

}
