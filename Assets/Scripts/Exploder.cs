using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explode()
    {
        Collider2D[] temp = new Collider2D[30];
        ContactFilter2D tempFilter = new ContactFilter2D();
        tempFilter.useTriggers = true;

        Collider2D myCollider = new Collider2D();
        if (GetComponent<BoxCollider2D>())  // Will this work?
        {
            myCollider = GetComponent<BoxCollider2D>();
        }
        else if (GetComponent<CircleCollider2D>())
        {
            myCollider = GetComponent<CircleCollider2D>();
        }

        int numColliders = myCollider.OverlapCollider(tempFilter, temp);

        for (int i = 0; i < numColliders; i++)
        {
            if (temp[i].gameObject.GetComponent<oil>())
            {
                temp[i].gameObject.GetComponent<oil>().burn();
            }
            else if (temp[i].gameObject.GetComponent<destructible>())
            {
                temp[i].gameObject.GetComponent<destructible>().getDestroyed();
            }
        }

    }

}
