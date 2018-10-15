using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
    [SerializeField] GameObject pickupable;
    int supply = 4;

    [SerializeField] Sprite sprite0, sprite1, sprite2, sprite3, sprite4;

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

        supply--;

        setSprite();

        if (supply <= 0)
        {
            Destroy(this, 0.01f);
        }

        return temp;
    }



    void setSprite()
    {
        switch (supply)
        {
            case 4:
                GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 0:
                GetComponent<SpriteRenderer>().sprite = sprite0;
                break;
            default:
                Debug.Log("Truck supply ERROR!");
                break;
        }
    }
}
