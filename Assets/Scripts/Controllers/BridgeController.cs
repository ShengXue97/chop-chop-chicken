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

    public SpawnMap spawnMap;
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
            spawnMap = controller.GetComponent<SpawnMap>();
            if (spawnMap.userAnswers.ContainsKey(currentQuestion))
            {
                changedColor = true;
                //spawnMap.userAnswers[currentQuestion] contains whether user answer correctly
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
            spawnMap = controller.GetComponent<SpawnMap>();
            if (!spawnMap.userAnswers.ContainsKey(currentQuestion))
            {
                playerObj = other.gameObject;
                if (containsCorrectAnswer)
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(50);
                    spawnMap.userAnswers.Add(currentQuestion, true);
                }
                else
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(-30);
                    spawnMap.userAnswers.Add(currentQuestion, false);
                }

            }

        }
    }
}

