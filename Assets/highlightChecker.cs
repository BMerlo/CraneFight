using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightChecker : MonoBehaviour
{
    playerController myController;
    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponentInParent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pickable")
        {
            collision.GetComponent<Outline>().updatePlayerRange(myController.getPlayerNum(), true);
            collision.GetComponent<Outline>().ShowOutline = true;//show outline when in range
        }
        else if (collision.transform.tag == "Carrier")
        {
            collision.GetComponent<Outline>().updatePlayerRange(myController.getPlayerNum(), true);
            collision.GetComponent<Outline>().ShowOutline = true;//show outline when in range
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pickable")
        {
            collision.GetComponent<Outline>().updatePlayerRange(myController.getPlayerNum(), false);
        }
        else if (collision.transform.tag == "Carrier")
        {
            collision.GetComponent<Outline>().updatePlayerRange(myController.getPlayerNum(), false);
        }
    }
}
