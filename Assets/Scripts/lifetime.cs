using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifetime : MonoBehaviour {


    [SerializeField] float lifeTime = 5.0f;
    float counter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if(counter >= lifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
