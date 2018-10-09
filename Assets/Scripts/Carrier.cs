using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
    [SerializeField] GameObject pickupable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject getPickUpable()
    {
        Debug.Log("getPickUpable()");
        GameObject temp = Instantiate(pickupable, this.transform.position, this.transform.rotation);
        return temp;
    }
}
