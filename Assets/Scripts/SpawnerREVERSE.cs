using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerREVERSE : MonoBehaviour
{

    [SerializeField] GameObject[] spawnees;
    // [SerializeField] GameObject spawnee;
    [SerializeField] float minTime = 2;
    [SerializeField] float maxTime = 6;
    float counter = 0;
    float timeActual;

    // Use this for initialization
    void Start()
    {
        timeActual = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timeActual)
        {
            counter = 0;
            timeActual = Random.Range(minTime, maxTime);

            int i = Random.Range(0, spawnees.Length);

            Instantiate(spawnees[i], this.transform.position, this.transform.rotation);

        }



    }
}
