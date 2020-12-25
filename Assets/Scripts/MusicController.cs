using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite musicOn;
    public Sprite musicOff;
    public GameObject musicButton;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void toggleMusic()
    {
        if (audio.isPlaying)
        {
            audio.Pause();
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            audio.Play();
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
    }
}
