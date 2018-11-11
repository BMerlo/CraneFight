using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPhysics : MonoBehaviour {

    [SerializeField] private float          m_initalForce = 200.0f;
    [SerializeField] private float          m_minInitalForce = 100.0f;
    [SerializeField] private float          m_maxInitalForce = 330.0f;
    [SerializeField] private float          m_lifeTime = 10.0f;
                     private Rigidbody2D    m_rb;
 
        

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.gravityScale = 0;
        m_initalForce = Random.Range(m_minInitalForce, m_maxInitalForce);
        m_rb.AddForce(transform.up * -m_initalForce, ForceMode2D.Impulse);
        Destroy(this.gameObject, m_lifeTime);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        
    }


}
