﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] bool isReverse = false;
    Vector3 dir = new Vector3(1, 0, 0);
    public float desiredSpeed;

    Rigidbody2D myBody;
    [SerializeField] float minSpeed = 20f;
    [SerializeField] float maxSpeed = 30f;

    float accelerateForce = 30f;
    float decelerateForce = 15f;

    float originalSpeed;
    float lowestSpeed;
    //float lowerSpeed;
    [SerializeField] float speedUsedTest;
    //new
    //[SerializeField] Vector2 speedUse;
    public ScrollingBackGround backGroundScript; 
    public float backgroundSpeed;

    public float testSpeed;
    public float timer = 0;

    float DIR_DIFF;

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        //lowerSpeed = 0;
        originalSpeed = Random.Range(minSpeed, maxSpeed);

        gameObject.layer = 19; //make every AI to belong to layer AI

        backGroundScript = FindObjectOfType<ScrollingBackGround>();
        backgroundSpeed = backGroundScript.getSpeed();

        if (isReverse)
        {
            dir *= -1;
            originalSpeed *= -1;    // original speed will carry dir now    // -20
            originalSpeed += backgroundSpeed;   // bgSpeed is negative, keep in mind    //-23
            
            //lowerSpeed += DIR_DIFF;
        }
        else
        {
            originalSpeed += backgroundSpeed;   //  20 - 3 = 17
            //minSpeed -= DIR_DIFF;
            //maxSpeed -= DIR_DIFF;
        }

        speedUsedTest = originalSpeed;
        desiredSpeed = originalSpeed;

        //float force = desiredSpeed * GetComponent<Rigidbody2D>().mass;
        myBody.velocity = new Vector2(desiredSpeed, 0);
        //GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);

       // Debug.Log("-----------------------------------speed set");
        
        //speedUsed = originalSpeed;
    }

    void accelerate()
    {
        myBody.AddForce(dir * accelerateForce);
        //Debug.Log("---------accelerate " + (dir * accelerateForce));
    }


    // Update is called once per frame
    void Update()
    {
     
    }

    public float getCurrentSpeed()
    {
        //return speedUsed;
        return desiredSpeed;
    }

    public bool getDirection()
    {
        return isReverse;
    }

    private void FixedUpdate()
    {

        //GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
        //different speed on base on direction 

        //old   - is it tho?
        //if (isReverse && transform.position.y < 0.01) //this checks if AI goes left and its position
        //{
        //    myBody.AddForce(transform.up * 3);

        //}
        //else if (!isReverse && transform.position.y > 0)
        //{
        //    myBody.AddForce(transform.up * -3);

        //}

        float currentSpeed = GetComponent<Rigidbody2D>().velocity.x;

        // google self drive has nothing on me
        if (isReverse)  // NEgative speed
        {
            // if (desiredSpeed < currentSpeed)
            //{
            //    accelerate();
            //}
            //else if (desiredSpeed > currentSpeed * 0.8)
            //{
            //    decelerate();
            //}

            if(myBody.velocity.x > originalSpeed)
            {
                accelerate();
            }
            
        }
        //else
        //{
        //    if (desiredSpeed > currentSpeed)
        //    {
        //        accelerate();
        //    }
        //    else if (desiredSpeed < currentSpeed * 0.8)
        //    {
        //        //decelerate();
        //    }
        //}

    }
    public void resetSpeedTimer()
    {
        //waits 3 seconds then reset the speed 
        Invoke("ResetSpeed", 3.0f);
    }
    private void ResetSpeed()
    {
        if (isReverse) //this checks if AI goes left and its position
        {
           // this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * -newSpeed + 1.5f, 0);
        }
        else if (!isReverse)
        {
          //  this.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * -newSpeed - 1.5f, 0);
        }
    }

}
