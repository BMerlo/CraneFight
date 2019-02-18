using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayersForTesting : MonoBehaviour
{
    //SCRIPT TO ADD TO PLAYERS SO THEY DIE WITH NO CONTRLLERS XD
    [SerializeField] float speedUsed;
    Vector3 dir = new Vector3(0, -1, 0);
    void Start()
    {
        speedUsed = 40;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(speedUsed * dir);
    }
}
