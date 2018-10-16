using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallOff : MonoBehaviour {
    [SerializeField] float maxY = 3.51f;
    [SerializeField] float minY = -3.91f;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(this.transform.position.x);
        if (this.transform.position.y > maxY)
        {
            fallFromTop();
        }
        else if (this.transform.position.y < minY)
        {
            fall();
        }
	}

    void fallFromTop()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -3;
        GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        fall();
    }

    void fall()
    {
        Debug.Log("Falling!");
        if (GetComponent<playerController>())
        {
            GetComponent<playerController>().enabled = false;
        }
        if (GetComponent<BoxCollider2D>())
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (GetComponent<CircleCollider2D>())
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
        }


        Destroy(this.gameObject, 1.5f);
    }
}
