using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public int myHealth;
    public int currentHealth;
    public GameObject healthBar;

	// Use this for initialization
	void Start ()
    {
        myHealth = 50;
        currentHealth = 50;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (myHealth != currentHealth)
        {
            healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            myHealth = currentHealth;
        }

    }
    
    public void SetHealthBar(int myHealth)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}

