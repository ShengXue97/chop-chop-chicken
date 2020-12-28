using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    public GameObject GameTip;
    public GameObject uploadingPanel;
    public GameObject FinishPanel;
    public UnityEngine.UI.Text FinishText;
    public UnityEngine.UI.Text ScoreDetails;
    public GameObject ScorePopup;
    public Animator anim;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public LayerMask vehicleLayer;
    public LayerMask waterLayer;
    public LayerMask logLayer;
    public float moveSpeed = 20f;
    [SerializeField]
    public UnityEngine.UI.Text scoreText;
    [SerializeField]
    public UnityEngine.UI.Text maxScoreText;
    private MeshRenderer meshRenderer;

    private Hashtable foods;

    public int zPos = 0;
    private int zMax = 0;
    private int highscore = 0;
    private int score = 0;
    private bool canMoveHorizontal = true;
    private bool canMoveVertical = true;
    public bool ended;
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
        ended = false;
        anim = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        movePoint.parent = null;
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            maxScoreText.text = "Your top: " + highscore;
        }

        foods = new Hashtable();

        foods.Add("apple(Clone)", 5);
        foods.Add("appleHalf(Clone)", 2);
        foods.Add("banana(Clone)", 10);
        foods.Add("beet(Clone)", 5);
        foods.Add("carrot(Clone)", 10);
        foods.Add("cauliflower(Clone)", 20);
        foods.Add("cherries(Clone)", 5);
        foods.Add("coconutHalf(Clone)", 10);
        foods.Add("corn(Clone)", 10);
        foods.Add("egg(Clone)", 5);
        foods.Add("eggHalf(Clone)", 2);
        foods.Add("grapes(Clone)", 10);
        foods.Add("pepper(Clone)", 5);
        foods.Add("pineapple(Clone)", 20);
        foods.Add("pumpkin(Clone)", 30);
        foods.Add("strawberry(Clone)", 5);
        foods.Add("watermelon(Clone)", 30);


    }

    // Update is called once per frame
    void Update()
    {
        if (ended)
        {
            return;
        }
        float pointer_x = Input.GetAxisRaw("Horizontal");
        float pointer_y = Input.GetAxisRaw("Vertical");
        // if (Input.touchCount > 0)
        // {
        //     pointer_x = Input.touches[0].deltaPosition.x;
        //     pointer_y = Input.touches[0].deltaPosition.y;
        // }

        if (!meshRenderer.isVisible && Time.timeSinceLevelLoad > 0.1)
        {
            //Dead if player is not visible by any cameras
            // EditorUtility.DisplayDialog("Info", "You died", "Ok");
            restartGame();

        }

        Tutorial_SpawnMap Tutorial_SpawnMap = controller.GetComponent<Tutorial_SpawnMap>();
        if (Tutorial_SpawnMap != null)
        {
            if (transform.position.z > 40)
            {
                if (Tutorial_SpawnMap.currentTutorialStage == 1)
                {
                    Tutorial_SpawnMap.forceFood();
                    if (pointer_y > 0f)
                    {
                        pointer_y = 0f;
                    }
                }
                else
                {
                    Tutorial_SpawnMap.answerStage();
                }
            }
            if (transform.position.z > 82)
            {
                Tutorial_SpawnMap.exitRoad();
            }
            if (transform.position.z > 122)
            {
                Tutorial_SpawnMap.endMessage();
            }
            if (transform.position.z > 124)
            {
                Tutorial_SpawnMap.completeTutorial();
            }
        }

        if (Mathf.Abs(pointer_x) == 0f)
        {
            //Prevents holding down arrow keys to move, must release
            canMoveHorizontal = true;
        }

        if (Mathf.Abs(pointer_y) == 0f)
        {
            //Prevents holding down arrow keys to move, must release
            canMoveVertical = true;
        }

        if (Physics.OverlapSphere(gameObject.transform.position, 0.2f, waterLayer).Length != 0)
        {
            if (Physics.OverlapSphere(gameObject.transform.position, 0.5f, logLayer).Length == 0)
            {
                //If on water but not on logs, die!
                //EditorUtility.DisplayDialog("Info", "You died", "Ok");
                controller.GetComponent<SoundController>().playSound("splashSound");
                restartGame();
            }
        }


        if (Physics.OverlapSphere(movePoint.position, 0.19f, vehicleLayer).Length != 0)
        {
            //EditorUtility.DisplayDialog("Info", "You died", "Ok");
            controller.GetComponent<SoundController>().playSound("carSound");
            restartGame();
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, 200f * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05F)
        {
            if (Mathf.Abs(pointer_x) == 1f && canMoveHorizontal)
            {
                canMoveHorizontal = false;
                if (Physics.OverlapSphere(movePoint.position + new Vector3(pointer_x * 1, 0f, 0f), 0.6f, whatStopsMovement).Length == 0)
                {
                    if (pointer_x == -1f)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                    else
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    anim.SetBool("Hop", true);
                    movePoint.position += new Vector3(pointer_x * 2, 0f, 0f);
                }
                if (Physics.OverlapSphere(movePoint.position + new Vector3(pointer_x * 1, 0f, 0f), 0.2f, vehicleLayer).Length != 0)
                {
                    //EditorUtility.DisplayDialog("Info", "You died", "Ok");
                    // Debug.Log("3");
                    // restartGame();
                }
            }
            else if (Mathf.Abs(pointer_y) == 1f && canMoveVertical)
            {
                canMoveVertical = false;
                if (Physics.OverlapSphere(movePoint.position + new Vector3(0f, 0f, pointer_y * 1), 0.3f, whatStopsMovement).Length == 0)
                {
                    if (pointer_y == -1f)
                    {
                        zPos -= 1;
                        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        zPos += 1;
                        if (zPos > zMax)
                        {
                            score += 1;
                            zMax = zPos;
                            scoreText.text = "Score: " + score;
                            if (score > highscore)
                            {
                                PlayerPrefs.SetInt("highscore", score);
                                highscore = score;
                                maxScoreText.text = "Your top: " + score;
                            }
                        }
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    anim.SetBool("Hop", true);
                    movePoint.position += new Vector3(0f, 0f, pointer_y * 2);
                }
                if (Physics.OverlapSphere(movePoint.position + new Vector3(0f, 0f, pointer_y * 1.4f), 0.2f, vehicleLayer).Length != 0)
                {
                    //EditorUtility.DisplayDialog("Info", "You died", "Ok");
                    // Debug.Log("5");
                    // restartGame();
                }
            }
            else
            {
                anim.SetBool("Hop", false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (foods.ContainsKey(other.name))
        {
            GameObject scorePopupObj = Instantiate(ScorePopup, new Vector3(transform.position.x + 37f, transform.position.y + 3f, transform.position.z), Quaternion.identity);
            scorePopupObj.GetComponent<TextMeshPro>().text = ((int)foods[other.name]).ToString();
            score += (int)foods[other.name];
            Destroy(other.gameObject);
            controller.GetComponent<SoundController>().playSound("itemSound");

            if (controller.GetComponent<Tutorial_SpawnMap>() != null)
            {
                controller.GetComponent<Tutorial_SpawnMap>().collectFood();
            }
        }

        else if (other.tag == "Snail")
        {
            GameObject scorePopupObj = Instantiate(ScorePopup, new Vector3(transform.position.x + 37f, transform.position.y + 3f, transform.position.z), Quaternion.identity);
            scorePopupObj.GetComponent<TextMeshPro>().text = "15";
            score += 15;
            Destroy(other.gameObject);
        }

        scoreText.text = "Score: " + score;
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore = score;
            maxScoreText.text = "Your top: " + score;
        }

    }

    public void changeScore(int scoreDiff)
    {
        if (scoreDiff > 0)
        {
            controller.GetComponent<SoundController>().playSound("answerCorrect");
        }
        else
        {
            controller.GetComponent<SoundController>().playSound("answerWrong");
        }
        GameObject scorePopupObj = Instantiate(ScorePopup, new Vector3(transform.position.x + 37f, transform.position.y + 3f, transform.position.z), Quaternion.identity);
        scorePopupObj.GetComponent<TextMeshPro>().text = scoreDiff.ToString();
        score += scoreDiff;
        scoreText.text = "Score: " + score;
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore = score;
            maxScoreText.text = "Your top: " + score;
        }
    }

    public void restartGame()
    {
        if (ended)
        {
            return;
        }

        ended = true;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "TutorialGame")
        {
            randomTip();
            GameObject azureControl = GameObject.FindGameObjectWithTag("Persistent");
            string name = PlayerPrefs.GetString("myname");
            string email = PlayerPrefs.GetString("myemail");

            FinishText.text = "Uploaded Score! \nName: " + name + "\nEmail: " + email;
            ScoreDetails.text = score.ToString();

            if (azureControl != null)
            {
                azureControl.GetComponent<AzureControl>().callUpdate(name, email, score);
            }
        }


        FinishPanel.SetActive(true);
    }

    public void setEnded()
    {
        ended = false;
    }

    public void randomTip()
    {
        List<string> tips = new List<string>() {
        "Did you know? Larger food give you more points!",
        "Did you know? The tree types will change every 100 blocks!",
        "Did you know? The cars and logs move faster every 100 blocks!",
        "Did you know? The rain makes the cars slower and logs faster!",
        "You were not chop-chop so you became chicken chop!",
        };

        int num = Random.Range(0, tips.Count);

        if (GameTip != null)
        {
            GameTip.GetComponent<UnityEngine.UI.Text>().text = tips[num];
        }
    }
}

