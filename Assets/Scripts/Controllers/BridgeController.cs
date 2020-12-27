using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject controller;
    public bool containsCorrectAnswer;
    public string currentQuestion;
    public bool changedColor;

    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
        changedColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changedColor)
        {
            gameController = controller.GetComponent<GameController>();
            if (gameController.userAnswers.ContainsKey(currentQuestion))
            {
                changedColor = true;
                if (containsCorrectAnswer)
                {
                    Color correctColor = new Color(1f, 0.604f, 0.157f, 1f);
                    GetComponent<MeshRenderer>().material.color = correctColor;
                }
                else
                {
                    Color correctColor = new Color(1f, 0.424f, 0.157f, 1f);
                    GetComponent<MeshRenderer>().material.color = correctColor;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameController = controller.GetComponent<GameController>();
            if (!gameController.userAnswers.ContainsKey(currentQuestion))
            {
                playerObj = other.gameObject;
                if (containsCorrectAnswer)
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(50);
                    gameController.userAnswers.Add(currentQuestion, true);


                }
                else
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(-30);
                    gameController.userAnswers.Add(currentQuestion, false);
                }

                if (controller.GetComponent<Tutorial_SpawnMap>() != null)
                {
                    controller.GetComponent<Tutorial_SpawnMap>().answerQuestion(containsCorrectAnswer);
                }

            }


        }
    }
}

