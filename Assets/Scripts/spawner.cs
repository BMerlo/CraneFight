using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    [SerializeField] GameObject[] spawnees;
   // [SerializeField] GameObject spawnee;
    [SerializeField] float minTime = 2;
    [SerializeField] float maxTime = 6;
    float counter = 0;
    float timeActual;

    int MAX_CARS = 8;

	// Use this for initialization
	void Start () {
        timeActual = Random.Range(minTime, maxTime);
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;


        SpawnChecked();


    }
        

    void SpawnChecked()
    {
            if (counter >= timeActual)
            {
                if (countAIvehicles() < MAX_CARS)
                {
                int i = Random.Range(0, spawnees.Length);

                Instantiate(spawnees[i], this.transform.position, this.transform.rotation);
                }


            counter = 0;
            timeActual = Random.Range(minTime, maxTime);
            }
    }

    public void ChangeMaxCars(int ammount) {
        Debug.Log("Max ammount of cars changed by +" + ammount);
        MAX_CARS += ammount;
    }

    public void ChangeMinimum(float ammount) {
        Debug.Log("min time of cars changed by" + ammount);
        minTime -= ammount;
        if (minTime < 0) {
            minTime = 0;
        }
    }

    public void ChangeMaximum(float ammount)
    {
        Debug.Log("max time of cars changed by" + ammount);
        maxTime -= ammount;

        if (maxTime < 2) //just in case cap gives issues
        {
            maxTime = 2;
        }
    }

    int countAIvehicles()
    {
        carAI[] cars = FindObjectsOfType<carAI>();
        //Debug.Log("There are " + cars.Length + " in the scene.----------------");
        return cars.Length;
    }
}
