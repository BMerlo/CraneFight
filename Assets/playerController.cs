using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    enum PlayerNum
    {
        P1,
        P2
    }
    [SerializeField] PlayerNum playerNum;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject r, l, u, d, ru, lu, rd, ld;
    GameObject colliderObj2Listen;
    GameObject objPicked;
    [SerializeField] float throwForce = 1f;


    bool isCarrying = false;
    [SerializeField] GameObject cranePoint;

    [SerializeField] Transform up, down, right, left;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isCarrying)
        {
            cranePoint.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            cranePoint.GetComponent<SpriteRenderer>().enabled = false;
        }



        // MOVEMENT
        if (getOwnAxis("Horizontal") > 0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0));
        }
        else if (getOwnAxis("Horizontal") < -0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, 0));
        }

        if (getOwnAxis("Vertical") > 0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed));
        }
        else if (getOwnAxis("Vertical") < -0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed));
        }



        //Debug.Log(Input.GetAxis("P1Horizontal2"));
        //Debug.Log(Input.GetAxis("P1Vertical2"));



        // Assign which collider we'll use for picking up w crane
        if (getOwnAxis("Horizontal2") < -0.34f)
        {
            if (getOwnAxis("Vertical2") < -0.34f)
            {
                colliderObj2Listen = lu;
            }
            else if (getOwnAxis("Vertical2") > 0.34f)
            {
                colliderObj2Listen = ld;
            }
            else
            {
                colliderObj2Listen = l;
            }
        }
        else if (getOwnAxis("Horizontal2") > 0.34f)
        {
            if (getOwnAxis("Vertical2") < -0.34f)
            {
                colliderObj2Listen = ru;
            }
            else if (getOwnAxis("Vertical2") > 0.34f)
            {
                colliderObj2Listen = rd;
            }
            else
            {
                colliderObj2Listen = r;
            }
        }
        else
        {
            if (getOwnAxis("Vertical2") < -0.34f)
            {
                colliderObj2Listen = u;
            }
            else if (getOwnAxis("Vertical2") > 0.34f)
            {
                colliderObj2Listen = d;
            }
            else
            {
                colliderObj2Listen = null;
            }
        }

        Debug.Log(getOwnAxis("Trigger"));
        if (isCarrying == false && getOwnAxis("Trigger") < -0.7f)
        {
            pickUp();
        }
        else if (isCarrying && getOwnAxis("Trigger") > -0.1f)
        {
            throwObj();
        }

        if (isCarrying)
        {
            //float x = right.transform.position.x;
            //float y = up.transform.position.y;
            //x -= this.transform.position.x;
            //y -= this.transform.position.y;

            //x *= getOwnAxis("Horizontal2");
            //y *= -getOwnAxis("Vertical2");

            //Vector2 pos = this.transform.position + new Vector3(x, y, 0);
            //cranePoint.transform.position = pos;


            Vector2 circularPos = new Vector2(getOwnAxis("Horizontal2"), -getOwnAxis("Vertical2"));
            if(circularPos.magnitude > 1)
            circularPos.Normalize();

            float localX = right.transform.position.x - this.transform.position.x;
            float localY = up.transform.position.y - this.transform.position.y;

            circularPos.x *= localX;
            circularPos.y *= localY;
            Vector3 pos = new Vector3(circularPos.x, circularPos.y, 0) + this.transform.position;
            cranePoint.transform.position = pos;
        }


    }

    float getOwnAxis(string axis)
    {
        return Input.GetAxis(playerNum.ToString() + axis);
    }

    void pickUp()   // Gimme your best pick up lines
    {
        if (colliderObj2Listen != null)
        {
            Debug.Log("picking up");
            objPicked = colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp();
            isCarrying = true;
            cranePoint.transform.position = objPicked.transform.position;
            objPicked.GetComponent<Rigidbody2D>().isKinematic = true;
            objPicked.transform.parent = cranePoint.transform;
            // ADD MORE LATER
        }

    }

    void throwObj(){
        if(objPicked != null)
        {
            objPicked.GetComponent<Rigidbody2D>().isKinematic = false;
            objPicked.transform.parent = null;
            

            Vector2 dir = new Vector2(getOwnAxis("Horizontal2"), -getOwnAxis("Vertical2"));
            dir.Normalize();
            objPicked.GetComponent<Rigidbody2D>().AddForce(dir * throwForce);

            objPicked = null;
            isCarrying = false;
        }
    }



}
