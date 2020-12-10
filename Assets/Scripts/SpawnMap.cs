using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public GameObject grass;
    public GameObject pavement;
    public GameObject river;
    public GameObject road1;
    public GameObject road2;
    public GameObject tree;
    // Start is called before the first frame update
    void Start()
    {
        float currentZ = 0f;
        for (float i = 0; i < 1000; i++)
        {
            //Spawn 20 different types of blocks in total
            var blockType = Random.Range(0, 3);
            if (blockType == 0)
            {
                //Spawn grass
                //Random number of vertical grass blocks
                var blockLength = Random.Range(0, 5);
                for (float j = 0; j < blockLength; j++)
                {
                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Spawn 20 blocks horizontally
                        GameObject grassObj = Instantiate(grass, new Vector3(x, -0.1f, currentZ), Quaternion.identity);
                        grassObj.transform.parent = gameObject.transform;
                    }
                    currentZ += 2f;
                }
            }
            else if (blockType == 1)
            {
                //Spawn river
                //Random number of vertical river blocks
                var blockLength = Random.Range(0, 5);
                for (float j = 0; j < blockLength; j++)
                {
                    var canSpawn = Random.Range(0, 2);
                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Create rivers responsible for spawning logs first

                        if (x == -20)
                        {
                            GameObject riverObj = Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().spawnRight = true;
                            if (canSpawn == 0)
                            {
                                riverObj.GetComponent<SpawnRiver>().canSpawn = true;
                            }
                        }
                        else if (x > 27)
                        {
                            GameObject riverObj = Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().spawnRight = false;
                            if (canSpawn == 1)
                            {
                                riverObj.GetComponent<SpawnRiver>().canSpawn = true;
                            }
                        }
                        else
                        {
                            //Spawn 20 blocks horizontally
                            GameObject riverObj = Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().canSpawn = false;
                        }
                    }
                    currentZ += 2f;
                }
            }
            else if (blockType == 2)
            {
                //Spawn road
                //Random number of vertical road blocks
                var blockLength = Random.Range(0, 5);
                for (float j = 0; j < blockLength; j++)
                {
                    var canSpawn = Random.Range(0, 2);
                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Create roads responsible for spawning cars first

                        if (x == -20)
                        {
                            GameObject road = Instantiate(road2, new Vector3(x, 0, currentZ + 2f), Quaternion.identity);
                            road.transform.parent = gameObject.transform;
                            road.GetComponent<SpawnCar>().spawnRight = true;
                            if (canSpawn == 0)
                            {
                                road.GetComponent<SpawnCar>().enabled = false;
                            }
                        }
                        else if (x > 27)
                        {
                            GameObject road = Instantiate(road2, new Vector3(x, 0, currentZ + 2f), Quaternion.identity);
                            road.transform.parent = gameObject.transform;
                            road.GetComponent<SpawnCar>().spawnRight = false;
                            if (canSpawn == 1)
                            {
                                road.GetComponent<SpawnCar>().enabled = false;
                            }
                        }
                        else
                        {
                            //Spawn 20 blocks horizontally
                            GameObject road = Instantiate(road2, new Vector3(x, 0, currentZ + 2f), Quaternion.identity);
                            road.transform.parent = gameObject.transform;
                            road.GetComponent<SpawnCar>().enabled = false;
                        }
                        GameObject roadObj = Instantiate(road1, new Vector3(x, 0, currentZ), Quaternion.Euler(new Vector3(0, -90, 0)));
                        roadObj.transform.parent = gameObject.transform;
                    }
                    currentZ += 4f;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
