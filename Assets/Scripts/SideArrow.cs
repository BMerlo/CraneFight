using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int pnum;
    fallBackChecker fallback;
    Camera camera;
    float width;
    void Start()
    {
        fallback = FindObjectOfType<fallBackChecker>();
        camera = Camera.main;
        //float height = 0.95f * camera.orthographicSize;
        // width = height * camera.aspect;
    }
    // Update is called once per frame
    void Update()
    {
        if (pnum == 1)
        {
            if (fallback.p1object() != null)
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p1object().transform.position.y);
            }
            if (fallback.returnp1left() || fallback.p1object() == null)
            {
                Destroy(this.gameObject);
            }
        }
        if (pnum == 2)
        {
            if (fallback.p2object() != null)
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p2object().transform.position.y);
            }
            if (fallback.returnp2left() || fallback.p2object() == null)
            {
                Destroy(this.gameObject);
            }
        }
        if (pnum == 3)
        {
            if (fallback.p3object() != null)
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p3object().transform.position.y);
            }
            if (fallback.returnp3left() || fallback.p3object() == null)
            {
                Destroy(this.gameObject);
            }
        }
        if (pnum == 4)
        {
            if (fallback.p4object() != null)
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p4object().transform.position.y);
            }
            if (fallback.returnp4left() || fallback.p4object() == null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void setnum(int num) {
        pnum = num;
    }
}
