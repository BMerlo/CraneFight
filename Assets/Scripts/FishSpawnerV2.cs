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

    int spawnPointX; //value random to spawn on X axis


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
            counter = 0;
            spawnPointX = Random.Range(-2, 6); //range to spawn fishes.. will it depend on size screen?
            Vector3 spawnPosition = new Vector3(spawnPointX, transform.position.y, transform.position.z);
            Instantiate(m_Fish, spawnPosition, this.transform.rotation);            
            timeActual = Random.Range(minTime, maxTime);
        }
    }

    //fish jump on to the road

    public void ChangeMinimum(float ammount)
    {
        Debug.Log("min time of fish spawner changed by minus" + ammount);
        minTime -= ammount;
        if (minTime < 0)
        {
            minTime = 0;
        }

    }

    public void ChangeMaximum(float ammount)
    {
        Debug.Log("max time of fish spawner changed by minus" + ammount);
        maxTime -= ammount;

        if (maxTime < 2) //just in case cap gives issues
        {
            maxTime = 2;
        }
    }

}
