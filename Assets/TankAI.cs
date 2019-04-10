using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
     ScrollingBackGround backGroundScript;
     float backgroundSpeed;

    float speed = -1.0f;
    Vector2 speedVector;

    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        backGroundScript = FindObjectOfType<ScrollingBackGround>();
        backgroundSpeed = backGroundScript.getSpeed();

        speedVector = new Vector2(backgroundSpeed + speed, 0);

        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = speedVector;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<destructible>())
        {
            collision.transform.GetComponent<destructible>().getDestroyed();
        }
    }
}
