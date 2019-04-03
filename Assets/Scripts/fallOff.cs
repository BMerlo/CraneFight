using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallOff : MonoBehaviour {
    [SerializeField] float maxY = 3.51f;
    [SerializeField] float minY = -3.91f;

    Game_Manager manager;
    bool isFalling = false;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<Game_Manager>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(this.transform.position.x);
        if (!isFalling)
        {
            if (this.transform.position.y > maxY)
            {
                manager.needsGhost = true;
                fallFromTop();
            }
            else if (this.transform.position.y < minY)
            {
                fall();
            }
        }
        
        
                
	}

    void fallFromTop()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -3;
        GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        fall();
    }

    void fall()
    {
        isFalling = true;

        GetComponent<arrangeLayers>().enabled = false;
        this.gameObject.layer = 16;


        if (GetComponent<BoxCollider2D>())
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (GetComponent<CircleCollider2D>())
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
        }

        if (GetComponent<playerController>())
        {
            switch (GetComponent<playerController>().getPlayerNum() - 1) {
                case 0:
                    manager.player1alive = false;
                    break;
                case 1:
                    manager.player2alive = false;
                    break;
                case 2:
                    manager.player3alive = false;
                    break;
                case 3:
                    manager.player4alive = false;
                    break;
            }
            
            Destroy(this.gameObject, 1.0f);

            GetComponent<playerController>().setHealth(0);
            GetComponent<playerController>().enabled = false;
        }
        

        //this.enabled = false;
        
    }


    //IEnumerator playerFall()
    //{
    //    Debug.Log("playerfall");
    //    yield return new WaitForSeconds(0.9f);
    //    Debug.Log("playerfall2");
    //    GetComponent<playerController>().takeDamage(100);

    //}

    
}
