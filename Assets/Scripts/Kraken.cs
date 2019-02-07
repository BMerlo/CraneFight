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
    public bool flag = false;

    // Use this for initialization
    void Start () {
        randomTime = Random.Range(minTime, maxTime);        
        flag = true;

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
        

        if (flag) {

        counter += Time.deltaTime;


            if (counter >= randomTime && !top)
        {
            counter = 0;
            
            Instantiate(tentacle, transform.position + (tentacle.transform.up * -0.5f), transform.rotation);
            randomTime = Random.Range(minTime, maxTime);
                //flag = false;
        }
        else if (counter >= randomTime && top)
        {            
            counter = 00;            
            Instantiate(tentacle, transform.position + (tentacle.transform.up * 1), transform.rotation);
            randomTime = Random.Range(minTime, maxTime);
                //flag = false;
            }
        }

    }
}
