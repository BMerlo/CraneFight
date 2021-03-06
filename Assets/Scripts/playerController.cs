﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class playerController : MonoBehaviour {
    [SerializeField] GameObject crownObj;
    Player player;
    int playerId;

    float hitPoints = 100f;
    [SerializeField] public HealthBar playerHealth;

    float aimSpeed = 160000f;
    Rigidbody2D rBody;
    bool isBoostCharging = false;
    float boostTimer = 0;
    float MAX_BOOST_TIME = 2.0f;

    [SerializeField] GameObject explosion;

    enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }

    [SerializeField] PlayerNum playerNum;
    private float horizontalDeadZone = 0.3f;
    private float verticalDeadZone = 0.3f;

    float moveSpeed = 100f;
    float horizontalMoveSpeed = 5f;
    float maxHorizontalSpeed = 4f;

    float evadeSpeed = 50f;
    [SerializeField] bool hasBoosted = false;
    [SerializeField] GameObject r, l, u, d, ru, lu, rd, ld;
    [SerializeField] GameObject colliderObj2Listen;
    GameObject objPicked;

    [SerializeField] Animator myAnim;

    bool isCarrying = false;
    [SerializeField] GameObject cranePoint;

    [SerializeField] Transform up, down, right, left;
    [SerializeField] GameObject throwRange;
    [SerializeField] GameObject targetReticle;
    [SerializeField] Transform upLarger, rightLarger;


    float collisionDamageMultiplier = 1f;

    bool isJumping = false;
    float jumpTimer = 0;
    float jumpTime = .5f;
	
    [SerializeField] GameObject spriteHolder;//Handling the jump here
    SpriteRenderer jumpSprite;

    Vector2 movementVector = Vector2.zero;

    float drag;
    public bool isOiled = false;
    bool isCoroutineRunning = false;
    int oilCount = 1;
    [SerializeField] float oilForce = 1.0f;
    [SerializeField] float oilForceTime = 0.5f;
    float oilTimer = 0.0f;

    [SerializeField] GameObject oilSprite;

    [SerializeField] float oilSpeed = 1.7f;
    private int oilDirectionModifier = 1;



    [SerializeField] GameObject craneActual;
    [SerializeField] Sprite craneL, craneLU, craneU, craneRU, craneR, craneRD, craneD, craneLD;
    [SerializeField]
    Animator
        Rcrane, RUcrane, RDcrane,
        Lcrane, LUcrane, LDcrane,
        Ucrane, Dcrane; //Animators for the 8 crane directions
    Animator activeAnimator;

    //public GameObject gameManager;
    public Game_Manager myManager;

    private Rigidbody2D m_rb;

    float throwForce = 1400f;
    //[SerializeField] float maxThrowTime = 3.0f;
    //[SerializeField] float minChargeTime = 1.0f;
    //[SerializeField] float minThrowForce = 1650f;
    //[SerializeField]float timeCharging;

    private float evadeCooldown = 0.6f;
    private bool hasEvaded = false;
    private float evadeCooldownTimer = 0;

    [SerializeField] private float oilCooldown = 15f; //15 by default
    private float oilCooldownTimer = 0;

    [SerializeField] private float smellyCooldown = 15f; //15 by default
    private float smellyCooldownTimer = 0;

    //float OilTimer = 0; // Amount of time the Oil Status lasts
    //float smellyTimer = 0; // Amount of time the Smelly Status lasts

    [SerializeField] private float jumpCooldown = 1f;
    private bool hasJumped = false;
    private float jumpCooldownTimer = 0;
    [SerializeField] private float throwCooldown = 0.5f;
    private float throwCooldownTimer = 0;
    private bool hasThrown = false;
    private bool isCharging = false;
    [SerializeField] private float isDestroyedFreq = 1.0f; //How often the game checks if the object the crane was holding is destroyed
    private float isDestroyedTimer = 0.0f;

    public GameObject m_arrow;
    private Vector3 m_arrowOriginalScale;

    private moveBack MoveBackScript;
    private float m_stunTime;


    Vector2 smellForceUp = new Vector2(0.0f, 0.1f);
    Vector2 smellForceDown = new Vector2(0.0f, -0.1f);
    float smellRadius = 3.3f;
    public bool isSmelly;

    PolygonCollider2D myCollider;
    [SerializeField] GameObject smellCloudB;
    [SerializeField] GameObject smellCloudF;

    //references to snap
    ReferencePosition referencePos;
    float referenceTop;
    float referenceTop1;
    float referenceTop2;
    float referenceMid;
    float referenceBot1;
    float referenceBot2;
    float referenceBot;

    float midPointLane1;
    float midPointLane2;
    float midPointLane3;
    float midPointLane4;
    float midPointLane5;
    float midPointLane6;

    public int currentLane;
    float forceToCenter = 50;

    //new movement    
    public bool canMoveAgain;
    private float rangeToBeAbleToMoveAgain = 0.05f;
    public float verticalMoveSpeed = 15.5f;
    public float maxVerticalMoveSpeed = 20;
    float laneForceTimer = 0;
    float laneForceDelay = 0.5f;
	int tempLayer = 0;


    arrangeLayers layerScript;
    Animator[] craneAnims;


    float maxVelocity = 25f;

    bool isInvulnerable = false;
    float invTimer = 2.5f;
    float invCounter = 0;

    //[SerializeField] ContactFilter2D tempFilter = new ContactFilter2D();
    // Use this for initialization
    void Start() {
        activeAnimator = Dcrane;

        craneAnims = new Animator[] {Rcrane, RUcrane, RDcrane,
        Lcrane, LUcrane, LDcrane,
        Ucrane, Dcrane};

        oilSprite.SetActive(false);
        layerScript = GetComponent<arrangeLayers>();
		tempLayer = gameObject.layer;
		myManager = FindObjectOfType<Game_Manager>();
        myCollider = GetComponent<PolygonCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
		jumpSprite = spriteHolder.GetComponent<SpriteRenderer>();
        switch (playerNum)
        {
            case PlayerNum.P1:
                this.gameObject.layer = 12;
                playerId = 0;
                break;
            case PlayerNum.P2:
                this.gameObject.layer = 13;
                playerId = 1;
                break;
            case PlayerNum.P3:
                this.gameObject.layer = 14;
                playerId = 2;
                break;
            case PlayerNum.P4:
                this.gameObject.layer = 15;
                playerId = 3;
                break;
            default:
                break;
        }

        //Rewired stuff
        player = ReInput.players.GetPlayer(playerId);

        
        m_rb = GetComponent<Rigidbody2D>();
        targetReticle.GetComponent<SpriteRenderer>().enabled = false;
        throwRange.GetComponent<SpriteRenderer>().enabled = false;
        //m_arrowOriginalScale = m_arrow.transform.localScale;  // No chargin anymore, no need
        // MoveBackScript = GetComponent<moveBack>();
        //isSmelly = true;

        //setting references
        referencePos = FindObjectOfType<ReferencePosition>();
        //get reference according to objects on references object
        referenceTop = referencePos.getReferenceTL();
        referenceTop1 = referencePos.getReferenceT1();
        referenceTop2 = referencePos.getReferenceT2();
        referenceMid = referencePos.getReferenceMid();
        referenceBot1 = referencePos.getReferenceB1();
        referenceBot2 = referencePos.getReferenceB2();
        referenceBot = referencePos.getReferenceBL();

        //calculate mid point
        midPointLane1 = referenceTop1 + ((referenceTop - referenceTop1) / 2);        
        midPointLane2 = referenceTop2 + ((referenceTop1 - referenceTop2) / 2);
        midPointLane3 = referenceMid + ((referenceTop2 - referenceMid) / 2);
        midPointLane4 = referenceBot1 + ((referenceMid - referenceBot1) / 2);
        midPointLane5 = referenceBot2 + ((referenceBot1 - referenceBot2) / 2);
        midPointLane6 = referenceBot + ((referenceBot2 - referenceBot) / 2);
       /* Debug.Log(midPointLane1);
        Debug.Log(midPointLane2);
        Debug.Log(midPointLane3);
        Debug.Log(midPointLane4);
        Debug.Log(midPointLane5);
        Debug.Log(midPointLane6);*/

        //forceToCenter = 50;     // Boris, why here?       
    }

    public float getHitpoints()
    {
        return hitPoints;
    }

    // Update is called once per frame
    void Update() {

        if (isInvulnerable)
        {
            invCounter += Time.deltaTime;
            if (Time.fixedTime % .5 < .2)
            {
                transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }

            if (invCounter >= invTimer)
            {
                isInvulnerable = false;
                GetComponent<PolygonCollider2D>().enabled = true;
                transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        //canMove();
        //REGENERATION
        //if (regenCounter > 0)
        //{
        //    regenCounter -= Time.deltaTime;
        //}
        //else
        //{
        //    getHealth(Time.deltaTime * regenAmount);
        //}

        //Debug.Log(getOwnAxis("Horizontal"));


        cooldownTimers();
        updateMovementVec();
        laneForceTimer += Time.deltaTime;

        //if (true)
        //{
        //    carAI[] cars = GameObject.FindObjectsOfType<carAI>();

        //    foreach (carAI car in cars)
        //    {
        //        if (true) //range
        //        {
        //            // add force
        //        }
        //    }
        //}
        if (!isBoostCharging)
        {
            if(!hasEvaded && player.GetButtonDown("Dash"))
            {
                isBoostCharging = true;

            }
        }
        else // Boost is charging
        {
            boostTimer += Time.deltaTime;

            if(player.GetButtonUp("Dash"))
            {
                boost();
            }
        }
        
        // This is expensive. Fix if we have time
        //if (isCarrying)
        //{
        //    //cranePoint.GetComponent<SpriteRenderer>().enabled = true;
        //   // targetReticle.GetComponent<SpriteRenderer>().enabled = true;
        //   // throwRange.GetComponent<SpriteRenderer>().enabled = true;
        //}
        //else
        //{
        //    //cranePoint.GetComponent<SpriteRenderer>().enabled = false;
        //    targetReticle.GetComponent<SpriteRenderer>().enabled = false;
        //    throwRange.GetComponent<SpriteRenderer>().enabled = false;
        //}


        if(isOiled)
        {
            oilCooldownTimer -= 1 * Time.deltaTime; // oily countdown

            Collider2D[] temp = new Collider2D[30];
            ContactFilter2D tempFilter = new ContactFilter2D();
            tempFilter.useTriggers = true;
            //int i = GetComponent<BoxCollider2D>().OverlapCollider(tempFilter, temp);
            //temp = Physics2D.OverlapBoxAll(this.transform.position, GetComponent<BoxCollider2D>().size, 0f);
            int numColliders = GetComponent<PolygonCollider2D>().OverlapCollider(tempFilter, temp);
            bool isStillOiled = false;
            for (int i = 0; i < numColliders; i++)
            {
                if (temp[i].gameObject.GetComponent<oil>())
                {
                    oilCooldownTimer = oilCooldown;
                    isStillOiled = true;
                }
            }
          if (isStillOiled == false && oilCooldownTimer <= 0)
           {
               getUnOiled();
               oilCooldownTimer = 0;
            }

        }

        // Assign which collider we'll use for picking up w crane
        if (!isCarrying)
        {
            checkColliders();
        }
        else
        {
            //float x = right.transform.position.x;
            //float y = up.transform.position.y;
            //x -= this.transform.position.x;
            //y -= this.transform.position.y;

            //x *= getOwnAxis("Horizontal2");
            //y *= -getOwnAxis("Vertical2");

            //Vector2 pos = this.transform.position + new Vector3(x, y, 0);
            //cranePoint.transform.position = pos;


            aim();

        }

        //Debug.Log(getOwnAxis("Trigger"));
        //if (!isJumping && isCarrying && getOwnAxis("Trigger") < 0.25f && !hasThrown)    // Charge the attack?   NO MORE CHARGING
        //{
        //    //timeCharging += Time.deltaTime;
        //    isCharging = true;
        //    //if (m_arrow.transform.localScale.y < 0.3 && timeCharging > minChargeTime)
        //    //    m_arrow.transform.localScale += new Vector3(0f, 0.01f, 0);
        //}
        //else if (!isJumping && isCarrying && isCharging && !hasThrown)
        //{
        //    //chargingForce();  // NO MORE CHARGING
        //    throwObj();
        //}

        if (!isJumping && isCarrying && !hasThrown && player.GetButton("Throw"))
        {
            throwObj();

            foreach (Animator anim in craneAnims)
            {
                anim.SetTrigger("throwTrigger");
            }
        }
		
        else if (isCarrying == false && !hasJumped && !isJumping && player.GetButton("Jump"))
            //getOwnAxis("Trigger") > 0.25f)
        {
            GetComponent<AudioSource>().Play();
            isJumping = true;
            //jumpSprite.sortingLayerName = "Air";//changed sprite sorting layer
            layerScript.jump();

            //myAnim.SetBool("IsJumping", true);
            myAnim.SetTrigger("IsJumpingTrigger");
			//GetComponent<BoxCollider2D>().enabled = false;
			myCollider.enabled = false;
			//GetComponent<SpriteRenderer>().enabled = false;
			//gameObject.layer = 18;//changing gameobject physics layer to "Air"
			jumpTimer = 0;
        }
        else if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpTime)
            {

				//gameObject.layer = tempLayer;//changing gameobject physics layer to default
				isJumping = false;
                jumpSprite.sortingLayerName = "Default";//changed sprite sorting layer to default
                layerScript.unJump();
				hasJumped = true;
                //myAnim.SetBool("IsJumping", false);
                //GetComponent<BoxCollider2D>().enabled = true;
                myCollider.enabled = true;
                //GetComponent<SpriteRenderer>().enabled = true;
                //takeDamage(jumpDamage);
            }
        }
        

        //reference script
        checkLane();       
    }


    private void FixedUpdate()
    {
        movement();

        //if (m_stunTime <= 0.0f)
        //{
        //    //MoveBackScript.enabled = false;
        //    //movement();
        //}
        //else
        //{
        //    m_stunTime -= Time.deltaTime;
        //}
        //if (isOiled == false)
        //{
        //    movement();
        //}
        //else
        //{
        //    oil
        //  += Time.deltaTime;
        //    if (oilTimer >= oilForceTime)
        //    {
        //        oiledMovement();
        //        oilTimer = 0.0f;
        //    }
        //}


        //if (isSmelly)
        //{
        //    carAI[] cars = GameObject.FindObjectsOfType<carAI>();

        //    foreach (carAI car in cars)
        //    {
        //        //if (GetDistanceFromClosest(GameObject.FindGameObjectsWithTag("AICar")) <= smellRadius)
        //        if(Vector2.Distance(car.transform.position, this.transform.position) <= smellRadius)
        //        {
        //            //Checks if car AI is in front of player
        //            if (car.transform.position.x > this.transform.position.x)
        //            {
        //                //Transforms if AICar position.y is above player
        //                if (car.transform.position.y > this.transform.position.y)
        //                {
        //                    car.GetComponent<Transform>().Translate(smellForceUp);
        //                  //  Debug.Log("CAR NAME: " + car.name + smellForceUp + "moving up: " + car.transform.position);
        //                }

        //                //Transforms if AICar position.y is below player
        //                else if (car.transform.position.y < this.transform.position.y)
        //                {
        //                    car.GetComponent<Transform>().Translate(smellForceDown);
        //                 //   Debug.Log("CAR NAME: " + car.name + smellForceDown + "moving down: " + car.transform.position);
        //                }
        //            }

        //            //Checks if AICar position.x is behind the player
        //            else if (car.transform.position.x < this.transform.position.x)
        //            {
        //                //Transforms if AICar position.y is above player
        //                if (car.transform.position.y > this.transform.position.y)
        //                {
        //                    car.GetComponent<Transform>().Translate(smellForceUp);
        //                  //  Debug.Log("CAR NAME: " + car.name + smellForceUp + "moving up: " + car.transform.position);
        //                }

        //                //Transforms if AICar position.y is below player
        //                else if (car.transform.position.y < this.transform.position.y)
        //                {
        //                    car.GetComponent<Transform>().Translate(smellForceDown);
        //                   // Debug.Log("CAR NAME: " + car.name + smellForceDown + "moving down: " + car.transform.position);
        //                }

        //            }
        //        }
        //    }
        //}
        if (rBody.velocity.magnitude > maxVelocity)
        {
            Vector2 temp = rBody.velocity;
            temp.Normalize();
            rBody.velocity = temp * maxVelocity;
        }
    }

    public void setInvulerable()
    {
        isInvulnerable = true;
        invCounter = 0;
    }

    float GetDistanceFromClosest(GameObject[] gameObjects)
    {
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject go in gameObjects)
        {
            shortestDistance = Mathf.Min(shortestDistance, Vector2.Distance(transform.position, go.transform.position));
        }
        return shortestDistance;
    }

    public void becomeSmelly()// work here 
    {
        isSmelly = true;
        smellyCooldownTimer = smellyCooldown; // Timer starts  at 15 seconds
      //  Debug.Log("Something is smelly");
        smellCloudF.SetActive(true);
        smellCloudB.SetActive(true);
    }

    public void becomeUnSmelly()
    {
        isSmelly = false;
        Debug.Log("No more Smell");
        smellCloudF.SetActive(false);
        smellCloudB.SetActive(false);
    }

    void updateMovementVec()
    {
        //movementVector = new Vector2(getOwnAxis("Horizontal"), getOwnAxis("Vertical"));
        //movementVector = new Vector2(0f, getOwnAxis("Vertical"));        
        movementVector = new Vector2(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"));
    }

    void boost()
    {
        //float force = (evadeSpeed / 3.0f) + ((evadeSpeed * 2.0f * (boostTimer / MAX_BOOST_TIME)) / 3.0f);
        Vector2 temp = movementVector.normalized;
        temp = new Vector2(temp.x * 1.25f, temp.y * 1.0f);
        GetComponent<Rigidbody2D>().AddForce(temp * evadeSpeed, ForceMode2D.Impulse);
        hasEvaded = true;
        isBoostCharging = false;
        boostTimer = 0;
        //Change color for dahs feedback
        spriteHolder.GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.1f, 0.1f);
    }

    Vector2 getRightStickDir()    // returns a normalized vector from right stick.
    {
        Vector2 dir = new Vector2(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"));

        //if (Vector3.Magnitude(dir) > 1)
        //{
            dir.Normalize();
        //}

        return dir;
    }

    void switchActiveAnim(Animator anim)
    {
        if (activeAnimator != anim)
        {
            activeAnimator.gameObject.SetActive(false);
            anim.gameObject.SetActive(true);
            activeAnimator = anim;
        }
    }

    void aim()
    {

        //m_arrow.SetActive(true);

        //COME BACK TO CLEAN THIS WHOLE PART IF WE HAVE TIME
        Vector2 dir = getRightStickDir();


        //float localX = right.transform.position.x - this.transform.position.x;
        //float localY = up.transform.position.y - this.transform.position.y;

        //Vector2 temp = new Vector2(localX * dir.x, dir.y * localY);
        //Vector3 pos = new Vector3(-temp.x, -temp.y, 0) + this.transform.position;
        //cranePoint.transform.position = pos;

        targetReticle.transform.Translate(dir * Time.deltaTime * aimSpeed);

        Vector3 temp = targetReticle.transform.position - this.transform.position;
        temp.Normalize();

        cranePoint.transform.position = this.transform.position - temp;
        //cranePoint.transform.position = craneT[tIndex].transform.position;
        //cranePoint.transform.position = activeAnimator.gameObject.transform.GetChild(0).transform.position;

        Vector2 craneAngle = cranePoint.transform.position - this.transform.position;
        //MOVE CRANE ACCORDING TO CRANE POINT //
        float angle = findDegree(craneAngle.x, craneAngle.y);
        //Debug.Log(angle);

        m_arrow.transform.localEulerAngles = new Vector3(0,0,-angle - 180);

        if (angle < 22.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneU;
            switchActiveAnim(Dcrane);
        }
        else if (angle < 67.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneRU;
            switchActiveAnim(LDcrane);
        }
        else if (angle < 112.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneR;
            switchActiveAnim(Lcrane);
        }
        else if (angle < 157.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneRD;
            switchActiveAnim(LUcrane);
        }
        else if (angle < 202.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneD;
            switchActiveAnim(Ucrane);
        }
        else if (angle < 247.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneLD;
            switchActiveAnim(RUcrane);
        }
        else if (angle < 292.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneL;
            switchActiveAnim(Rcrane);
        }
        else if (angle < 337.5f)
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneLU;
            switchActiveAnim(RDcrane);
        }
        else
        {
            //craneActual.GetComponent<SpriteRenderer>().sprite = craneU;
            //switchActiveAnim(LDcrane);
        }





        ///////////////////////////////////////////////
        while (isInsideElipse(this.transform.position.x, this.transform.position.y, targetReticle.transform.position.x, targetReticle.transform.position.y,
            rightLarger.position.x - this.transform.position.x, upLarger.position.y - this.transform.position.y) == false)
        {
            targetReticle.transform.position = (0.99f * (targetReticle.transform.position - this.transform.position)) + this.transform.position;
        }

        if (objPicked && activeAnimator)
        {
        objPicked.transform.position = activeAnimator.gameObject.transform.GetChild(0).transform.position;
        }
        else
        {
            Debug.Log("YOU SHOULDT SEE THIS....");
        }
        //Debug.Log("Is inside eclipse? " + tem);
    }

    float findDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) * 180f) / Mathf.PI);
        if (value < 0)
        {
            value += 360f;
        }
        return value;
    }

    public int getPlayerNum()
    {
        switch (playerNum)
        {
            case PlayerNum.P1:
                return 1;
            case PlayerNum.P2:
                return 2;
            case PlayerNum.P3:
                return 3;
            case PlayerNum.P4:
                return 4;
            default:
                break;
        }
        return 0;
    }

    //void oldAim()
    //{
    //    Vector2 circularPos = new Vector2(getOwnAxis("Horizontal2"), -getOwnAxis("Vertical2"));
    //    if (circularPos.magnitude > 1)
    //        circularPos.Normalize();

    //    float localX = right.transform.position.x - this.transform.position.x;
    //    float localY = up.transform.position.y - this.transform.position.y;

    //    Vector2 temp = new Vector2(localX * circularPos.x, circularPos.y * localY);
    //    Vector3 pos = new Vector3(-temp.x, -temp.y, 0) + this.transform.position;
    //    cranePoint.transform.position = pos;




    //   // float localX2 = right2.transform.position.x - this.transform.position.x;
    //   // float localY2 = up2.transform.position.y - this.transform.position.y;

    //   // Vector2 temp2 = new Vector2(localX2 * circularPos.x, circularPos.y * localY2);
    //   // Vector3 pos2 = new Vector3(temp2.x, temp2.y, 0) + this.transform.position;
    //  //  targetReticle.transform.position = pos2;
    //}

    void checkColliders()
    {
        if (player.GetAxis("AimHorizontal") < -0.34f)
        {
            if (player.GetAxis("AimVertical") > 0.34f)
            {
                colliderObj2Listen = lu;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneLU;
                switchActiveAnim(RDcrane);
            }
            else if (player.GetAxis("AimVertical") < -0.34f)
            {
                colliderObj2Listen = ld;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneLD;
                switchActiveAnim(RUcrane);
            }
            else
            {
                colliderObj2Listen = l;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneL;
                switchActiveAnim(Rcrane);
            }
            if(!hasThrown)
                pickUp();
        }
        else if (player.GetAxis("AimHorizontal") > 0.34f)
        {
            if (player.GetAxis("AimVertical") > 0.34f)
            {
                colliderObj2Listen = ru;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneRU;
                switchActiveAnim(LDcrane);
            }
            else if (player.GetAxis("AimVertical") < -0.34f)
            {
                colliderObj2Listen = rd;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneRD;
                switchActiveAnim(LUcrane);
            }
            else
            {
                colliderObj2Listen = r;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneR;
                switchActiveAnim(Lcrane);
            }
            if (!hasThrown)
                pickUp();
        }
        else
        {
            if (player.GetAxis("AimVertical") > 0.34f)
            {
                colliderObj2Listen = u;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneU;
                switchActiveAnim(Dcrane);
                if (!hasThrown)
                    pickUp();
            }
            else if (player.GetAxis("AimVertical") < -0.34f)
            {
                colliderObj2Listen = d;
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneD;
                switchActiveAnim(Ucrane);
                if (!hasThrown)
                    pickUp();
            }
            else
            {
                //colliderObj2Listen = null;
                colliderObj2Listen = d; //Do we need this?
                //craneActual.GetComponent<SpriteRenderer>().sprite = craneD;
                switchActiveAnim(Ucrane);
            }
        }
        
    }

    
    //new movement
    void canMove() {
        if (transform.position.y <= midPointLane1 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane1 - rangeToBeAbleToMoveAgain) //0..5f by default
        {
            canMoveAgain = true;
        }
        else if (transform.position.y <= midPointLane2 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane2 - rangeToBeAbleToMoveAgain)
        {
            canMoveAgain = true;
        }
        else if (transform.position.y <= midPointLane3 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane3 - rangeToBeAbleToMoveAgain)
        {
            canMoveAgain = true;
        }
        else if (transform.position.y <= midPointLane4 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane4 - rangeToBeAbleToMoveAgain)
        {
            canMoveAgain = true;
        }
        else if (transform.position.y <= midPointLane5 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane5 - rangeToBeAbleToMoveAgain)
        {
            canMoveAgain = true;
        }
        else if (transform.position.y <= midPointLane6 + rangeToBeAbleToMoveAgain && transform.position.y >= midPointLane6 - rangeToBeAbleToMoveAgain)
        {
            canMoveAgain = true;
        }
        else {
            canMoveAgain = false;
        }
    }
    //until here

    void movement()
    {
        //float modifiedSpeed = moveSpeed;
        //if (isOiled)
        //{
        //    modifiedSpeed *= oilSpeed * oilDirectionModifier;
        //        //* (hitPoints /100f);
        //}
        //else
        //{
        //    modifiedSpeed *= (hitPoints / 100f);
        //}

        // MOVEMENT
        //if (getOwnAxis("Horizontal") > horizontalDeadZone)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0));
        //   // Debug.Log(GetComponent<Rigidbody2D>().velocity);
        //}
        //else if (getOwnAxis("Horizontal") < -horizontalDeadZone)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, 0));
        //}

        //if (getOwnAxis("Vertical") > horizontalDeadZone)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed));
        //}
        //else if (getOwnAxis("Vertical") < -horizontalDeadZone)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed));
        //}

        // OLD MOVEMENT
        //rBody.AddForce(movementVector * moveSpeed);

        //Vector2 v = rBody.velocity;
        Vector2 v = new Vector2(0, 0);

        if (movementVector.x > 0)
        {
            if (rBody.velocity.x < movementVector.x * maxHorizontalSpeed)
            {
                v.x = movementVector.x * maxHorizontalSpeed;
            }
        }
        else if (movementVector.x < 0)
        {
            if (rBody.velocity.x > movementVector.x * maxHorizontalSpeed * 1.5f)
            {
                v.x = movementVector.x * maxHorizontalSpeed;
            }
        }

        if (movementVector.y > 0)
        {
            laneForceTimer = 0;
            if (rBody.velocity.y < movementVector.y * maxHorizontalSpeed * .7f)
            {
                v.y = movementVector.y * maxHorizontalSpeed * .75f;
            }
        }
        else if (movementVector.y < 0)
        {
            laneForceTimer = 0;
            if (rBody.velocity.y > movementVector.y * maxHorizontalSpeed * .7f)
            {
                v.y = movementVector.y * maxHorizontalSpeed * .75f;
            }
        }
        else if(laneForceTimer > laneForceDelay)
        {
            pushToMid();
        }

        // Nope velocity change now
        //rBody.velocity = v;
        rBody.velocity += v;


        //if (vel.x < maxHorizontalSpeed && vel.x > -maxHorizontalSpeed)
        //{
        //    vel.x = getOwnAxis("Horizontal") * horizontalMoveSpeed;
        //}

        //new movement
        //if (canMoveAgain) {
        //    if (vel.y < maxVerticalMoveSpeed && vel.y > -maxVerticalMoveSpeed)
        //    {
        //        vel.y = getOwnAxis("Vertical") * verticalMoveSpeed;
        //    }            
        //}

        //if ((getOwnAxis("Vertical") > 0.2f && canMoveAgain)|| (getOwnAxis("Vertical") < -0.2f && canMoveAgain))
        //{
        //    addForceTimer = 0;
        //}
        //until here


        //rBody.velocity = vel;

       // Debug.Log("move");

        //if (getOwnAxis("RBumper") > 0 && !hasEvaded)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -evadeSpeed), ForceMode2D.Impulse);
        //    hasEvaded = true;
            
        //}
        //else if (getOwnAxis("LBumper") > 0 && !hasEvaded)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, evadeSpeed), ForceMode2D.Impulse);
        //    hasEvaded = true;
        //}
        //else if (getOwnAxis("Trigger") < 0 && !hasEvaded)
        //{
            
        //}

        /* added diagonal up and down controls (in progress)
           if (getOwnAxis("RBumper") > 0 && !hasEvaded )
        {
            if (getOwnAxis("Horizontal") < 0.8)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -evadeSpeed), ForceMode2D.Impulse); //d
            }
            if (getOwnAxis("Horizontal") > 0 && getOwnAxis("Vertical") > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(evadeSpeed / 2, -evadeSpeed / 2), ForceMode2D.Impulse); //dr
            }
            else if (getOwnAxis("Horizontal") > 0 && getOwnAxis("Vertical") < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(evadeSpeed / 2, evadeSpeed / 2), ForceMode2D.Impulse); //ur
            }

            hasEvaded = true;
        }
     
         */

        //if (hitPoints < maximumHitPoints)
        //{
        //    this.transform.Translate(new Vector3(-1, 0, 0) * 0.04f * ((100f-hitPoints) / 100f));
        //}
    }

    //Coroutine deals with the movement of a player when they are oiled up


    //void oiledMovement()
    //{
    //    if (oilCount % 2 == 0)
    //    {
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed * oilForce *(hitPoints / 100f)));
    //    }
    //    else
    //    {
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed *oilForce* (hitPoints / 100f)));
    //    }
    //    oilCount++;
    //    oilForce += 0.1f;
    //}

    float getOwnAxis(string axis)
    {
        return Input.GetAxis(playerNum.ToString() + axis);
    }

    bool getOwnButtonDown(string i)
    {
        return Input.GetButtonDown(playerNum.ToString() + i);
    }

    bool getOwnButtonUp(string i)
    {
        return Input.GetButtonUp(playerNum.ToString() + i);
    }

    bool getOwnButton(string i)
    {
        return Input.GetButton(playerNum.ToString() + i);
    }

    void pickUp()   // Gimme your best pick up lines, programmer intern. > "I hope we can merge without any conflicts ( ͡° ͜ʖ ͡°) " > Not bad playa -- E
    {
        if (colliderObj2Listen != null && colliderObj2Listen.GetComponent<CraneZone>().isTherePickable())
        {
            Debug.Log("attempting to pick up");
            //if (colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp().GetComponent<throwable>() 
            //    && colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp().GetComponent<throwable>().isItThrown())

            //Checks if the current player can pick up the object
            //if (colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp().GetComponent<throwable>().canIPickup((int)playerNum))
            // {
            objPicked = colliderObj2Listen.GetComponent<CraneZone>().getObj2PickUp();
            m_arrow.SetActive(true);



            objPicked.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
                isCarrying = true;
                cranePoint.transform.position = objPicked.transform.position;
                objPicked.GetComponent<Rigidbody2D>().isKinematic = true;

                if (objPicked.GetComponent<CircleCollider2D>())
                {
                    objPicked.GetComponent<CircleCollider2D>().enabled = false;
                }

                if (objPicked.GetComponent<BoxCollider2D>())
                {
                    objPicked.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (objPicked.GetComponent<moveBack>())
                {
                    objPicked.GetComponent<moveBack>().enabled = false;
                }


                objPicked.transform.parent = cranePoint.transform;
                // ADD MORE LATER
           // }



        }

    }

    //void chargingForce()
    //{
    //    isCharging = false;
    //    if (timeCharging > maxThrowTime)
    //        throwForce = maxThrowForce;
    //    else if (timeCharging < minChargeTime)
    //        throwForce = minThrowForce;
    //    else
    //        throwForce = (timeCharging / maxThrowTime) * maxThrowForce;
    //    timeCharging = 0f;
    //    m_arrow.transform.localScale = m_arrowOriginalScale;
    //}

    void throwObj()
    {
        if (objPicked != null)
        {
            //float distance = Vector3.Distance(cranePoint.transform.position, targetReticle.transform.position);

            objPicked.GetComponent<Rigidbody2D>().isKinematic = false;
            objPicked.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            objPicked.transform.parent = null;

            Vector2 dir = targetReticle.transform.position - cranePoint.transform.position;
            dir.Normalize();

            switch (playerNum)
            {
                case PlayerNum.P1:
                    objPicked.GetComponent<throwable>().setLayer(1);
                    break;
                case PlayerNum.P2:
                    objPicked.GetComponent<throwable>().setLayer(2);
                    break;
                case PlayerNum.P3:
                    objPicked.GetComponent<throwable>().setLayer(3);
                    break;
                case PlayerNum.P4:
                    objPicked.GetComponent<throwable>().setLayer(4);
                    break;
                default:
                    break;
            }

            objPicked.GetComponent<throwable>().setThrown();

            if (objPicked.GetComponent<CircleCollider2D>())
            {
                objPicked.GetComponent<CircleCollider2D>().enabled = true;
            }
            if (objPicked.GetComponent<BoxCollider2D>())
            {
                objPicked.GetComponent<BoxCollider2D>().enabled = true;
            }

            if (objPicked.GetComponent<moveBack>())
            {
                objPicked.GetComponent<moveBack>().enabled = true;
            }

            //objPicked.GetComponent<throwable>().setDistance(distance);
            objPicked.GetComponent<Rigidbody2D>().AddForce(dir * throwForce);    // NOW< WE ALWAYS THROW WITH MAX FORCE

            objPicked = null;
            isCarrying = false;
            hasThrown = true;
            m_arrow.SetActive(false);
        }
    }

    //Used if object gets destoryed while in possession of crane, resets crane to default state
    void resetCrane()
    {
        if (objPicked == null)
        {
            isCarrying = false;
            hasThrown = true;
            m_arrow.SetActive(false);
        }
    }


    bool isInsideElipse(float centreX, float centreY, float posX, float posY, float horDis, float verDis)
    {
        float p = (Mathf.Pow((posX - centreX), 2) / Mathf.Pow(horDis, 2)) + (Mathf.Pow(posY - centreY, 2) / Mathf.Pow(verDis, 2));

        //Debug.Log(p);
        if (p > 0.98f)
        {
            return false;
        }

        return true;
    }
    //void oldthrowObj(){
    //    if(objPicked != null)
    //    {
    //        float distance = Vector3.Distance(cranePoint.transform.position, targetReticle.transform.position);


    //        objPicked.GetComponent<Rigidbody2D>().isKinematic = false;
    //        objPicked.transform.parent = null;
            

    //        Vector2 dir = new Vector2(getOwnAxis("Horizontal2"), -getOwnAxis("Vertical2"));
    //        dir.Normalize();
    //        //objPicked.GetComponent<throwable>().setPos(targetReticle.transform.position);

    //        switch (playerNum)
    //        {
    //            case PlayerNum.P1:
    //                objPicked.GetComponent<throwable>().setLayer(1);
    //                break;
    //            case PlayerNum.P2:
    //                objPicked.GetComponent<throwable>().setLayer(1);
    //                break;
    //            default:
    //                break;
    //        }

    //        if (objPicked.GetComponent<CircleCollider2D>())
    //        {
    //            objPicked.GetComponent<CircleCollider2D>().enabled = true;
    //        }

    //        objPicked.GetComponent<throwable>().setDistance(distance);
    //        objPicked.GetComponent<Rigidbody2D>().AddForce(dir * throwForce);
            

    //        objPicked = null;
    //        isCarrying = false;
    //    }
    //}

    public void takeDamage(float amount, bool isLoud)
    {
        //stun(amount * 0.1f);
        hitPoints -= amount;
        
        //if(amount >0)   //just in case?
        //regenCounter = regenDelay;

        //if (hitPoints < 0)
        //{
        //    hitPoints = 0;
        //}

        playerHealth.updateHealthBar(hitPoints);

        if (hitPoints <= 0)
        {
            isSmelly = false;
            myManager.spawnPlayer(getPlayerNum() - 1);
            if (isLoud)
            {
                Instantiate(explosion, this.transform.position, explosion.transform.rotation);
            }

            Destroy(this.gameObject);
        }
    }


    public void setHealth(float value)
    {
        playerHealth.updateHealthBar(value);
    }

    public void getHealth(float amount)
    {
        //hitPoints += amount;

        //if (hitPoints > maximumHitPoints)
        //{
        //    hitPoints = maximumHitPoints;
        //}

        //playerHealth.updateHealthBar(hitPoints);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //takeDamage(collision.relativeVelocity.magnitude * collisionDamageMultiplier);

        }
        else if (collision.transform.GetComponent<destructible>())
        {
           // Debug.Log("destructible collided");
            collision.transform.GetComponent<destructible>().getDestroyed();
            takeDamage(collision.transform.GetComponent<destructible>().getDmgAmount(), true);
        }

    }

    //Called when the player hits an oil spill. Sets drag to 0 while storing original drag value.
    public void getOiled() // work here too
    {
        if(!isOiled)
        //if (GetComponent<Rigidbody2D>().drag != 0)
        {
            drag = GetComponent<Rigidbody2D>().drag;

        }
        GetComponent<Rigidbody2D>().drag = 0.5f;

        // DOESNT WORK THIS WAY> background speed fucks things up
        //Vector2 v = rBody.velocity;
        //v *= 10;
        //rBody.velocity.Set(v.x, v.y);

        oilSprite.SetActive(true);
        isOiled = true;
        oilCooldownTimer = oilCooldown; // Timer starts  at 15 seconds
        oilDirectionModifier = -1;
        //Debug.Log("got oiled!");
        //Debug.Log("Oil started timer at: " + oilCooldownTimer);
        //change sprites here to oily
        /*March 17, 2019: Oily artwork/Animations for the vehicles were not in the project or in the google drive.
         * So I used the smell animation just so I can debug. Please change this when the Oily animations are implemented.*/
        
    }
    //Called when the player leaves an oil spill. Sets drag to original value.
    public void getUnOiled() // get unoiled after 15 seconds
    {
        GetComponent<Rigidbody2D>().drag = drag;
        isOiled = false;

        oilSprite.SetActive(false);
       // myAnim.SetBool("isSmelly", false); // See above comments in getOiled()
        //change sprite back to normal

        //if (oilCount % 2 == 0)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed * oilForce / 2));
        //        //* (hitPoints / 100f)));
        //}
        //else
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed * oilForce / 2));
        //        //* (hitPoints / 100f)));
        //}
        //oilCount = 1;
        //oilForce = 1.0f;
        //oilDirectionModifier = 1;
        Debug.Log("got unoiled!");
    }

    public bool getIsOiled()//Getter Called in Explosion script to check if the player is oily
    {
        return isOiled;
    }


    public void stun(float addedStunTime)
    {
        m_stunTime += addedStunTime;
        //MoveBackScript.enabled = true;
    }


    void cooldownTimers()
    {
        if (hasJumped)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown)
            {
                hasJumped = false;
                jumpCooldownTimer = 0;
            }
        }

        if (hasEvaded)
        {
            evadeCooldownTimer += Time.deltaTime;
            if (evadeCooldownTimer >= evadeCooldown)
            {
                spriteHolder.GetComponent<SpriteRenderer>().color = Color.white;
                hasEvaded = false;
                evadeCooldownTimer = 0;
            }
        }

        if (hasThrown)
        {
            throwCooldownTimer += Time.deltaTime;
            if (throwCooldownTimer >= throwCooldown)
            {
                hasThrown = false;
                throwCooldownTimer = 0;
            }
        }

        if (isCarrying)
        {
            isDestroyedTimer += Time.deltaTime;
            if (isDestroyedTimer >= isDestroyedFreq)
            {
                resetCrane();
                isDestroyedTimer = 0;
            }
        }

        //if (isSmelly) // smelly countdown
        //{
        //    smellyCooldownTimer -= 1 * Time.deltaTime;
        //    //Debug.Log("Smelly Timer:" + smellyCooldownTimer);
        //    if (smellyCooldownTimer <= 0)
        //    {
        //        becomeUnSmelly();
        //        smellyCooldownTimer = 0;
        //    }
        //}

    }

    public bool amIoily()
    {
        return isOiled;
    }

    //reference script
    float getVelocity() {
        return m_rb.velocity.magnitude;
    }

    void checkLane() {
        if (transform.position.y <= referenceTop && transform.position.y > referenceTop1) {
            //    Debug.Log("I'm on 1st lane from the top");           
            currentLane = 1;

        }
        else if (transform.position.y <= referenceTop1 && transform.position.y > referenceTop2)
        {            
            currentLane = 2;
            //  Debug.Log("I'm on 2nd lane from the top");
        }
        else if (transform.position.y <= referenceTop2 && transform.position.y > referenceMid)
        {            
            currentLane = 3;
            //Debug.Log("I'm on 3th lane from the top");
        }     
        else if (transform.position.y <= referenceMid && transform.position.y > referenceBot1)
        {           
            currentLane = 4;
            //    Debug.Log("I'm on 4th lane from the top");
        }
        else if (transform.position.y <= referenceBot1 && transform.position.y > referenceBot2)
        {
            currentLane = 5;
            //  Debug.Log("I'm on 5th lane from the top");
        }
        else if (transform.position.y <= referenceBot2 && transform.position.y > referenceBot)
        {          
            currentLane = 6;
            //Debug.Log("I'm on 6th lane from the top");
        }

    }

    void pushToMid() {
        switch (currentLane)
        {
            case 1:
                Vector3 pointToPush1 = new Vector3(transform.position.x, midPointLane1, 0); //calculate the point to push towards to
                m_rb.AddForce((pointToPush1 - transform.position) * forceToCenter); //pushes on that direction with a force
                break;
            case 2:                
                Vector3 pointToPush2 = new Vector3(transform.position.x, midPointLane2, 0);
                m_rb.AddForce((pointToPush2 - transform.position) * forceToCenter);
                break;
            case 3:                
                Vector3 pointToPush3 = new Vector3(transform.position.x, midPointLane3, 0);
                m_rb.AddForce((pointToPush3 - transform.position) * forceToCenter);
               // Debug.Log("Trying to push towards: " + pointToPush3);
                break;
            case 4:                
                Vector3 pointToPush4 = new Vector3(transform.position.x, midPointLane4, 0);
                m_rb.AddForce((pointToPush4 - transform.position) * forceToCenter);                
                break;
            case 5:                
                Vector3 pointToPush5 = new Vector3(transform.position.x, midPointLane5, 0);
                m_rb.AddForce((pointToPush5 - transform.position) * forceToCenter);
                break;
            case 6:                
                Vector3 pointToPush6 = new Vector3(transform.position.x, midPointLane6, 0);
                m_rb.AddForce((pointToPush6 - transform.position) * forceToCenter);
                break;
            default:
                Debug.Log("Just give it some until update gives this 0 a value: " + currentLane);
                break;

        }
    }

    public void setCrown(bool setBool)
    {
        crownObj.SetActive(setBool);
    }

}
