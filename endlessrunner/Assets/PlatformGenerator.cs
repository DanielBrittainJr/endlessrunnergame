using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    private const float SPAWN_DISTANCE = 50f;
    
    public Transform platform1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        SpawnPlatform(new Vector3(3,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlatform()
    {
        
    }
    private void SpawnPlatform(Vector3 spawnPosition)
    {
        Instantiate(platform1, spawnPosition, Quaternion.identity);
    }
}
