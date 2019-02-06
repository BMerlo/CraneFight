using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {
    const float speed = 0.05f;
    Vector3 dir = new Vector3(-1, 0, 0);    
    public bool reverse;
    
    void Start () {

        //to know which way to go
        if (transform.position.y < 0)
        {
            reverse = true;
        }
        else {
            reverse = false;
        }
    }
    
    void Update() {

        if (reverse) { 
        this.transform.Translate(dir * speed);
        }
        else { 
        this.transform.Translate(-dir * speed);
        }
        
        //if outside screen, destroy automatically
        if (transform.position.x < -10)
            Destroy(gameObject);
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "AICar") {
            Destroy(collision.gameObject);
        }
    }

}
