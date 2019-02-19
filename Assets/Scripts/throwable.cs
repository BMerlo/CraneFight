using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwable : MonoBehaviour {
    //[SerializeField] Vector3 pos;
    [SerializeField] float stopDistance = 0.5f;
    bool isStopped = false;
    bool isThrown = false;
    [SerializeField] private bool[] isPlayerPickupAble = new bool[4];
    Vector3 lastFramePos;
    float distanceTravelled = 0;
    [SerializeField] float highlightSwapTimer = 1f; //How often the highlight will change
    private float highlightCounter;
    private int currentHighlight = 0;
    private Color[] playerColors = new Color[4]{Color.yellow,Color.green,Color.magenta,Color.blue};


	// Use this for initialization
	void Start () {
        lastFramePos = this.transform.position;
        //isPlayerPickupAble[3] = true;
        //isPlayerPickupAble[2] = true;
    }
	
	// Update is called once per frame
	void Update () {
        

        if (isStopped == false && isThrown)
        {
            distanceTravelled += Vector3.Distance(this.transform.position, lastFramePos);
            lastFramePos = this.transform.position;

            //if (distanceTravelled >= stopDistance / 2)
            //{
            //    this.gameObject.layer = 0;
            //}

            if (distanceTravelled >= stopDistance)
            {
                isStopped = true;
                this.gameObject.layer = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                isThrown = false;

                if (GetComponent<Exploder>())
                {
                    GetComponent<Exploder>().explode();
                }
            }

        }

        //highlightObject();    // Old Chris version of hilight
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
            case 3:
                this.gameObject.layer = 10;
                break;
            case 4:
                this.gameObject.layer = 11;
                break;
            default:
                break;
        }
        
    }

    //Sets boolean to true in array isPlayerPickupAble at index position playerNumber
    public void makePickupAble(int playerNumber)
    {
        isPlayerPickupAble[playerNumber] = true;
    }

    //Sets boolean to false in array isPlayerPickupAble at index position playerNumber
    public void makeNotPickupAble(int playerNumber)
    {
        isPlayerPickupAble[playerNumber] = false;
    }

    public bool canIPickup(int playerNumber)
    {
        return isPlayerPickupAble[playerNumber] = false;
    }

    //Highlights the object after a set time with a colour that is allowed to pick up the object
    private void highlightObject()
    {
        highlightCounter += Time.deltaTime;
        if(highlightCounter >= highlightSwapTimer)
        {
            if (currentHighlight == 3)
                currentHighlight = -1;
            while (!isPlayerPickupAble[currentHighlight+1])
            {
                if (currentHighlight == 2)
                    currentHighlight = -1;
                else currentHighlight++;
            }
            currentHighlight++;
            GetComponent<SpriteRenderer>().color = playerColors[currentHighlight];
            highlightCounter = 0;
        }
    }
}
