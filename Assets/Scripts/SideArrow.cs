using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int pnum;
    SideArrowSpawn sidearrow;
    void Start()
    {
         sidearrow = FindObjectOfType<SideArrowSpawn>();

    }
    // Update is called once per frame
    void Update()
    {
        playerController[] cars = GameObject.FindObjectsOfType<playerController>();
        foreach (playerController car in cars)
        {

            if (pnum == 1 && car.getPlayerNum() == 1)
            {
                transform.position = new Vector2(-8, car.transform.position.y);
                if (sidearrow.returnp1left()) {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 2 && car.getPlayerNum() == 2)
            {
               transform.position = new Vector2(-8, car.transform.position.y);
                if (sidearrow.returnp2left())
                {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 3 && car.getPlayerNum() == 3)
            {
                transform.position = new Vector2(-8, car.transform.position.y);
                if (sidearrow.returnp3left())
                {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 4 && car.getPlayerNum() == 4)
            {
                transform.position = new Vector2(-8, car.transform.position.y);
                if (sidearrow.returnp4left())
                {
                    Destroy(this.gameObject);
                }
            }
               
        }
    }
}
