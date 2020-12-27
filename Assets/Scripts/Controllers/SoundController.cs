using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip itemSound;
    public AudioClip answerCorrect;
    public AudioClip answerWrong;
    public AudioClip rainSound;
    public AudioClip splashSound;
    public AudioClip carSound;
    public AudioSource audio;
    public GameObject persistent;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        persistent = GameObject.FindGameObjectWithTag("Persistent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound(string soundType)
    {
        if (persistent != null && persistent.GetComponent<MusicController>().audioEnabled)
        {



            if (soundType == "itemSound")
            {
                audio.PlayOneShot(itemSound);
            }
            else if (soundType == "answerCorrect")
            {
                audio.PlayOneShot(answerCorrect);
            }
            else if (soundType == "answerWrong")
            {
                audio.PlayOneShot(answerWrong);
            }
            else if (soundType == "rainSound")
            {
                audio.PlayOneShot(rainSound);
            }
            else if (soundType == "splashSound")
            {
                audio.PlayOneShot(splashSound);
            }
            else if (soundType == "carSound")
            {
                audio.PlayOneShot(carSound, 0.5f);
            }
        }
    }

    public void stopSound(string soundType)
    {
        if (soundType == "rainSound")
        {
            audio.Stop();
        }
    }
}
