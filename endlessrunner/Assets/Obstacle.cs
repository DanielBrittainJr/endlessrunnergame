using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
       //kill player 
       if (collision.gameObject.name == "Player")
       {
           playerController.Die();

       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
