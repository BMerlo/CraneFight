using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour
{
    Rigidbody2D fishRigidbody;
    Animator fishAnimation;//animation
    float minForce = 40.0f;
    float maxForce = 140.0f;
    float randomForce;
    bool Jumping = false;// if it does not stop yet it should go straight up 

    // Start is called before the first frame update
    void Start()
    {
        randomForce = Random.Range(minForce, maxForce);
        fishAnimation = this.GetComponent<Animator>();
        fishRigidbody = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("speed  " + fishRigidbody.velocity.magnitude);
        FishUP();

      
    }

    void FishUP()
    {
        if (!Jumping)
        {
            fishRigidbody.AddForce(Vector2.up* randomForce);//add force to push upwards
           
        }
        if (fishRigidbody.velocity.magnitude < 0.3&&!Jumping)// if the speed is less than 0.3 then it should stop
        {
            Jumping = true;
            fishAnimation.SetBool("Landed", Jumping);//when landed play turn and wiggle animation 
            fishRigidbody.velocity = new Vector2(0, 0);//set velocity to 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Something is smelly");
            collision.GetComponent<playerController>().becomeSmelly();
        }
        }
}
