using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallOff : MonoBehaviour {
    [SerializeField] float maxY = 3.51f;
    [SerializeField] float minY = -3.91f;

    Game_Manager manager;    

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<Game_Manager>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(this.transform.position.x);
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

    void fallFromTop()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -3;
        GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        fall();
    }

    public void fall()
    {
        GetComponent<arrangeLayers>().enabled = false;
        this.gameObject.layer = 16;

        Debug.Log("Falling!");        
        if (GetComponent<playerController>())
        {
            GetComponent<playerController>().takeDamage(100);
            GetComponent<playerController>().enabled = false;
        }
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
            manager.spawnGhost(GetComponent<playerController>().getPlayerNum() - 1);

            if ((GetComponent<playerController>().getPlayerNum() - 1) == 0)
            {
                Debug.Log("player 1 dead");
                manager.player1alive = false;
                //manager.ghostToSpawn = 0;
                Destroy(this.gameObject, 1.0f);
                manager.playersCurrentlyAlive--;
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 1)
            {
                Debug.Log("player 2 dead");
                manager.player2alive = false;
                //manager.ghostToSpawn = 1;
                Destroy(this.gameObject, 1.0f);
                manager.playersCurrentlyAlive--;
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 2)
            {
                Debug.Log("player 3 dead");
                manager.player3alive = false;
                //manager.ghostToSpawn = 2;
                Destroy(this.gameObject, 1.0f);
                manager.playersCurrentlyAlive--;
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 3)
            {
                Debug.Log("player 4 dead");
                manager.player4alive = false;
                //manager.ghostToSpawn = 3;
                Destroy(this.gameObject, 1.0f);
                manager.playersCurrentlyAlive--;
            }
        }        

        this.enabled = false;
        
    }
}
