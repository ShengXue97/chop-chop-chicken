using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int currentRain;
    private float currentTime = -999999f;
    public Dictionary<string, bool> userAnswers = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        int chance = Random.Range(0, 10);
        if (chance < 3)
        {
            currentRain = 1;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "TutorialGame")
            {
                GetComponent<SoundController>().playSound("rainSound");
            }
        }
        else
        {
            currentRain = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= currentTime + 1f)
        {
            //Check if should change rain status every 10 seconds
            if (currentRain == 0)
            {
                currentTime = Time.timeSinceLevelLoad;
                //Can start raining 
                int chance = Random.Range(0, 30);
                if (chance < 2)
                {
                    currentRain = 1;
                    if (GetComponent<SoundController>() != null)
                    {
                        GetComponent<SoundController>().playSound("rainSound");
                    }
                }
            }
            else if (currentRain == 1 && Time.timeSinceLevelLoad >= currentTime + 20f)
            {
                currentTime = Time.timeSinceLevelLoad;
                //More likely to rain heavier if it is already raining
                int chance = Random.Range(0, 10);
                if (chance < 3)
                {
                    currentRain = 0;
                    if (GetComponent<SoundController>() != null)
                    {
                        GetComponent<SoundController>().playSound("rainSound");
                    }
                }
                else
                {
                    currentRain = 2;
                }
            }
            else if (currentRain == 2 && Time.timeSinceLevelLoad >= currentTime + 20f)
            {
                currentTime = Time.timeSinceLevelLoad;
                //More likely to stop raining than to keep raining
                int chance = Random.Range(0, 10);
                if (chance < 3)
                {
                    currentRain = 2;
                }
                else
                {
                    currentRain = 0;
                    GetComponent<SoundController>().stopSound("rainSound");

                }
            }


        }
    }
}
