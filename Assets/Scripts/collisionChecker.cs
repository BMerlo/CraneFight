using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionChecker : MonoBehaviour
{
    float collisionDmg = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Front collision");
        if (collision.transform.GetComponent<playerController>())
        {
            collision.transform.GetComponent<playerController>().takeDamage(collisionDmg);
        }


        //Vector2 contactPoint = collision.contacts[0].point;
        //Vector2 centre = collision.collider.bounds.center;

        
    }
}
