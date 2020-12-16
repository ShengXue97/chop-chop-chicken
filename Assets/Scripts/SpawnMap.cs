using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public GameObject player;
    public GameObject grass1;
    public GameObject grass2;
    public GameObject pavement;
    public GameObject river1;
    public GameObject river2;
    public GameObject road1;
    public GameObject road2;
    public GameObject tree1;
    public GameObject questionText;
    public GameObject answerText;

    public GameObject carrot;
    public int prevGrass;
    public int prevRoad;
    public int prevRiver;
    private float currentZ;
    private int currentRow;
    public string[] mapList = new string[90];
    // Start is called before the first frame update
    void Start()
    {
        currentZ = 0f;
        currentRow = 0;
        for (int i = 0; i < mapList.Length; i++)
        {
            int blockType;

            if (i <= 20)
            {
                //first 20 blocks
                blockType = 0;
            }
            else if (i <= 50)
            {
                //next 30 blocks
                blockType = Random.Range(0, 2);
            }
            else
            {
                //next 40 blocks
                blockType = Random.Range(0, 3);
            }

            //Populates map list with predefined blocks
            switch (blockType)
            {
                case 0:
                    mapList[i] = "grass";
                    break;
                case 1:
                    mapList[i] = "road";
                    break;
                case 2:
                    mapList[i] = "river";
                    break;
                default:
                    mapList[i] = "grass";
                    break;
            }
        }
        spawnNextBatch();
    }

    // Update is called once per frame
    void Update()
    {
        //Times 2 because each block is 2f long in z axis
        int zPos = player.GetComponent<CharacterMovement>().zPos * 2;
        if (zPos > currentZ - 80)
        {
            spawnNextBatch();
        }
        // Debug.Log(zPos + ";" + currentZ);
        //Debug.Log(zPos);
    }

    void spawnNextBatch()
    {
        //Only spawn tiles when player is nearby
        for (int i = 0; i < 100; i++)
        {
            string blockType = "";
            if ((currentRow % 30) == 0 || (currentRow % 30) == 29)
            {
                //Spawn question every 30 tiles
                blockType = "grass";
            }
            else if (currentRow < mapList.Length)
            {
                blockType = mapList[currentRow];
            }
            else
            {
                blockType = getRandomRow();
            }

            switch (blockType)
            {
                case "grass":
                    spawnGrassRow();
                    break;
                case "road":
                    spawnRoadRow();
                    break;
                case "river":
                    spawnRiverRow();
                    break;
                default:
                    spawnGrassRow();
                    break;
            }

            currentRow++;
        }
    }

    string getRandomRow()
    {
        int blockType = Random.Range(0, 3);
        string chosenRow = "";

        //Populates map list with predefined blocks
        switch (blockType)
        {
            case 0:
                chosenRow = "grass";
                break;
            case 1:
                chosenRow = "road";
                break;
            case 2:
                chosenRow = "river";
                break;
            default:
                chosenRow = "grass";
                break;
        }
        return chosenRow;
    }

    void spawnGrassRow()
    {
        var canSpawn = Random.Range(0, 2);
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
            if (x == -20)
            {
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().spawnRight = true;
                if (canSpawn == 0)
                {
                    grassObj.GetComponent<SpawnFood>().enabled = false;
                }
                if ((currentZ / 2) % 30 == 0)
                {
                    //Spawn questions every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;

                    GameObject questionTextObj = Instantiate(questionText, new Vector3(x + 53.9f, -4.6f, currentZ + 6.5f), Quaternion.identity);
                    questionTextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                    questionTextObj.transform.SetParent(gameObject.transform, false);
                }
                else if ((currentZ / 2) % 30 == 29)
                {
                    //Spawn answers every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;

                    GameObject answer1TextObj = Instantiate(answerText, new Vector3(x + 53.9f, -4.6f, currentZ + 6.5f), Quaternion.identity);
                    answer1TextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                    answer1TextObj.transform.SetParent(gameObject.transform, false);

                    GameObject answer2TextObj = Instantiate(answerText, new Vector3(x + 83.9f, -4.6f, currentZ + 6.5f), Quaternion.identity);
                    answer2TextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                    answer2TextObj.transform.SetParent(gameObject.transform, false);
                }
            }
            else if (x > (30f - 1.9f))
            {
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().spawnRight = false;
                if (canSpawn == 1)
                {
                    grassObj.GetComponent<SpawnFood>().enabled = false;
                }

                if ((currentZ / 2) % 30 == 0)
                {
                    //Spawn questions every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;
                }
                else if ((currentZ / 2) % 30 == 29)
                {
                    //Spawn answers every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;
                }
            }
            else
            {
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().enabled = false;

                if ((currentZ / 2) % 30 == 0)
                {
                    //Spawn questions every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;
                }
                else if ((currentZ / 2) % 30 == 29)
                {
                    //Spawn answers every 30 tiles
                    GameObject grassTopObj = Instantiate(grass, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    grassTopObj.transform.parent = gameObject.transform;
                }
            }

            var shouldSpawnTree = Random.Range(0, 10);
            if (shouldSpawnTree == 0)
            {
                GameObject treeObj = Instantiate(tree1, new Vector3(x, 0f, currentZ), Quaternion.identity);
                treeObj.transform.parent = gameObject.transform;
            }
            else
            {
                var shouldSpawnCarrot = Random.Range(0, 50);
                if (shouldSpawnCarrot == 0)
                {
                    GameObject carrotObj = Instantiate(carrot, new Vector3(x, 0.22f, currentZ), Quaternion.identity);
                    carrotObj.transform.rotation = Quaternion.Euler(-6, -90, 0);
                    carrotObj.transform.parent = gameObject.transform;
                }
            }

        }
        currentZ += 2f;
    }

    void spawnRoadRow()
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

    void spawnRiverRow()
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

