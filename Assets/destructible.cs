﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour {
    [SerializeField] float minForce2Destroy = 0.5f;
    [SerializeField] GameObject spawnee;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject, 0.1f);
    }


   
}
