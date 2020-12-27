using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChicken : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 1f);
    }
}
