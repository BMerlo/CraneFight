using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {
    [SerializeField] GameObject tentacle;    
    [SerializeField] float minTime = 1.5f;
    [SerializeField] float maxTime = 10.5f;
    public float randomTime;
    float counter = 0;
    public bool top;

    // Use this for initialization
    void Start () {
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log(transform.position);

        if (transform.position.y < 0)
        {
            top = false;
        }
        else
        {
            top = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter >= randomTime && !top)
        {
            counter = 0;
            
            Instantiate(tentacle, transform.position + (tentacle.transform.up * -0.5f), transform.rotation);
            randomTime = Random.Range(minTime, maxTime);
        }
        else if (counter >= randomTime && top)
        {            
            counter = 0;            
            Instantiate(tentacle, transform.position + (tentacle.transform.up * 1), transform.rotation);
            randomTime = Random.Range(minTime, maxTime);
        }

    }
}
