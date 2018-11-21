using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour {
    Vector3 dir = new Vector3(1, 0, 0);
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float originalSpeed;
    float lowerSpeed;
    [SerializeField] float speedUsed;//serialized to check if it changed

    
    void Start () {
        originalSpeed = Random.Range(minSpeed, maxSpeed);
        speedUsed = originalSpeed;
        lowerSpeed = Random.Range(2, 5);
    }
		
	void Update () {
        //this.transform.Translate(dir * speedActual);
        //casts raycast to infinity
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        //debug the direction of the raycast
        Debug.DrawRay(transform.position, Vector2.right, Color.red);

        //check if raycasts collides, then decreases the speed
        if (hit.collider != null)
        {
            //Debug.Log("Distance to other obj: " + hit.distance);
            //Debug.Log("Name of other obj: " + hit.collider.name);
            if (hit.transform.tag == "Player" && (hit.distance <= 1.0f) && (hit.distance >= 0.0f))
            {
                //Debug.Log("Speed decreased");
                speedUsed = lowerSpeed;
            }
            else if ((hit.distance <= 3) && (hit.distance >= 2.0f))
            {
                //Debug.Log("Speed decreased");
                speedUsed = lowerSpeed;
            }
        }
        else
        {
            //Debug.Log("Speed increased");
            speedUsed = originalSpeed;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
    }
}
