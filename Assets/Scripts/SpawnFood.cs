using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField]
    public bool spawnRight;
    public GameObject snail;
    private float currentTime = -999999f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var shouldSpawn = Random.Range(0, 999);
        var foodType = Random.Range(0, 2);
        //1/3 probability every frame of spawning a food
        if (shouldSpawn == 0 && Time.timeSinceLevelLoad >= currentTime + 10.2f)
        {
            //Only allow spawning once in a while to prevent car collision
            currentTime = Time.timeSinceLevelLoad;
            Vector3 newPos = transform.position;
            newPos.y += 2.73f;
            if (foodType == 0 && spawnRight)
            {
                GameObject snailObj = Instantiate(snail, newPos, Quaternion.identity);
                snailObj.transform.rotation = Quaternion.Euler(0, -90, 0);
                snailObj.transform.parent = gameObject.transform;
                snailObj.GetComponent<SnailMovement>().movingRight = true;
            }
            else if (foodType == 0 && !spawnRight)
            {
                GameObject snailObj = Instantiate(snail, newPos, Quaternion.identity);
                snailObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                snailObj.transform.parent = gameObject.transform;
                snailObj.GetComponent<SnailMovement>().movingRight = false;
            }
        }
    }
}
