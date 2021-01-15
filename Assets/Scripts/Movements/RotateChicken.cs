using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChicken : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed;

    void Start()
    {
        RotationSpeed = 20f;
    }
    // Update is called once per frame
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * RotationSpeed * Mathf.Deg2Rad;

        // transform.RotateAround(Vector3.up, -rotX);
        // transform.RotateAround(Vector3.right, -rotY);
    }
}
