using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReverse : MonoBehaviour
{
    Vector3 dir = new Vector3(-1, 0, 0);
    [SerializeField] float minSpeed = 0.05f;
    [SerializeField] float maxSpeed = 0.1f;
    float speedActual;

    // Use this for initialization
    void Start()
    {
        speedActual = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(dir * speedActual);

    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(speedActual * dir);
    }
}