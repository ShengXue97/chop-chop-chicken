﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnCar : MonoBehaviour
{
    GameObject player;
    public bool canSpawn;
    [SerializeField]
    public bool spawnRight;
    public GameObject car1;
    public GameObject car2;
    private float currentTime = -999999f;
    private MeshRenderer meshRenderer;
    public bool isVisible;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.z - transform.position.z) > 30f)
        {
            return;
        }

        if (!canSpawn)
        {
            return;
        }
        //var spawnTimer = Mathf.FloorToInt(Random.Range(0, 2f + Mathf.Max(0, (10f - transform.position.z / 200))));
        var shouldSpawn = 1;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainGame")
        {
            shouldSpawn = Random.Range(0, 100);
        }
        else
        {
            shouldSpawn = Random.Range(0, 150);
        }

        var carType = Random.Range(0, 2);
        //1/3 probability every frame of spawning a car
        if (shouldSpawn == 0 && Time.timeSinceLevelLoad >= currentTime + 1.2f)
        {
            //Only allow spawning once in a while to prevent car collision
            currentTime = Time.timeSinceLevelLoad;
            Vector3 newPos = transform.position;
            newPos.y += 2f;
            if (carType == 0 && spawnRight)
            {
                GameObject car = Instantiate(car1, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, 90, 0);
                car.transform.parent = gameObject.transform;
                car.GetComponent<VehicleMovement>().movingRight = true;
            }
            else if (carType == 0 && !spawnRight)
            {
                GameObject car = Instantiate(car1, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, -90, 0);
                car.transform.parent = gameObject.transform;
                car.GetComponent<VehicleMovement>().movingRight = false;
            }
            else if (carType == 1 && spawnRight)
            {
                GameObject car = Instantiate(car2, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, 90, 0);
                car.transform.parent = gameObject.transform;
                car.GetComponent<VehicleMovement>().movingRight = true;
            }
            else if (carType == 1 && !spawnRight)
            {
                GameObject car = Instantiate(car2, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, -90, 0);
                car.transform.parent = gameObject.transform;
                car.GetComponent<VehicleMovement>().movingRight = false;
            }
        }
    }
}
