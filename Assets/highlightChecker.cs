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
            collision.GetComponent<SpriteOutline>().updatePlayerRange(myController.getPlayerNum(), true);
            collision.GetComponent<SpriteOutline>().Show();//show outline when in range
            collision.GetComponent<SpriteOutline>().addNumOfTrues();//add number of trues
        }
        else if (collision.transform.tag == "Carrier")
        {
            collision.GetComponent<SpriteOutline>().updatePlayerRange(myController.getPlayerNum(), true);
            collision.GetComponent<SpriteOutline>().Show();//show outline when in range
            collision.GetComponent<SpriteOutline>().addNumOfTrues();//add number of trues
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pickable")
        {
            collision.GetComponent<SpriteOutline>().updatePlayerRange(myController.getPlayerNum(), false);
            collision.GetComponent<SpriteOutline>().minusNumOfTrues();//minus number of trues
        }
        else if (collision.transform.tag == "Carrier")
        {
            collision.GetComponent<SpriteOutline>().updatePlayerRange(myController.getPlayerNum(), false);
            collision.GetComponent<SpriteOutline>().minusNumOfTrues();//minus number of trues
        }
    }
}
