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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isP1here)
        {
            counter1 += Time.deltaTime;            
            if (counter1 >= timeToLose)
            {
                p1.GetComponent<playerController>().takeDamage(100);
                p1.GetComponent<fallOff>().fall();
                //Destroy(p1);
            }
        }

        if (isP2here)
        {
            counter2 += Time.deltaTime;
            if (counter2 >= timeToLose)
            {
                p2.GetComponent<playerController>().takeDamage(100);
                p2.GetComponent<fallOff>().fall();
                //Destroy(p2);
            }
        }

        if (isP3here)
        {
            counter3 += Time.deltaTime;
            if (counter3 >= timeToLose)
            {
                p3.GetComponent<playerController>().takeDamage(100);
                p3.GetComponent<fallOff>().fall();
                //Destroy(p3);
            }
        }

        if (isP4here)
        {
            counter4 += Time.deltaTime;
            if (counter4 >= timeToLose)
            {
                p4.GetComponent<playerController>().takeDamage(100);
                p4.GetComponent<fallOff>().fall();
                //Destroy(p4);
            }
        }


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
                    break;
                case 2:
                    isP2here = true;
                    p2 = collision.gameObject;
                    break;
                case 3:
                    isP3here = true;
                    p3 = collision.gameObject;
                    break;
                case 4:
                    isP4here = true;
                    p4 = collision.gameObject;
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
                    break;
                case 2:
                    isP2here = false;
                    counter2 = 0;
                    break;
                case 3:
                    isP3here = false;
                    counter3 = 0;
                    break;
                case 4:
                    isP4here = false;
                    counter4 = 0;
                    break;
                default:
                    break;
            }

        }
    }
}
