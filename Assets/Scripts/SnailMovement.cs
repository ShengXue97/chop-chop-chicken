using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public bool movingRight;
    public bool canMove;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -40 || transform.position.x > 45)
        {
            //Outside of screen, destroy!
            Destroy(gameObject);
        }

        if (!canMove)
        {
            return;
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            Debug.Log("oh no");
            canMove = false;
            anim.SetBool("Idle", true);
        }
    }
}
