using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour
{
    enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }

    [SerializeField] PlayerNum playerNum;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] bool isPossesing;
    [SerializeField] bool wantToPossess;
    [SerializeField] bool krakenPossessed;    
    [SerializeField] GameObject objectToPossess;

    private float moveRightBust;
    private float moveLeftBust;
    private float m_timeElapsed;
    
    void Start()
    {
        isPossesing = false;
        wantToPossess = false;
        krakenPossessed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           // Debug.Log("Want to possess A");            
            wantToPossess = true;
        }

        if (Input.GetButtonDown("Fire1") && isPossesing)
        {
            if (krakenPossessed) {
                objectToPossess.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //so the kraken velocity is zero when unpossessed;
                objectToPossess.GetComponent<Kraken>().enabled = true;
                objectToPossess.GetComponent<Kraken>().setInitialPosition();
            }

            wantToPossess = false;
            isPossesing = false;
            krakenPossessed = false;
            objectToPossess = null;
        }

        if (Input.GetButtonUp("Fire1") && !isPossesing)
        {
            wantToPossess = false;
            isPossesing = false;
        }

        m_timeElapsed += Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && isPossesing && krakenPossessed) //when possesing Kraken, B button spawns a tentacle
        {
            objectToPossess.GetComponent<Kraken>().spawnTentacleInFront();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 19 && wantToPossess)
        {
            // Debug.Log("I can try to possess" + collision.gameObject.name);
            isPossesing = true;
            objectToPossess = collision.gameObject;

            if (collision.gameObject.GetComponent<MoveReverse>() != null)
            {
                moveLeftBust = -30;
                moveRightBust = 0;
                Debug.Log("BUST TO LEFT");
            }

            if (collision.gameObject.GetComponent<moveForward>() != null)
            {
                moveLeftBust = 0;
                moveRightBust = 30;
                Debug.Log("BUST TO RIGHT");
            }

            if (collision.gameObject.GetComponent<Kraken>() != null)
            {
                collision.gameObject.GetComponent<Kraken>().enabled = false;
                krakenPossessed = true;
            }

            if (collision.gameObject.name == "YellowCarRubble(Clone)") //I didn't do this, I don't get it but I'll leave it as is
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                wantToPossess = !wantToPossess;
            }

            collision.gameObject.transform.position = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 19 && wantToPossess)
        {
            wantToPossess = false;
            krakenPossessed = false;
        }
    }
    
    private void FixedUpdate()
    {
        if (!krakenPossessed) { //to move different when kraken is possessed
            movement();
        }
        else {
            movementKraken();
        }        
    }

    float getOwnAxis(string axis)
    {
        return Input.GetAxis(playerNum.ToString() + axis);
    }

    void movement()
    {        
        if (getOwnAxis("Horizontal") > 0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed+ moveRightBust, 0));
        }
        else if (getOwnAxis("Horizontal") < -0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed+moveLeftBust, 0));
        }

        if (getOwnAxis("Vertical") > 0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed));
        }
        else if (getOwnAxis("Vertical") < -0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed));
        }
    }

    void movementKraken()
    {
        //so player can only move between these 2 points.
        if (gameObject.transform.position.x > objectToPossess.GetComponent<Kraken>().getRandomLx() && gameObject.transform.position.x < objectToPossess.GetComponent<Kraken>().getRandomRx()) 
        {
           // Debug.Log("I'm HEEERE!");
            if (getOwnAxis("Horizontal") > 0.3f)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed + moveRightBust, 0));
            }
            else if (getOwnAxis("Horizontal") < -0.3f)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed + moveLeftBust, 0));
            }
        }
        else {
            //Debug.Log("I'm not HEEERE!");
            //Debug.Log("LEFT side: " + objectToPossess.GetComponent<Kraken>().getRandomLx());         
            //Debug.Log("RIGHT side: " + objectToPossess.GetComponent<Kraken>().getRandomRx());

            if (gameObject.transform.position.x < objectToPossess.GetComponent<Kraken>().getRandomLx()) { //to constrict player's movement side to side
               GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed + moveRightBust, 0));
            }
            else if (gameObject.transform.position.x > objectToPossess.GetComponent<Kraken>().getRandomRx())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed + moveLeftBust, 0));
            }
        }
    }


    public void setPlayerNum(int i)
    {
        playerNum = (PlayerNum)i;   
    }
}
