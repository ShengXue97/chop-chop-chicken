using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    GameObject controller;
    GameObject persistent;
    public int currentRain;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.FindGameObjectWithTag("Controller");
        persistent = GameObject.FindGameObjectWithTag("Persistent");
        currentRain = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.transform.position;
        newPos.y -= 15f;
        gameObject.transform.position = newPos;
        if (controller.GetComponent<GameController>().currentRain == 0)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
            currentRain = 0;
        }
        else if (controller.GetComponent<GameController>().currentRain == 1)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
            currentRain = 1;
        }
        else if (controller.GetComponent<GameController>().currentRain == 2)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            currentRain = 2;
        }

    }
}
