using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenFar : MonoBehaviour {
    float maxX = 35;
    float minX = -20;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x < minX)
        {
            Destroy(this.gameObject);
        }
	}
}
