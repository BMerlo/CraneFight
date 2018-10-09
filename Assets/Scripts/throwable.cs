using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwable : MonoBehaviour {
    //[SerializeField] Vector3 pos;
    [SerializeField] float stopDistance = 0.5f;
    bool isStopped = false;
    bool isThrown = false;
    Vector3 lastFramePos;
    float distanceTravelled = 0;
	// Use this for initialization
	void Start () {
        lastFramePos = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        

        if (isStopped == false && isThrown)
        {
            distanceTravelled += Vector3.Distance(this.transform.position, lastFramePos);
            lastFramePos = this.transform.position;

            if (distanceTravelled >= stopDistance / 2)
            {
                this.gameObject.layer = 0;
            }

            if (distanceTravelled >= stopDistance)
            {
                isStopped = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                isThrown = false;
            }

        }
	}


    //public void setPos(Vector3 p)
    //{
    //    pos = p;
    //}

    public void setDistance(float dis)
    {
        distanceTravelled = 0;
        Debug.Log("distance "+ dis);
        stopDistance = dis;
        lastFramePos = this.transform.position;
        isThrown = true;
        isStopped = false;
    }

    public void setLayer(int i)
    {
        switch (i)
        {
            case 1:
                this.gameObject.layer = 8;
                break;
            case 2:
                this.gameObject.layer = 9;
                break;
            default:
                break;
        }
        
    }
}
