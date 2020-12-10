using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRiver : MonoBehaviour
{
    [SerializeField]
    public bool canSpawn = false;
    public bool spawnRight;
    public GameObject log1;
    private float currentTime = -999999f;
    private MeshRenderer meshRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (meshRenderer.isVisible)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }

        if (!canSpawn)
        {
            return;
        }

        var shouldSpawn = Random.Range(0, 50);
        var logType = Random.Range(0, 2);
        //1/3 probability every frame of spawning a log
        if (shouldSpawn == 0 && Time.timeSinceLevelLoad >= currentTime + 10.2f)
        {
            //Only allow spawning once in a while to prevent log collision
            currentTime = Time.timeSinceLevelLoad;
            Vector3 newPos = transform.position;

            if (logType == 0 && spawnRight)
            {
                newPos.z -= 2f;
                GameObject log = Instantiate(log1, newPos, Quaternion.identity);
                log.transform.rotation = Quaternion.Euler(0, 90, 0);
                log.transform.parent = gameObject.transform;
                log.GetComponent<LogMovement>().movingRight = true;
            }
            else if (logType == 0 && !spawnRight)
            {
                newPos.z += 2f;
                GameObject log = Instantiate(log1, newPos, Quaternion.identity);
                log.transform.rotation = Quaternion.Euler(0, -90, 0);
                log.transform.parent = gameObject.transform;
                log.GetComponent<LogMovement>().movingRight = false;
            }
        }
    }
}
