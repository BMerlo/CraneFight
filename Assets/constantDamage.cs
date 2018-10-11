using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class constantDamage : MonoBehaviour {
    [SerializeField] float damage = 1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<destructible>())
        {
            collision.GetComponent<destructible>().getDestroyed();
        }
        else if (collision.GetComponent<oil>())
        {
            collision.GetComponent<oil>().burn();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<playerController>())
        {
            collision.GetComponent<playerController>().takeDamage(Time.deltaTime * damage);
        }
    }

    
}
