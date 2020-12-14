using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public GameObject grass1;
    public GameObject grass2;
    public GameObject pavement;
    public GameObject river1;
    public GameObject river2;
    public GameObject road1;
    public GameObject road2;
    public GameObject tree1;
    public int prevGrass;
    public int prevRoad;
    public int prevRiver;

    // Start is called before the first frame update
    void Start()
    {
        float currentZ = 0f;
        for (float i = 0; i < 1000; i++)
        {
            //Spawn 20 different types of blocks in total
            int blockType;

            if (i <= 10)
            {
                //No river and road for the first 10 blocks
                blockType = 0;
            }
            else
            {
                blockType = Random.Range(0, 3);
            }

            if (blockType == 0)
            {
                //Spawn grass
                //Random number of vertical grass blocks
                var blockLength = Random.Range(0, 3);
                for (float j = 0; j < blockLength; j++)
                {
                    GameObject grass;
                    //Alternate between lighter and darker shades of grass
                    if (prevGrass == 0)
                    {
                        prevGrass = 1;
                        grass = grass1;
                    }
                    else
                    {
                        prevGrass = 0;
                        grass = grass2;
                    }

                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Spawn 20 blocks horizontally
                        GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                        grassObj.transform.parent = gameObject.transform;

                        var shouldSpawnTree = Random.Range(0, 10);
                        if (shouldSpawnTree == 0)
                        {
                            GameObject treeObj = Instantiate(tree1, new Vector3(x, 0f, currentZ), Quaternion.identity);
                            treeObj.transform.parent = gameObject.transform;
                        }

                    }
                    currentZ += 2f;
                }
            }

            else if (blockType == 1)
            {
                //Spawn road
                //Random number of vertical road blocks
                var blockLength = Random.Range(0, 3);
                for (float j = 0; j < blockLength; j++)
                {
                    var canSpawn = Random.Range(0, 2);
                    GameObject road;
                    //Alternate between lighter and darker shades of grass
                    if (prevRoad == 0)
                    {
                        prevRoad = 1;
                        road = road1;
                    }
                    else
                    {
                        prevRoad = 0;
                        road = road2;
                    }

                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Create roads responsible for spawning cars first

                        if (x == -20)
                        {
                            GameObject roadObj = Instantiate(road, new Vector3(x, -2f, currentZ), Quaternion.Euler(new Vector3(0, -90, 0)));
                            roadObj.transform.parent = gameObject.transform;
                            roadObj.GetComponent<SpawnCar>().spawnRight = true;
                            if (canSpawn == 0)
                            {
                                roadObj.GetComponent<SpawnCar>().enabled = false;
                            }
                        }
                        else if (x > (30f - 1.9f))
                        {
                            GameObject roadObj = Instantiate(road, new Vector3(x, -2f, currentZ), Quaternion.Euler(new Vector3(0, -90, 0)));
                            roadObj.transform.parent = gameObject.transform;
                            roadObj.GetComponent<SpawnCar>().spawnRight = false;
                            if (canSpawn == 1)
                            {
                                roadObj.GetComponent<SpawnCar>().enabled = false;
                            }
                        }
                        else
                        {
                            //Spawn 20 blocks horizontally
                            GameObject roadObj = Instantiate(road, new Vector3(x, -2f, currentZ), Quaternion.Euler(new Vector3(0, -90, 0)));
                            roadObj.transform.parent = gameObject.transform;
                            roadObj.GetComponent<SpawnCar>().enabled = false;
                        }
                    }
                    currentZ += 2f;
                }
            }

            else if (blockType == 2)
            {
                //Spawn river
                //Random number of vertical river blocks
                var blockLength = Random.Range(0, 3);
                for (float j = 0; j < blockLength; j++)
                {
                    GameObject river;
                    //Alternate between lighter and darker shades of grass
                    if (prevRiver == 0)
                    {
                        prevRiver = 1;
                        river = river1;
                    }
                    else
                    {
                        prevRiver = 0;
                        river = river2;
                    }
                    for (float x = -20; x < 30; x = x + 1.9f)
                    {
                        //Create rivers responsible for spawning logs first

                        if (x == -20)
                        {
                            GameObject riverObj = Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().spawnRight = true;
                            if (prevRiver == 0)
                            {
                                riverObj.GetComponent<SpawnRiver>().canSpawn = true;
                            }
                        }
                        else if (x > (30f - 1.9f))
                        {
                            GameObject riverObj = Instantiate(river, new Vector3(x, 0, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().spawnRight = false;
                            if (prevRiver == 1)
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
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
