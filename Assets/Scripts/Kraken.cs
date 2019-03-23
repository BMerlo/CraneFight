using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {
    [SerializeField] GameObject tentacle;    
    [SerializeField] float minTime = 1.5f;
    [SerializeField] float maxTime = 10.5f;
    public float randomTime;
    float counter = 0;
    float speed = 20;
    public bool top;
    public bool flag = false;
    private float randomRx;//random X on right axis
    private float randomLx;
    private Transform initialPosition;
    private Rigidbody2D krakenRB;

    float spawnPointX; //value random to spawn on X axis

    // Use this for initialization
    void Start () {
        randomTime = Random.Range(minTime, maxTime);        
        flag = true;

        initialPosition = gameObject.transform;

        gameObject.layer = 19;

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
                spawnPointX = Random.Range(randomLx, randomRx); //range to spawn tentacles.. will it depend on size screen?
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
                    spawnPointX = Random.Range(randomLx, randomRx);
                    Vector3 spawnPosition = new Vector3(spawnPointX, transform.position.y, transform.position.z);
                  //Debug.Log("spawning at " + spawnPosition);
                    Instantiate(tentacle, spawnPosition + (tentacle.transform.up * 1), transform.rotation);                  
                    randomTime = Random.Range(minTime, maxTime);
                }
            }
        }
    }

    public void spawnTentacleInFront() { //so player can spawn a tentacle from ghost
        if (GameObject.Find("Tentacle(Clone)") != null) //check if a tentacle already exists
        {
            counter = 0;
            Debug.Log("tried to spawn but already exists one");
        }
        else {       
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);        
        Instantiate(tentacle, spawnPosition + (tentacle.transform.up * 1), transform.rotation);
        }

        randomTime = Random.Range(minTime, maxTime);
    }

    public void setRandomRx(float positionRx) {
        randomRx = positionRx;
        //Debug.Log("Position on Right X axis: " + randomRx);
    }

    public void setRandomLx(float positionLx)
    {
        randomLx = positionLx;
       // Debug.Log("Position on Right X axis: " + randomLx);
    }

    public float getRandomRx()
    {
        return randomRx; 
    }

    public float getRandomLx()
    {
        return randomLx;
    }

    public void setInitialPosition() {
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, initialPosition.position.y, gameObject.transform.position.z);
    }
}

