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

    int spawnPointX; //value random to spawn on X axis

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

        if (flag)
        {
            counter += Time.deltaTime;

            if (counter >= randomTime && !top)
            {
                counter = 0;
                spawnPointX = Random.Range(-2, 6); //range to spawn tentacles.. will it depend on size screen?
                Vector3 spawnPosition = new Vector3(spawnPointX, transform.position.y, transform.position.z);
                Instantiate(tentacle, spawnPosition, transform.rotation);
                randomTime = Random.Range(minTime, maxTime);
                //flag = false;
            }
            else if (counter >= randomTime && top)
            {
                if (GameObject.Find("Tentacle(Clone)") != null) //check if a tentacle already exists
                {
                    counter = 0;
                    Debug.Log("tried to spawn but already exists one");
                }
                else
                {
                    counter = 0;
                    spawnPointX = Random.Range(0, 8);
                    Vector3 spawnPosition = new Vector3(spawnPointX, transform.position.y, transform.position.z);
                    Debug.Log("spawning at " + spawnPosition);
                    Instantiate(tentacle, spawnPosition + (tentacle.transform.up * 1), transform.rotation);
                    //Instantiate(tentacle, transform.position + (tentacle.transform.up * 1), transform.rotation);
                    randomTime = Random.Range(minTime, maxTime);
                }
            }
        }

    }
}
