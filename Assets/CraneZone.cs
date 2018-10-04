using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneZone : MonoBehaviour {
    Collider2D myCollider;
    [SerializeField] GameObject obj2pick;

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (obj2pick == collision.gameObject)
        {
            obj2pick = null;
        }
    }


    public GameObject getObj2PickUp()
    {
        return obj2pick;
    }
}
