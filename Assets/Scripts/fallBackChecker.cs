using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallBackChecker : MonoBehaviour {
    bool isP1here = false;
    bool isP2here = false;
    bool isP3here = false;
    bool isP4here = false;

    [SerializeField] float timeToLose = 2.0f;
    float counter1 = 0;
    float counter2 = 0;
    float counter3 = 0;
    float counter4 = 0;

    GameObject p1, p2, p3, p4;

    public GameObject p1arrow;
    public GameObject p2arrow;
    public GameObject p3arrow;
    public GameObject p4arrow;


     bool p1left;
     bool p2left;
     bool p3left;
     bool p4left;

    float bound;


    // Use this for initialization
    void Start () {
        Collider2D collider2D;
        collider2D = gameObject.GetComponent<Collider2D>();
        bound = collider2D.bounds.max.x+1;
    }
	
	// Update is called once per frame
	void Update () {
        if (isP1here)
        {
            counter1 += Time.deltaTime;
            if (counter1 >= timeToLose&&p1!=null)
            {
                p1.GetComponent<playerController>().takeDamage(100);
                Destroy(p1);
                p1left = true;
            }
        }

        if (isP2here)
        {
            counter2 += Time.deltaTime;
            if (counter2 >= timeToLose && p2 != null)
            {
                p2.GetComponent<playerController>().takeDamage(100);
                Destroy(p2);
                p2left = true;
            }
        }

        if (isP3here)
        {
            counter3 += Time.deltaTime;
            if (counter3 >= timeToLose && p3 != null)
            {
                p3.GetComponent<playerController>().takeDamage(100);
                Destroy(p3);
                p3left = true;
            }
        }

        if (isP4here)
        {
            counter4 += Time.deltaTime;
            if (counter4 >= timeToLose && p4 != null)
            {
                p4.GetComponent<playerController>().takeDamage(100);
                Destroy(p4);
                p4left = true;
            }
        }
        //p1arrow.GetComponent<SideArrow>().follow(p1);
        //p2arrow.GetComponent<SideArrow>().follow(p2);
        //p3arrow.GetComponent<SideArrow>().follow(p3);
        //p4arrow.GetComponent<SideArrow>().follow(p4);
       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<playerController>())
        {
            switch (collision.GetComponent<playerController>().getPlayerNum())
            {
                case 1:
                    isP1here = true;
                    p1 = collision.gameObject;

                    Instantiate(p1arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p1arrow.GetComponent<SideArrow>().setnum(1);
                    p1left = false;
                    break;
                case 2:
                    isP2here = true;
                    p2 = collision.gameObject;

                    Instantiate(p2arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p2arrow.GetComponent<SideArrow>().setnum(2);
                    p2left = false;
                    break;
                case 3:
                    isP3here = true;
                    p3 = collision.gameObject;

                    Instantiate(p3arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p3arrow.GetComponent<SideArrow>().setnum(3);
                    p3left = false;
                    break;
                case 4:
                    isP4here = true;
                    p4 = collision.gameObject;

                    Instantiate(p4arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p4arrow.GetComponent<SideArrow>().setnum(4);
                    p4left = false;
                    break;
                default:
                    break;
            }
            
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<playerController>())
        {
            switch (collision.GetComponent<playerController>().getPlayerNum())
            {
                case 1:
                    isP1here = false;
                    counter1 = 0;
                    p1left = true;
                    break;
                case 2:
                    isP2here = false;
                    counter2 = 0;
                    p2left = true;
                    break;
                case 3:
                    isP3here = false;
                    counter3 = 0;
                    p3left = true;
                    break;
                case 4:
                    isP4here = false;
                    counter4 = 0;
                    p4left = true;
                    break;
                default:
                    break;
            }

        }
    }
    public bool returnp1left()
    {
        return p1left;
    }
    public bool returnp2left()
    {
        return p2left;
    }
    public bool returnp3left()
    {
        return p3left;
    }
    public bool returnp4left()
    {
        return p4left;
    }

    public GameObject p1object() {
        return p1;
    }
    public GameObject p2object()
    {
        return p2;
    }
    public GameObject p3object()
    {
        return p3;
    }
    public GameObject p4object()
    {
        return p4;
    }
    public float returnbound() {
        return bound;
    }

}
