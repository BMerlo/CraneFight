using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {
    const float speed = 0.05f;
    Vector3 dir = new Vector3(-1, 0, 0);    
    public bool reverse;

    float attackTime = 1.0f;
    float counter = 0;
    bool hasAttacked = false;
    bool hasPushed = false;
    float pushForce = 700;
    float pushWait = 0.3f;

    [SerializeField] Animator tentacleAnim;


    TentaclePusher[] pushers;
    
    void Start () {
        pushers = GetComponentsInChildren<TentaclePusher>();

        tentacleAnim.SetTrigger("tentacleTrigger");

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
        if (!hasPushed)
        {
            counter += Time.deltaTime;

            if (counter >= attackTime)
            {
                counter = 0;
                Debug.Log("tentacle attack!!1!");
                //tentacleAnim.SetTrigger("tentacleTrigger");
                hasPushed = true;
                //foreach (TentaclePusher pusher in pushers)
                //{
                //    pusher.enabled = true;
                //}
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
        else if (!hasAttacked)
        {
            counter += Time.deltaTime;

            if (counter >= pushWait)
            {
                GetComponent<PolygonCollider2D>().isTrigger = false;
                hasAttacked = true;
            }

        }




        if (reverse) { 
        this.transform.Translate(-dir * speed);
        }
        else { 
        this.transform.Translate(dir * speed);
        }
        
        //if outside screen, destroy automatically
        if (transform.position.x < -10)
            Destroy(gameObject);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<destructible>())
        {
            collision.GetComponent<destructible>().getDestroyed();
        }
        else if (collision.GetComponent<playerController>())
        {
            Debug.Log("TENTACLE TRIGGERED!!!~~~~~~~~~~~~");

            if (collision.transform.position.x >= transform.position.x)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(transform.right * pushForce);
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().AddForce(-transform.right * pushForce);
            }

        }
        else if (collision.GetComponent<carAI>())
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.tag == "AICar")
        if (collision.transform.GetComponent<carAI>())
        {
            Destroy(collision.gameObject);
        }
    }

}
