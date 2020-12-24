using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public bool movingRight;
    GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -40 || transform.position.x > 45)
        {
            //Outside of screen, destroy!
            Destroy(gameObject);
        }

        float vehicleSpeed = 10f + Mathf.FloorToInt((transform.position.z / 100)) * 2;
        if (controller.GetComponent<SpawnMap>().currentRain == 1)
        {
            //vehicle moves slower when it is raining
            vehicleSpeed -= 3f;
        }
        else if (controller.GetComponent<SpawnMap>().currentRain == 2)
        {
            //vehicle moves slower when it is raining
            vehicleSpeed -= 7f;
        }

        float yValue = 1f;
        if (movingRight)
        {
            yValue = 1f;
        }
        else
        {
            yValue = -1f;
        }

        Vector3 newPos = gameObject.transform.position;
        newPos.x += yValue;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPos, Time.deltaTime * vehicleSpeed);
    }
}
