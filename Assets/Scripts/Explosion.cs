using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField] float pushForce = 1f;
    [SerializeField] float damageMultiplier = 100f;
    [SerializeField] float isOilyDamge = 5f; ///

    Tentacle t;
    // Use this for initialization
    void Start () {
        //foreach (var item in find)
        //{

       // }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.name);

        if (collision.GetComponent<Rigidbody2D>() && !collision.GetComponent<oil>())
        {
            //Debug.Log(collision.gameObject.transform.position);

            Vector3 dir = collision.gameObject.transform.position - this.transform.position;
            dir.Normalize();
            //Debug.Log(dir);
            collision.GetComponent<Rigidbody2D>().AddForce(pushForce * dir);
            //   * (1f / Vector3.Distance(this.transform.position, collision.gameObject.transform.position)));

            if (collision.GetComponent<playerController>())
            {
                collision.GetComponent<playerController>().
                    takeDamage(damageMultiplier * (1f / Vector3.Distance(this.transform.position, collision.gameObject.transform.position)), false);

               // Debug.Log(collision.gameObject.name + "Health: " + collision.GetComponent<playerController>().getHitpoints());

                if (collision.GetComponent<playerController>().getIsOiled()) // extra damage if oily
                {
                    collision.GetComponent<playerController>().takeDamage(isOilyDamge, false);
                    Debug.Log(collision.gameObject.name+" has taken an extra: " + isOilyDamge);
                    Debug.Log(collision.gameObject.name +"Health: " + collision.GetComponent<playerController>().getHitpoints());

                }
            }
        }
        if (collision.GetComponent<oil>())
        {
            collision.GetComponent<oil>().burn();
        }
        //else if (collision.GetComponent<Tentacle>())
        //{
        //    Debug.Log("TENTACLE HIT!!!!!!!!!!!!!!!!!!!!!");
        //}
    }


    //void OnTriggerEnter2D(Collider2D collision)
    //{ 
    //    t = collision.gameObject.GetComponent<Tentacle>();
    //    Debug.Log(collision.gameObject + "--------------------------------");

    //    if (t != null)
    //    {
    //        Debug.Log(t + "is hit");
    //        t.getWiggle();
    //        t.getHit();
    //        t.retreat();

    //    }
    //}
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Tentacle hit!!!!!" + collision.GetComponent<Tentacle>());
    //    if (collision.GetComponent<Tentacle>())
    //    {
    //        collision.GetComponent<Tentacle>().retreat();
    //    }
    //}


}
