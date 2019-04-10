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
            if (pnum == 1 )
            {
            transform.position = new Vector2(fallback.returnbound(), fallback.p1object().transform.position.y);
                if (fallback.returnp1left()) {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 2 )
            {
               transform.position = new Vector2(fallback.returnbound(), fallback.p2object().transform.position.y);
                if (fallback.returnp2left())
                {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 3 )
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p3object().transform.position.y);
                if (fallback.returnp3left())
                {
                    Destroy(this.gameObject);
                }
            }
            if (pnum == 4)
            {
                transform.position = new Vector2(fallback.returnbound(), fallback.p4object().transform.position.y);
                if (fallback.returnp4left())
                {
                    Destroy(this.gameObject);
                }
            }
    }
    public void setnum(int num) {
        pnum = num;
    }
}
