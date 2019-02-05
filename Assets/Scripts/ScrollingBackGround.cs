using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private float speed = -3f;
    private SpriteRenderer render;
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(speed, 0);
        render = GetComponent<SpriteRenderer>();
        length = render.size.x;
}

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < -length)
            Reposition();
    }
    private void Reposition()
    {
        Vector2 ground = new Vector2(length*3, 0);
        this.transform.position = (Vector2)transform.position + ground;
    }
}
 
