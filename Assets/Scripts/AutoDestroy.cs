﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int zPos = player.GetComponent<CharacterMovement>().zPos;
        if (zPos > transform.position.z + 10f)
        {
            //Destroy tile if player passed it by 20f z 
            Destroy(gameObject);
        }
    }
}