using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {
    [SerializeField] GameObject spawnee;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explode()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
