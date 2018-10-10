using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player is here!");
            collision.GetComponent<playerController>().getOiled();
        }
    }

}
