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
            manager.spawnGhost(GetComponent<playerController>().getPlayerNum() - 1);

            if ((GetComponent<playerController>().getPlayerNum() - 1) == 0)
            {
                Debug.Log("player 1 dead");
                manager.player1alive = false;
                //manager.ghostToSpawn = 0;
                Destroy(this.gameObject, 1.0f);
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 1)
            {
                Debug.Log("player 2 dead");
                manager.player2alive = false;
                //manager.ghostToSpawn = 1;
                Destroy(this.gameObject, 1.0f);
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 2)
            {
                Debug.Log("player 3 dead");
                manager.player3alive = false;
                //manager.ghostToSpawn = 2;
                Destroy(this.gameObject, 1.0f);
            }
            else if ((GetComponent<playerController>().getPlayerNum() - 1) == 3)
            {
                Debug.Log("player 4 dead");
                manager.player4alive = false;
                //manager.ghostToSpawn = 3;
                Destroy(this.gameObject, 1.0f);
            }

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
