using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {
    [SerializeField] GameObject dropee;
    [SerializeField] Transform dropPos;
    [SerializeField] float minTime = 1.5f;
    [SerializeField] float maxTime = 2.5f;
    float timeActual;
    float counter = 0;

	// Use this for initialization
	void Start () {
        timeActual = Random.Range(minTime, maxTime);
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter >= timeActual)
        {
            counter = 0;
            timeActual = Random.Range(minTime, maxTime);
            Instantiate(dropee, dropPos.position, dropPos.rotation);
        }
		
	}
}
