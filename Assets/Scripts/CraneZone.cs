using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneZone : MonoBehaviour {
    Collider2D myCollider;
    [SerializeField] GameObject obj2pick;
    [SerializeField] GameObject carrier2pickUpFrom;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pickable")
        {
            obj2pick = collision.gameObject;
            obj2pick.GetComponent<Outline>().ShowOutline = true;//show outline when in range
        }
        else if (collision.transform.tag == "Carrier")
        {
            carrier2pickUpFrom = collision.gameObject;
            //carrier2pickUpFrom.GetComponent<Outline>().ShowOutline = true;//show outline when in range
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (obj2pick == collision.gameObject)
        {
            obj2pick.GetComponent<Outline>().ShowOutline = false;//hide the outline when pick up when pick up when not in range
            obj2pick = null;

        }
        else if (carrier2pickUpFrom == collision.gameObject)
        {
            // carrier2pickUpFrom.GetComponent<Outline>().ShowOutline = false;//hide the outline when pick up when not in range
            carrier2pickUpFrom = null;
        }
    }

    public bool isTherePickable()
    {
        if (obj2pick)
        {
            obj2pick.GetComponent<Outline>().ShowOutline = false;//hide the outline when pick up
            return true;
        }
        if (carrier2pickUpFrom)
        {
            //carrier2pickUpFrom.GetComponent<Outline>().ShowOutline = false;//hide the outline when pick up
            return true;
        }
        return false;
    }

    public GameObject getObj2PickUp()
    {
        Debug.Log("getObj2PickUp()");
        if (obj2pick)
        {
            Debug.Log("if (obj2pick)()");
            return obj2pick;
        }
        else if (carrier2pickUpFrom)
        {   // Instantiate obj here
            Debug.Log("else if (carrier2pickUpFrom)");
            return carrier2pickUpFrom.GetComponent<Carrier>().getPickUpable();    // Infinite oil
        }
        return null;
    }
}
