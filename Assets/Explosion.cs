using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField] float pushForce = 1f;
    [SerializeField] float damageMultiplier = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Vector3 dir = collision.transform.position - this.transform.position;
            dir.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(pushForce * dir * 
                (1f / Vector3.Distance(this.transform.position, collision.transform.position)));

            if (collision.GetComponent<playerController>())
            {
                collision.GetComponent<playerController>().
                    takeDamage(damageMultiplier* (1f / Vector3.Distance(this.transform.position, collision.transform.position)));
            }

        }
    }
}
