using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArrowSpawn : MonoBehaviour
{
    public GameObject p1arrow;
    public GameObject p2arrow;
    public GameObject p3arrow;
    public GameObject p4arrow;
    public bool p1left;
    public bool p2left;
    public bool p3left;
    public bool p4left;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<playerController>())
        {
            switch (collision.GetComponent<playerController>().getPlayerNum())
            {
                case 1:
                    Instantiate(p1arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p1left = false;
                    break;
                case 2:
                    Instantiate(p2arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p2left = false;
                    break;
                case 3:
                    Instantiate(p3arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
                    p3left = false;
                    break;
                case 4:
                    Instantiate(p4arrow, new Vector2(0, collision.transform.position.y), collision.transform.rotation);
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
                    p1left = true;
                    break;
                case 2:
                    p2left = true;
                    break;
                case 3:
                    p3left = true;
                    break;
                case 4:
                    p4left = true;
                    break;
                default:
                    break;
            }

        }
    }
    public bool returnp1left() {
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
}
