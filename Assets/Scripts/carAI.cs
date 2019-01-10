using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour {
    [SerializeField] bool isReverse = false;
    Vector3 dir = new Vector3(1, 0, 0);

    [SerializeField] float minSpeed = 20f;
    [SerializeField] float maxSpeed = 30f;

    float originalSpeed;
    float lowerSpeed;
    [SerializeField] float speedUsed;

    float DIR_DIFF = 12f;

    // Use this for initialization
    void Start () {
        lowerSpeed = 0;

        if (isReverse)
        {
            dir *= -1;
            lowerSpeed += DIR_DIFF;
        }
        else
        {
            minSpeed -= DIR_DIFF;
            maxSpeed -= DIR_DIFF;
        }

        originalSpeed = Random.Range(minSpeed, maxSpeed);
        speedUsed = originalSpeed;       
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            if (hit.transform.tag == "Player" && (hit.distance <= 3.0f) && (hit.distance >= 0.0f))
            {
                //Debug.Log("Speed decreased");
                //Debug.Log("Name of other obj: " + hit.collider.name);
                speedUsed = lowerSpeed;
            }
            else if (hit.transform.GetComponent<carAI>() != null)
            {
                if (hit.transform.GetComponent<carAI>().getDirection() == isReverse)
                {
                    speedUsed = hit.transform.GetComponent<carAI>().getCurrentSpeed();
                }
                else
                {
                    speedUsed = lowerSpeed;
                }
            
            }
            else if (hit.distance <= 3f)
            {
                //Debug.Log("Speed decreased");
                //Debug.Log("Name of other obj: " + hit.collider.name);
                speedUsed = lowerSpeed;
            }
            else
            {
                //   Debug.Log("Speed increased");
                speedUsed = originalSpeed;
            }

        }
        else
        {
            speedUsed = originalSpeed;
        }
    }

    public float getCurrentSpeed()
    {
        return speedUsed;
    }

    public bool getDirection()
    {
        return isReverse;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
    }


}
