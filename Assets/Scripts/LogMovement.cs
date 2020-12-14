using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour
{
    public bool movingRight;
    public LayerMask playerLayer;
    public GameObject playerObj;
    public bool playerOnLog;
    // Start is called before the first frame update
    void Start()
    {
        playerOnLog = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " enter");
            playerOnLog = true;
            playerObj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " exit");
            playerOnLog = false;
            playerObj = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -40 || transform.position.x > 45)
        {
            //Outside of screen, destroy!
            Destroy(gameObject);
        }

        float yValue = 1f;
        if (movingRight)
        {
            yValue = 1f;
        }
        else
        {
            yValue = -1f;
        }

        Vector3 newPos = gameObject.transform.position;
        newPos.x += yValue;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPos, Time.deltaTime * 3f);

        // if (playerOnLog)
        // {
        //     Vector3 playerPos = playerObj.transform.position;
        //     playerPos.x += 2f;
        //     playerObj.transform.position = playerPos;
        // }
    }
}
