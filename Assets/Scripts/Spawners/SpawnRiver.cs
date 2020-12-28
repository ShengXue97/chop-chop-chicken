using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRiver : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    public bool canSpawn = false;
    public bool spawnRight;
    public GameObject log1;
    private float currentTime = -9999999f;
    private MeshRenderer meshRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
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

        int zPos = player.GetComponent<CharacterMovement>().zPos;

        if (Mathf.Abs(player.transform.position.z - transform.position.z) > 30f)
        {
            return;
        }

        if (!canSpawn)
        {
            return;
        }
        Debug.Log(spawnRight);

        var shouldSpawn = Random.Range(0, 4);
        var logType = Random.Range(0, 2);

        //Always have at least one log on screen
        //1/3 probability every frame of spawning a log
        if ((shouldSpawn == 0 && Time.timeSinceLevelLoad > currentTime + 1.2f) || transform.childCount == 0)
        {
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
