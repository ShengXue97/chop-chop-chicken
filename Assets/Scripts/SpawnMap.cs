using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnMap : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public GameObject player;
    public GameObject grass1;
    public GameObject grass2;
    public GameObject bridge;
    public GameObject pavement;
    public GameObject river1;
    public GameObject river2;
    public GameObject road1;
    public GameObject road2;
    public GameObject tree1;
    public GameObject questionText;
    public GameObject answerText;

    public GameObject apple;
    public GameObject appleHalf;
    public GameObject banana;
    public GameObject beet;
    public GameObject bread;
    public GameObject broccoli;
    public GameObject cabbage;
    public GameObject carrot;
    public GameObject cauliflower;
    public GameObject cherries;
    public GameObject coconutHalf;
    public GameObject corn;
    public GameObject egg;
    public GameObject eggHalf;
    public GameObject fish;
    public GameObject grapes;
    public GameObject orange;
    public GameObject pepper;
    public GameObject pineapple;
    public GameObject pumpkin;
    public GameObject strawberry;
    public GameObject watermelon;
    public List<GameObject> foods;


    public int prevGrass;
    public int prevRoad;
    public int prevRiver;
    private float currentZ;
    private int currentRow;
    private int currentQuestion;
    public string[] mapList = new string[90];
    public List<string> questionList;

    public List<string> answer1List;

    public List<string> answer2List;

    public List<int> correctList;
    public Dictionary<string, bool> userAnswers = new Dictionary<string, bool>();

    public List<GameObject> decorations = new List<GameObject>();

    public List<int> excludeList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        foods = new List<GameObject>()
        { apple, appleHalf, banana, beet, bread, broccoli,
          cabbage, carrot, cauliflower, cherries, coconutHalf,
          corn, egg, eggHalf, fish, grapes, orange, pepper,
          pineapple, pumpkin, strawberry, watermelon
        };


        questionList = new List<string>() {
        "Which app can be used to submit a question to IT Care?",
        "What is the name of IT Care chatbot?",
        "Where can IT Care chatbot be found?",
        "Is there a website/portal to reset my password?",
        "I can perform a reset of my NUS-ID password via a self-service web portal",
        "Is there a website/portal to change my NUS-ID password?",
        "I can change my NUS-ID password via a self-service web portal",
        "What should I do if I forgot my NUS-ID password?",
        "What is the mobile app to access NUSafe and declare temperature?",
        "NUS IT recently deployed Trend Micro Email Security to help protect against advanced email borne threats.",
        "What should I do if I receive a suspected phishing email?",
        "I can configure 2FA soft token in more than one mobile device.",
        "Can I buy desktops and laptops from IT Care?",
        "I should _______ my password when the password is expired.",
        "What is the minimum length for NUS password?",
        "How often do I have to change my password?",
        "Which password provides better security?",
        "Which email address looks more suspicious?",
        "Which can I do in nTouch?",
        "What is the name of NUS IT mascot?"
        };

        answer1List = new List<string>() {
        "nTouch",
        "WikiITCare",
        "NUS IT website",
        "Yes",
        "True",
        "Yes",
        "True",
        "Reset via web portal",
        "uNivUS",
        "True",
        "Click report phishing",
        "Yes",
        "Yes",
        "Change",
        "12",
        "1 year",
        "12Dec1980Peter",
        "itcare@nus.edu.sg",
        "Submit IT request",
        "bITbIT",
        };

        answer2List = new List<string>() {
        "CallMeNow",
        "ALCA",
        "NUS website",
        "No",
        "False",
        "No",
        "False",
        "Do nothing",
        "nSafe",
        "False",
        "Do nothing",
        "No",
        "No",
        "Reset",
        "5",
        "6 months",
        "%q2Wf2U4*d/NG",
        "itcare@nus.com.sg ",
        "Buy a notebook",
        "ALCA"
        };

        correctList = new List<int>() { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 2, 2, 1, 1 };

        currentZ = 0f;
        currentRow = 0;
        for (int i = 0; i < mapList.Length; i++)
        {
            int blockType;

            if (i <= 10)
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
        // for (int i = 0; i < 20; i++)
        // {
        //     spawnNextBatch();
        // }
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
            if ((currentRow % 30) == 0 || (currentRow % 30) >= 27)
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
        for (float x = -20; x <= 30; x = x + 2)
        {
            //Spawn 20 blocks horizontally
            if (excludeList.Count < questionList.Count)
            {
                if ((currentZ / 2) % 30 == 27)
                {
                    if (x == -20)
                    {
                        currentQuestion = random_except_list(questionList.Count, excludeList);
                        excludeList.Add(currentQuestion);
                    }
                    //Spawn questions every 30 tiles
                    GameObject pavementObj = Instantiate(pavement, new Vector3(x, -0.1f, currentZ), Quaternion.identity);
                    pavementObj.transform.parent = gameObject.transform;
                }
                else if ((currentZ / 2) % 30 == 28)
                {
                    //Spawn questions every 30 tiles
                    GameObject pavementObj = Instantiate(pavement, new Vector3(x, -0.1f, currentZ), Quaternion.identity);
                    pavementObj.transform.parent = gameObject.transform;

                    if (x == -20)
                    {
                        GameObject questionTextObj = Instantiate(questionText, new Vector3(x + 52.4f, 0f, currentZ - 4.6f), Quaternion.identity);
                        questionTextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                        questionTextObj.transform.SetParent(gameObject.transform, false);
                        questionTextObj.GetComponent<TextMeshPro>().SetText(questionList[currentQuestion]);
                    }
                }
                else if ((currentZ / 2) % 30 == 29 || (currentZ / 2) % 30 == 0)
                {
                    //Spawn answers every 30 tiles
                    if (currentZ != 0)
                    {
                        if (x >= -20 + (2 * 5) && x <= -20 + (2 * 10))
                        {
                            //Spawn the bridges for the answers
                            GameObject bridgeObj = Instantiate(bridge, new Vector3(x, -0.5f, currentZ), Quaternion.identity);
                            bridgeObj.transform.SetParent(gameObject.transform);
                            bridgeObj.GetComponent<BridgeController>().currentQuestion = questionList[currentQuestion];
                            if (correctList[currentQuestion] == 1)
                            {
                                bridgeObj.GetComponent<BridgeController>().containsCorrectAnswer = true;
                            }
                            else
                            {
                                bridgeObj.GetComponent<BridgeController>().containsCorrectAnswer = false;
                            }
                        }
                        else if (x >= -20 + (2 * 15) && x <= -20 + (2 * 20))
                        {
                            //Spawn the bridges for the answers
                            GameObject bridgeObj = Instantiate(bridge, new Vector3(x, -0.5f, currentZ), Quaternion.identity);
                            bridgeObj.transform.SetParent(gameObject.transform);
                            bridgeObj.GetComponent<BridgeController>().currentQuestion = questionList[currentQuestion];
                            if (correctList[currentQuestion] == 2)
                            {
                                bridgeObj.GetComponent<BridgeController>().containsCorrectAnswer = true;
                            }
                            else
                            {
                                bridgeObj.GetComponent<BridgeController>().containsCorrectAnswer = false;
                            }
                        }
                        else
                        {
                            //Spawn the rivers for the answers
                            GameObject riverObj = Instantiate(river1, new Vector3(x, 0f, currentZ), Quaternion.identity);
                            riverObj.transform.parent = gameObject.transform;
                            riverObj.GetComponent<SpawnRiver>().canSpawn = false;
                        }

                        if ((currentZ / 2) % 30 == 0 && x == 30 && questionList.Count > 1)
                        {
                            //Move on to next question on the last block
                            // questionList.Remove(questionList[currentQuestion]);
                            // answer1List.Remove(answer1List[currentQuestion]);
                            // answer2List.Remove(answer2List[currentQuestion]);
                            // correctList.Remove(correctList[currentQuestion]);
                        }

                        if ((currentZ / 2) % 30 == 0 && x == -20)
                        {
                            GameObject answer1TextObj = Instantiate(answerText, new Vector3(x + 60.5f, 0f, currentZ - 5f), Quaternion.identity);
                            answer1TextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                            answer1TextObj.transform.SetParent(gameObject.transform, false);
                            answer1TextObj.GetComponent<TextMeshPro>().SetText(answer1List[currentQuestion]);

                            GameObject answer2TextObj = Instantiate(answerText, new Vector3(x + 80.5f, 0f, currentZ - 5f), Quaternion.identity);
                            answer2TextObj.transform.rotation = Quaternion.Euler(90, 0, 0);
                            answer2TextObj.transform.SetParent(gameObject.transform, false);
                            answer2TextObj.GetComponent<TextMeshPro>().SetText(answer2List[currentQuestion]);
                        }
                    }

                }
                else if (x == -20)
                {
                    //First block of row
                    GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                    grassObj.transform.parent = gameObject.transform;
                    grassObj.GetComponent<SpawnFood>().spawnRight = true;
                    if (canSpawn == 0)
                    {
                        grassObj.GetComponent<SpawnFood>().enabled = false;
                    }
                }
                else if (x == 30)
                {
                    //Last block of row
                    GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                    grassObj.transform.parent = gameObject.transform;
                    grassObj.GetComponent<SpawnFood>().spawnRight = false;
                    if (canSpawn == 1)
                    {
                        grassObj.GetComponent<SpawnFood>().enabled = false;
                    }
                }
                else
                {
                    GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                    grassObj.transform.parent = gameObject.transform;
                    grassObj.GetComponent<SpawnFood>().enabled = false;
                }
            }

            else if (x == -20)
            {
                //First block of row
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().spawnRight = true;
                if (canSpawn == 0)
                {
                    grassObj.GetComponent<SpawnFood>().enabled = false;
                }
            }
            else if (x == 30)
            {
                //Last block of row
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().spawnRight = false;
                if (canSpawn == 1)
                {
                    grassObj.GetComponent<SpawnFood>().enabled = false;
                }
            }
            else
            {
                GameObject grassObj = Instantiate(grass, new Vector3(x, -2f, currentZ), Quaternion.identity);
                grassObj.transform.parent = gameObject.transform;
                grassObj.GetComponent<SpawnFood>().enabled = false;
            }

            if ((currentZ / 2) % 30 != 0 && (currentZ / 2) % 30 < 27)
            {
                //Spawn more tree the further you travel
                //var shouldSpawnTree = Mathf.FloorToInt(Random.Range(0, 3 + Mathf.Max(0, (50 - currentZ / 20))));
                var shouldSpawnTree = Random.Range(0, 10);
                if (shouldSpawnTree == 0)
                {
                    GameObject decoration;
                    int currentDecoration = Mathf.FloorToInt(currentZ / 100);
                    //Change decoration style every 50 blocks
                    if (currentDecoration >= decorations.Count)
                    {
                        decoration = decorations[decorations.Count - 1];
                    }
                    else
                    {
                        decoration = decorations[Mathf.FloorToInt(currentZ / 100)];
                    }

                    GameObject decorationObj = Instantiate(decoration, new Vector3(x, 0f, currentZ), Quaternion.identity);
                    decorationObj.transform.parent = gameObject.transform;
                }
                else
                {
                    var shouldSpawnFood = Random.Range(0, 100);
                    if (shouldSpawnFood == 0)
                    {
                        GameObject food;
                        food = foods[Random.Range(0, foods.Count)];

                        GameObject foodObj = Instantiate(food, new Vector3(x, 0f, currentZ), Quaternion.identity);
                        foodObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                        foodObj.transform.parent = gameObject.transform;
                    }
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

        for (float x = -20; x <= 30; x = x + 2)
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
            else if (x == 30)
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
        for (float x = -20; x <= 30; x = x + 2)
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
            else if (x == 30)
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

    public void restartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void goHome()
    {
        Scene scene = SceneManager.GetActiveScene();
        string mainScene = "MainGame";
        if (scene.name == mainScene)
        {
            SceneManager.LoadScene("HomeScreen");
        }

    }

    public void sendFeedback()
    {
        if (feedbackText.text != "")
        {
            GameObject azureControl = GameObject.FindGameObjectWithTag("Persistent");
            string name = PlayerPrefs.GetString("myname");
            string email = PlayerPrefs.GetString("myemail");

            azureControl.GetComponent<AzureControl>().callFeddback(name, email, feedbackText.text);
            feedbackText.text = "";
        }
    }

    public static int random_except_list(int n, List<int> x)
    {
        List<int> includeList = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (!x.Contains(i))
            {
                includeList.Add(i);
            }
        }

        if (includeList.Count == 0)
        {
            return 0;
        }

        int newNumber = Random.Range(0, includeList.Count);
        return includeList[newNumber];
    }
}

