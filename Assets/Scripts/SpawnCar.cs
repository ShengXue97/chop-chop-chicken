using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    [SerializeField]
    public bool spawnRight;
    public GameObject car1;
    public GameObject car2;
    private float currentTime;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var shouldSpawn = Random.Range(0, 999);
        var carType = Random.Range(0, 2);
        //1/3 probability every frame of spawning a car
        if (shouldSpawn == 0 && Time.time >= currentTime + 0.2f)
        {
            //Only allow spawning once in a while to prevent car collision
            currentTime = Time.time;
            Vector3 newPos = transform.position;
            newPos.z -= 2f;
            if (carType == 0 && spawnRight)
            {
                GameObject car = Instantiate(car1, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, 90, 0);
                car.GetComponent<VehicleMovement>().movingRight = true;
            }
            else if (carType == 0 && !spawnRight)
            {
                GameObject car = Instantiate(car1, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, -90, 0);
                car.GetComponent<VehicleMovement>().movingRight = false;
            }
            else if (carType == 1 && spawnRight)
            {
                GameObject car = Instantiate(car2, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, 90, 0);
                car.GetComponent<VehicleMovement>().movingRight = true;
            }
            else if (carType == 1 && !spawnRight)
            {
                GameObject car = Instantiate(car2, newPos, Quaternion.identity);
                car.transform.rotation = Quaternion.Euler(0, -90, 0);
                car.GetComponent<VehicleMovement>().movingRight = false;
            }
        }
    }
}
