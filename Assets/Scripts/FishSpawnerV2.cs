using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerV2 : MonoBehaviour
{
    [SerializeField] GameObject m_Fish;
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


        SpawnChecked();


    }


    void SpawnChecked()
    {
        if (counter >= timeActual)
        {
            Instantiate(m_Fish, this.transform.position, this.transform.rotation);

            counter = 0;
            timeActual = Random.Range(minTime, maxTime);
        }
    }

    //fish jump on to the road

}
