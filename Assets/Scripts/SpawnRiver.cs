using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRiver : MonoBehaviour
{
    [SerializeField]
    public bool canSpawn = false;
    public bool spawnRight;
    public GameObject log1;
    private float currentTime = -9999999f;
    private MeshRenderer meshRenderer;
    private int maxProbability;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        maxProbability = 3;
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

        var shouldSpawn = Random.Range(0, maxProbability);
        var logType = Random.Range(0, 2);

        if (maxProbability > 1)
        {
            //Become more likely to spawn logs every frame
            maxProbability--;
        }

        //1/3 probability every frame of spawning a log
        if (shouldSpawn == 0 && Time.timeSinceLevelLoad > currentTime + 3.2f)
        {
            maxProbability = 3;
            //Only allow spawning once in a while to prevent log collision
            currentTime = Time.timeSinceLevelLoad;
            Vector3 newPos = transform.position;
            newPos.y -= 0.5f;

            if (logType == 0 && spawnRight)
            {
                newPos.z += 1.4f;
                GameObject log = Instantiate(log1, newPos, Quaternion.identity);
                log.transform.rotation = Quaternion.Euler(0, 90, -90);
                log.transform.parent = gameObject.transform;
                log.GetComponent<LogMovement>().movingRight = true;
            }
            else if (logType == 0 && !spawnRight)
            {
                newPos.z -= 0.5f;
                GameObject log = Instantiate(log1, newPos, Quaternion.identity);
                log.transform.rotation = Quaternion.Euler(0, 90, 90);
                log.transform.parent = gameObject.transform;
                log.GetComponent<LogMovement>().movingRight = false;
            }
        }
    }
}
