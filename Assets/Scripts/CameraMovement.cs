using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerObj;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.transform.position.z - 0.5 >= gameObject.transform.position.z)
        {
            //Only follow player if the player is not moving backwards
            Vector3 newPos = Vector3.Lerp(gameObject.transform.position, playerObj.transform.position, Time.deltaTime);
            gameObject.transform.position = new Vector3(newPos.x, 1, newPos.z);
        }
        else
        {
            Vector3 newPos = gameObject.transform.position;
            newPos.z += 1f;
            Vector3 newDir = Vector3.MoveTowards(gameObject.transform.position, newPos, Time.deltaTime);
            gameObject.transform.position = new Vector3(newDir.x, 1, newDir.z);
        }

    }
}
