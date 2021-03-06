﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerObj;
    public float cameraMovementSpeed;

    [SerializeField]
    public bool shouldMove;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (shouldMove)
        {
            if (Application.isMobilePlatform || SystemInfo.deviceModel.Contains("iPad"))
            {
                cameraMovementSpeed = 3.0f;
            }
            else
            {
                cameraMovementSpeed = 3.5f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.GetComponent<CharacterMovement>().ended)
        {
            return;
        }

        if (playerObj.transform.position.z - 50.5 >= gameObject.transform.position.z)
        {
            //Only follow player if the player is not moving backwards
            Vector3 newPos = gameObject.transform.position;
            //Camera should be 30f z behind player.
            newPos.z = playerObj.transform.position.z - 50f;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime);
        }
        else
        {
            Vector3 newPos = gameObject.transform.position;
            newPos.z += 1f;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPos, Time.deltaTime * cameraMovementSpeed);
        }

    }
}
