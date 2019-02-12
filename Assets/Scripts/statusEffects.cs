using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class statusEffects : MonoBehaviour
{

    [SerializeField] bool isSmelly;
    [SerializeField] GameObject[] cars;
    [SerializeField] Vector2 smellForceUp;
    [SerializeField] Vector2 smellForceDown;
    [SerializeField] float smellRadius;

    private int index = 0;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void becomeSmelly()
    {
        isSmelly = true;
    }

    void Update()
    {
        if (isSmelly)
        {
            //checks if AICars are in SmellRadius
            if (GetDistanceFromClosest(GameObject.FindGameObjectsWithTag("AICar")) <= smellRadius)
            {
                //Transforms if AICar position.x is behind the player 
                if (cars[index].transform.position.x > this.transform.position.x)
                {
                    //Transforms if AICar position.y is above player
                    if (cars[index].transform.position.y > this.transform.position.y)
                    {
                        cars[index].GetComponent<Transform>().Translate(smellForceUp);
                        Debug.Log("CAR NAME: " + cars[index].name + smellForceUp + "moving up: " + cars[index].transform.position);
                    }

                    //Transforms if AICar position.y is below player
                    else if (cars[index].transform.position.y < this.transform.position.y)
                    {
                        cars[index].GetComponent<Transform>().Translate(smellForceDown);
                        Debug.Log("CAR NAME: " + cars[index].name + smellForceDown + "moving down: " + cars[index].transform.position);
                    }
                }

                //Transforms if AICar position.x is infront of player
                else if (cars[index].transform.position.x < this.transform.position.x)
                {
                    //Transforms if AICar position.y is above player
                    if (cars[index].transform.position.y > this.transform.position.y)
                    {
                        cars[index].GetComponent<Transform>().Translate(smellForceUp);
                        Debug.Log("CAR NAME: " + cars[index].name + smellForceUp + "moving up: " + cars[index].transform.position);
                    }

                    //Transforms if AICar position.y is below player
                    else if (cars[index].transform.position.y < this.transform.position.y)
                    {
                        cars[index].GetComponent<Transform>().Translate(smellForceDown);
                        Debug.Log("CAR NAME: " + cars[index].name + smellForceDown + "moving down: " + cars[index].transform.position);
                    }
                }
            }
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SmellPowerup")//CHANGE this to the tag of the gameobject that triggers this.
        {
            becomeSmelly();
        }
    }

    //Detects whats in range of the vehicle
    float GetDistanceFromClosest(GameObject[] gameObjects)
    {
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject go in gameObjects)
        {
            shortestDistance = Mathf.Min(shortestDistance, Vector2.Distance(transform.position, go.transform.position));
            cars[index] = go;
        }
        return shortestDistance;
    }
}