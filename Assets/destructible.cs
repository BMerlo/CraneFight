using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour {
    [SerializeField] float minForce2Destroy = 0.5f;
    [SerializeField] GameObject spawnee;
    [SerializeField] bool doesSpawnOnDestroy = false;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        getDestroyed();
    }

   public void getDestroyed()
    {
        if (doesSpawnOnDestroy && spawnee)
        {
            Instantiate(spawnee, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject, 0.01f);
    }
}
