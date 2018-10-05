﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneZone : MonoBehaviour {
    Collider2D myCollider;
    [SerializeField] GameObject obj2pick;
    GameObject carrier2pickUpFrom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pickable")
        {
            obj2pick = collision.gameObject;
        }
        else if (collision.transform.tag == "Carrier")
        {
            carrier2pickUpFrom = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (obj2pick == collision.gameObject)
        {
            obj2pick = null;
        }
        else if (obj2pick == collision.gameObject)
        {
            carrier2pickUpFrom = null;
        }
    }

    public bool isTherePickable()
    {
        if (obj2pick)
            return true;
        if (carrier2pickUpFrom)
            return true;

        return false;
    }

    public GameObject getObj2PickUp()
    {
        Debug.Log("getObj2PickUp()");
        if (obj2pick)
        {
            Debug.Log("if (obj2pick)()");
            return obj2pick;
        }
        else if (carrier2pickUpFrom)
        {   // Instantiate obj here
            Debug.Log("else if (carrier2pickUpFrom)");
            return carrier2pickUpFrom.GetComponent<Carrier>().getPickUpable();
        }
        return null;
    }
}
