using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenFarPlayer : MonoBehaviour
{
    float killDistance = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, new Vector2(0,0)) > killDistance)
        {
            GetComponent<playerController>().takeDamage(1000);
        }
    }
}
