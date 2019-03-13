using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    [SerializeField] private GameObject     m_Fish;
    [SerializeField] private float          m_minSpawnTime = 3.0f;
    [SerializeField] private float          m_maxSpawnTime = 5.0f;
                     private float          m_currentTime = 0.0f;
                     private float          m_spawnTime;



    // Use this for initialization
    void Start () {
        m_spawnTime = Random.Range(m_minSpawnTime, m_maxSpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
        m_currentTime += Time.deltaTime;

        if (m_currentTime >= m_spawnTime)
        {
            m_currentTime = 0;
            m_spawnTime = Random.Range(m_minSpawnTime, m_maxSpawnTime);

            Instantiate(m_Fish, this.transform.position, this.transform.rotation);
        }
		
	}

    public void ChangeMinimum(float ammount)
    {
        Debug.Log("min time of fish spawner changed by minus" + ammount);
        m_minSpawnTime -= ammount;
        if (m_minSpawnTime < 0)
        {
            m_minSpawnTime = 0;
        }
        
    }

    public void ChangeMaximum(float ammount)
    {
        Debug.Log("max time of fish spawner changed by minus" + ammount);
        m_maxSpawnTime -= ammount;

        if (m_maxSpawnTime < 2) //just in case cap gives issues
        {
            m_maxSpawnTime = 2;
        }
    }
}
