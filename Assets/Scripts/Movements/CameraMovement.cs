using System.Collections;
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
            cameraMovementSpeed = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.GetComponent<CharacterMovement>().ended)
        {
            return;
        }

        if (playerObj.transform.position.z - 30.5 >= gameObject.transform.position.z)
        {
            //Only follow player if the player is not moving backwards
            Vector3 newPos = gameObject.transform.position;
            //Camera should be 30f z behind player.
            newPos.z = playerObj.transform.position.z - 30f;
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
