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

    [SerializeField] Animator myAnim;

    bool isCarrying = false;
    [SerializeField] GameObject cranePoint;

    [SerializeField] Transform up, down, right, left;
    [SerializeField] GameObject throwRange;
    [SerializeField] GameObject targetReticle;
    [SerializeField] Transform up2, right2;

    float hitPoints = 100;
    float collisionDamageMultiplier = 1f;

    bool isJumping = false;
    float jumpTimer = 0;
    [SerializeField] float jumpTime = 3f;
    [SerializeField] float jumpDamage = 1.0f;

    float drag;
    bool isOiled = false;

    // Use this for initialization
    void Start() {
        switch (playerNum)
        {
            case PlayerNum.P1:
                this.gameObject.layer = 12;
                break;
            case PlayerNum.P2:
                this.gameObject.layer = 13;
                break;
            default:
                break;
        }

        

        targetReticle.GetComponent<SpriteRenderer>().enabled = false;
        throwRange.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (isCarrying)
        {
            cranePoint.GetComponent<SpriteRenderer>().enabled = true;
            targetReticle.GetComponent<SpriteRenderer>().enabled = true;
            throwRange.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            cranePoint.GetComponent<SpriteRenderer>().enabled = false;
            targetReticle.GetComponent<SpriteRenderer>().enabled = false;
            throwRange.GetComponent<SpriteRenderer>().enabled = false;
        }


        if(isOiled == false)
        {
            movement();
        }
        else
        {
            Collider2D[] temp;
           // ContactFilter2D tempFilter = new ContactFilter2D();
            //int i = GetComponent<BoxCollider2D>().OverlapCollider(tempFilter, temp);
           temp = Physics2D.OverlapBoxAll(this.transform.position, GetComponent<BoxCollider2D>().size, 0f);

            bool isStillOiled = false;
            foreach (Collider2D collider in temp)
            {
                if (collider.GetComponent<oil>())
                {
                    isStillOiled = true;
                }
            }

            if (isStillOiled == false)
            {
                getUnOiled();
            }

        }
        





        //Debug.Log(Input.GetAxis("P1Horizontal2"));
        //Debug.Log(Input.GetAxis("P1Vertical2"));



        // Assign which collider we'll use for picking up w crane
        checkColliders();

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

            Vector2 temp = new Vector2(localX * circularPos.x, circularPos.y * localY);
            Vector3 pos = new Vector3(-temp.x, -temp.y, 0) + this.transform.position;
            cranePoint.transform.position = pos;



        
            float localX2 = right2.transform.position.x - this.transform.position.x;
            float localY2 = up2.transform.position.y - this.transform.position.y;

            Vector2 temp2 = new Vector2(localX2 * circularPos.x, circularPos.y * localY2);
            Vector3 pos2 = new Vector3(temp2.x, temp2.y, 0) + this.transform.position;
            targetReticle.transform.position = pos2;

        }

        //Debug.Log(getOwnAxis("Trigger"));
        if (isJumping == false && isCarrying == false && getOwnAxis("Trigger") < -0.7f)
        {
            pickUp();
        }
        else if (isCarrying && getOwnAxis("Trigger") > -0.25f)
        {
            throwObj();
        }
        else if (isCarrying == false && getOwnAxis("Trigger") > 0.25f)
        {
            isJumping = true;
            myAnim.SetBool("IsJumping", true);
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<SpriteRenderer>().enabled = false;
            jumpTimer = 0;
        }
        else if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpTime)
            {
                isJumping = false;
                myAnim.SetBool("IsJumping", false);
                GetComponent<BoxCollider2D>().enabled = true;
                //GetComponent<SpriteRenderer>().enabled = true;
                takeDamage(jumpDamage);
            }
        }
    }

    void checkColliders()
    {
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
    }

    void movement()
    {
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
    }

    float getOwnAxis(string axis)
    {
        return Input.GetAxis(playerNum.ToString() + axis);
    }

    void pickUp()   // Gimme your best pick up lines
    {
        if (colliderObj2Listen != null && colliderObj2Listen.GetComponent<CraneZone>().isTherePickable())
        {
            Debug.Log("picking up");
            objPicked = colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp();
            isCarrying = true;
            cranePoint.transform.position = objPicked.transform.position;
            objPicked.GetComponent<Rigidbody2D>().isKinematic = true;

            if (objPicked.GetComponent<CircleCollider2D>())
            {
                objPicked.GetComponent<CircleCollider2D>().enabled = false;
            }

            objPicked.transform.parent = cranePoint.transform;
            // ADD MORE LATER
        }

    }

    void throwObj(){
        if(objPicked != null)
        {
            float distance = Vector3.Distance(cranePoint.transform.position, targetReticle.transform.position);


            objPicked.GetComponent<Rigidbody2D>().isKinematic = false;
            objPicked.transform.parent = null;
            

            Vector2 dir = new Vector2(getOwnAxis("Horizontal2"), -getOwnAxis("Vertical2"));
            dir.Normalize();
            //objPicked.GetComponent<throwable>().setPos(targetReticle.transform.position);

            switch (playerNum)
            {
                case PlayerNum.P1:
                    objPicked.GetComponent<throwable>().setLayer(1);
                    break;
                case PlayerNum.P2:
                    objPicked.GetComponent<throwable>().setLayer(1);
                    break;
                default:
                    break;
            }

            if (objPicked.GetComponent<CircleCollider2D>())
            {
                objPicked.GetComponent<CircleCollider2D>().enabled = true;
            }

            objPicked.GetComponent<throwable>().setDistance(distance);
            objPicked.GetComponent<Rigidbody2D>().AddForce(dir * throwForce);
            

            objPicked = null;
            isCarrying = false;
        }
    }

    public void takeDamage(float amount)
    {
        hitPoints -= amount;

        if (hitPoints <= 0)
        {   // DEAD

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        takeDamage(collision.relativeVelocity.magnitude * collisionDamageMultiplier);


    }


    public void getOiled()
    {
        drag = GetComponent<Rigidbody2D>().drag;
        GetComponent<Rigidbody2D>().drag = 0;
        isOiled = true;
    }

    public void getUnOiled()
    {
        GetComponent<Rigidbody2D>().drag = drag;
        isOiled = false;
    }
}
