﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoController : MonoBehaviour
{
    public float Speed = 1f;
    public bool IsMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
//        if (this.gameObject.activeSelf)
        if (IsMoving)
        {
            this.gameObject.transform.position += this.gameObject.transform.right * Speed;
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Torpedo collided - BOOM");
        this.gameObject.SetActive(false);
    }
}
