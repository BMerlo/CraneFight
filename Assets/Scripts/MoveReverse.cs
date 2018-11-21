using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReverse : MonoBehaviour
{
    Vector3 dir = new Vector3(-1, 0, 0);
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float originalSpeed;
    float lowerSpeed;
    [SerializeField] float speedUsed;

    // Use this for initialization
    void Start()
    {
        originalSpeed = Random.Range(minSpeed, maxSpeed);
        speedUsed = originalSpeed;
        lowerSpeed = Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(dir * speedActual);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

        Debug.DrawRay(transform.position, Vector2.left, Color.blue);


        if (hit.collider != null)
        {
            Debug.Log("Distance to other obj: " + hit.distance);
            
            if (hit.transform.tag == "Player" && (hit.distance <= 1.0f) && (hit.distance >= 0.0f))
            {
                //Debug.Log("Speed decreased");
                //Debug.Log("Name of other obj: " + hit.collider.name);
                speedUsed = lowerSpeed;
            }
            else if ((hit.distance <= 3) && (hit.distance >= 2.0f))
            {
                //Debug.Log("Speed decreased");
                //Debug.Log("Name of other obj: " + hit.collider.name);
                speedUsed = lowerSpeed;
            }
        }
        else
        {
            Debug.Log("Speed increased");
            speedUsed = originalSpeed;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
    }
}