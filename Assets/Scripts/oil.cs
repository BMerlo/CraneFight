using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil : MonoBehaviour {
    [SerializeField] GameObject burningOil;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something is on oil");
        if (collision.tag == "Player")
        {
            Debug.Log("Player is here!");
            collision.GetComponent<playerController>().getOiled();
        }
    }

    public void burn()
    {
        Instantiate(burningOil, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

}
