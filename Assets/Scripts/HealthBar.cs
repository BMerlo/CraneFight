using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private float myHealth;
    //public float currentHealth;
    [SerializeField] GameObject healthBar;

	// Use this for initialization
	void Start ()
    {
        myHealth = 100;
        //currentHealth = 100;
	}
	public void ChangeHealth(float damage)
    {

        myHealth -= damage;

        if (myHealth < 0)
            myHealth = 0;

        if (myHealth > 100)
            myHealth = 100;

        healthBar.transform.localScale = new Vector3(myHealth / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

    }

    public void updateHealthBar(float hp)
    {
        healthBar.transform.localScale = new Vector3(hp / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

  
}

