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
    bool isHit = false;
    bool isWiggle = false;
    float retractSpeed = 0.1f;

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
               // retreat();//TEMPORARY SPOT TO CHECK FOR ANIMATIIONSz
            }
        }

        if(isHit)
        {
            Debug.Log("Tentacle retracting away-------");
            transform.GetChild(0).gameObject.SetActive(false);//sets the shadow to false so we don't see it // just for debugging
            GetComponent<PolygonCollider2D>().enabled = false;//disables the straight polygon collider
            this.transform.Translate(0, retractSpeed, 0);
            //tentacleAnim.SetBool("hitWithExplosive", isHit); //retreating animation
        }

        if(isWiggle)
        {
            tentacleAnim.SetBool("writhing", isWiggle); //wiggle animation 
            transform.GetChild(0).gameObject.SetActive(false);//sets the shadow to false so we don't see it // just for debugging
            GetComponent<PolygonCollider2D>().enabled = false;//disables the straight polygon collider
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

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("EXPLOSION HIT TENTACLE" + isHit);
    //    Explosion e = collision.GetComponent<Explosion>();
    //    Debug.Log(e.name + "is hit@@");
    //    if (e != null)
    //    {
    //        retreat();

    //    }
    //    Debug.Log("EXPLOSION HIT TENTACLE2222" + isHit);
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        oil oi = FindObjectOfType<oil>();
        if (oi != null)
        {
            Debug.Log("Tentacle Hit by" + oi);
            isWiggling();
        }

        Explosion ex = FindObjectOfType<Explosion>();
        if (ex != null)
        {
            Debug.Log("Tentacle Hit by," + ex);
            retreat();
        }
        Debug.Log("hit by " + ex);
        Debug.Log("hit by " + oi);


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<destructible>())
        {
            collision.GetComponent<destructible>().getDestroyed();
        }
        else if (collision.GetComponent<playerController>())
        {
         //   Debug.Log("TENTACLE TRIGGERED!!!~~~~~~~~~~~~");

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

    public void retreat()
    {
        isHit = true;
        Debug.Log("TENTACLEHIT");
    }

    public void isWiggling()
    {
        isWiggle = true;
    }

    public bool getHit()
    {
        return isHit;
    }

    public bool getWiggle()
    {
        return isWiggle;
    }
}
