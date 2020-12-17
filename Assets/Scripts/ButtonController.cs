using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject controller;
    public bool containsCorrectAnswer;
    public string currentQuestion;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnMap spawnMap = controller.GetComponent<SpawnMap>();
            if (!spawnMap.answeredQuestions.Contains(currentQuestion))
            {
                playerObj = other.gameObject;
                if (containsCorrectAnswer)
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(50);
                }
                else
                {
                    playerObj.GetComponent<CharacterMovement>().changeScore(-30);
                }
                spawnMap.answeredQuestions.Add(currentQuestion);
            }

        }
    }
}
