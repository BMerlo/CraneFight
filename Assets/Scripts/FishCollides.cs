using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollides : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("-*-*-*-*-*-* FISH COLLIDING WITH " + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
          // Debug.Log("--**-*-*-*-*make this smelly " + col.gameObject.name);
            col.gameObject.GetComponent<playerController>().becomeSmelly();
            //Destroy(col.gameObject);
        }
    }
}
