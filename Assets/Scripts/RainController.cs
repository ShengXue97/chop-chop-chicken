using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<SpawnMap>().currentRain == 0)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        }
        else if (controller.GetComponent<SpawnMap>().currentRain == 1)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        }
        else if (controller.GetComponent<SpawnMap>().currentRain == 2)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        }

    }
}
