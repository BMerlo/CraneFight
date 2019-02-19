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
    [SerializeField] float timeToDestroy;
    [SerializeField] GameObject objectToPossess;

    private float moveRightBust;
    private float moveLeftBust;
    private float m_timeElapsed;
    //float timeToDestroy;

    // Use this for initialization
    void Start()
    {
        isPossesing = false;
        wantToPossess = false;
        timeToDestroy = 5.0f;
        Destroy(gameObject, timeToDestroy);       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Want to possess A");
            
            wantToPossess = !wantToPossess;
        }
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    wantToPossess = false;
        //    isPossesing = false;
        //    objectToPossess = null;
        //}

        m_timeElapsed += Time.deltaTime;

        if (m_timeElapsed > timeToDestroy) {
            Destroy(gameObject);
        }

        if (isPossesing)
        {
       //     objectToPossess = tra
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0 && wantToPossess )
        {
           // Debug.Log("I can try to possess" + collision.name);
            
            if(collision.gameObject.GetComponent<MoveReverse>() != null)
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

            if(collision.gameObject.name == "YellowCarRubble(Clone)")
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                wantToPossess = !wantToPossess;
            }

            collision.gameObject.transform.position = transform.position;            
            
            //isPossesing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0 && wantToPossess)
        {
            wantToPossess = false;
        }
    }





    private void FixedUpdate()
    {
        movement();
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed));
        }
        else if (getOwnAxis("Vertical") < -0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed));
        }
    }
    

    public void setPlayerNum(int i)
    {
        playerNum = (PlayerNum)i;
        Color color = Color.white;
        switch (i)
        {
            case 0:
                color = Color.green;
                break;
            case 1:
                color = Color.blue;
                break;
            case 2:
                color = Color.red;
                break;
            case 3:
                color = Color.black;
                break;
            default:
                break;
        }

        GetComponent<SpriteRenderer>().color = color;

    }
}
