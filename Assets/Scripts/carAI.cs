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
    //new
    //[SerializeField] Vector2 speedUse;
    public ScrollingBackGround scrollspeed;
    public float newSpeed;
    public float testSpeed;
    public float timer = 0;

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

        scrollspeed = FindObjectOfType<ScrollingBackGround>();
        newSpeed = scrollspeed.GetComponent<Rigidbody2D>().velocity.x;

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
                Debug.Log("Name of other obj: " + hit.collider.name);
                //speedUsed = lowerSpeed;
                if (isReverse) //this checks if AI goes left and its position
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(newSpeed/4, 0);
                }
                else if (!isReverse)
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(-newSpeed/4,0);
                }
                
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
                Debug.Log("Name of other obj: " + hit.collider.name);
                speedUsed = lowerSpeed;
            }
            else
            {
                  Debug.Log("Speed increased");
                //speedUsed = originalSpeed;
                ResetSpeed();
            }

        }
        else
        {
           //speedUsed = originalSpeed;
            ResetSpeed();
        }
        testSpeed = this.GetComponent<Rigidbody2D>().velocity.x;
        Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
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

        //GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
        //different speed on base on direction 

            //old
        if (isReverse && transform.position.y<0.01) //this checks if AI goes left and its position
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up*3);
            
        }
        else if(!isReverse && transform.position.y > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * -3);
            
        }
    }
    public void resetSpeedTimer() {
        //waits 3 seconds then reset the speed 
        Invoke("ResetSpeed", 3.0f);
    }
    private void ResetSpeed()
    {
        if (isReverse) //this checks if AI goes left and its position
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * -newSpeed + 1.5f, 0);
        }
        else if (!isReverse)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * -newSpeed - 1.5f, 0);
        }
    }

}
