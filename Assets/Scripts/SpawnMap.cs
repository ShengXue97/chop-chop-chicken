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
                    for (float x = -5; x < 20; x = x + 1.9f)
                    {
                        //Spawn 20 blocks horizontally
                        Instantiate(grass, new Vector3(x, -0.1f, currentZ), Quaternion.identity);
                    }
                    currentZ += 2f;
                }
            }
            else if (blockType == 1)
            {
                //Spawn pavement
                //Random number of vertical pavement blocks
                var blockLength = Random.Range(0, 5);
                for (float j = 0; j < blockLength; j++)
                {
                    for (float x = -5; x < 20; x = x + 1.9f)
                    {
                        //Spawn 20 blocks horizontally
                        Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
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
                    for (float x = -5; x < 20; x = x + 1.9f)
                    {
                        //Create roads responsible for spawning cars first

                        if (x == -5)
                        {
                            GameObject road = Instantiate(road2, new Vector3(x, 0, currentZ + 2f), Quaternion.identity);
                            road.GetComponent<SpawnCar>().spawnRight = true;
                            if (canSpawn == 0)
                            {
                                road.GetComponent<SpawnCar>().enabled = false;
                            }
                        }
                        else if (x > 17)
                        {
                            GameObject road = Instantiate(road2, new Vector3(x, 0, currentZ + 2f), Quaternion.identity);
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
                            road.GetComponent<SpawnCar>().enabled = false;
                        }
                        Instantiate(road1, new Vector3(x, 0, currentZ), Quaternion.Euler(new Vector3(0, -90, 0)));
                    }
                    currentZ += 2f;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
