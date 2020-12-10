using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    public Animator anim;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public float moveSpeed = 5f;
    [SerializeField]
    public TextMeshProUGUI scoreText;
    [SerializeField]
    public TextMeshProUGUI maxScoreText;
    private MeshRenderer meshRenderer;


    private int zPos = 0;
    private int zMax = 0;
    private int highscore = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        movePoint.parent = null;
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            maxScoreText.text = "Top: " + highscore;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!meshRenderer.isVisible && Time.timeSinceLevelLoad > 0.1)
        {
            //Dead if player is not visible by any cameras
            // EditorUtility.DisplayDialog("Info", "You died", "Ok");
            // Scene scene = SceneManager.GetActiveScene();
            // SceneManager.LoadScene(scene.name);
        }

        if (Physics.OverlapSphere(movePoint.position, 0.2f, whatStopsMovement).Length != 0)
        {
            EditorUtility.DisplayDialog("Info", "You died", "Ok");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05F)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (Physics.OverlapSphere(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f), 0.2f, whatStopsMovement).Length == 0)
                {
                    if (Input.GetAxisRaw("Horizontal") == -1f)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                    else
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    anim.SetBool("Hop", true);
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f);
                }
                else
                {
                    EditorUtility.DisplayDialog("Info", "You died", "Ok");
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Physics.OverlapSphere(movePoint.position + new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 2), 0.2f, whatStopsMovement).Length == 0)
                {
                    if (Input.GetAxisRaw("Vertical") == -1f)
                    {
                        zPos -= 1;
                        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        zPos += 1;
                        if (zPos > zMax)
                        {
                            zMax = zPos;
                            scoreText.text = "Score: " + zMax;
                            if (zMax > highscore)
                            {
                                PlayerPrefs.SetInt("highscore", zMax);
                                highscore = zMax;
                                maxScoreText.text = "Top: " + zMax;
                            }
                        }
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    anim.SetBool("Hop", true);
                    movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 2);
                }
                else
                {
                    EditorUtility.DisplayDialog("Info", "You died", "Ok");
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
            else
            {
                anim.SetBool("Hop", false);
            }
        }
    }
}

