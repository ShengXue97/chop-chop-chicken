using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRiver : MonoBehaviour
{
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
    }
}
