using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {
    [SerializeField] float bossEventTime = 10f;
    float bossEventCounter = 0;
    [SerializeField] Transform tentacleSpawnPos;
    [SerializeField] GameObject tentaclePrefab;
	[SerializeField] GameObject krakenHead;//Takes in existing krakenhead in the scene. If we want to make it the prefab then we have to instantiate it in the script.
	[SerializeField] float kUpSpeed = 0;//Up movement speed for kraken head
	//[SerializeField] float delayKSpawner = 0;//Delay time between tentacle spawn and kraken down movement
	[SerializeField] float kDownSpeed = 0;//Down movement speed for kraken head
	[SerializeField] float delayK = 0;//Delay between tentacles and spawning

	[SerializeField] Vector2 KrakenStartPos;
	[SerializeField] Vector2 KrakenEndPos;


    [SerializeField] GameObject tankWarningPrefab;
    [SerializeField] GameObject tankPrefab;

    [SerializeField] Transform[] warningPos;
    [SerializeField] Transform[] tankPos;

	[SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;

    [SerializeField] GameObject winPanel;
    //[SerializeField] GameObject tiePanel;

    [SerializeField] GameObject[] numberOfPlayers;

    //public Transform player1Location;
    //public Transform player2Location;
    //public Transform player3Location;
    //public Transform player4Location;

    public int playersCurrentlyAlive;

    [SerializeField] Vector3 locationToSpawn1;
    [SerializeField] Vector3 locationToSpawn2;
    [SerializeField] Vector3 locationToSpawn3;
    [SerializeField] Vector3 locationToSpawn4;

    public bool player1alive;
    public bool player2alive;
    public bool player3alive;
    public bool player4alive;

    public int ghostToSpawn;
    public int lastGhostToSpawn;

    public bool needsGhost;

    private float m_timeToSpawn1;
    private float m_timeToSpawn2;
    private float m_timeToSpawn3;
    private float m_timeToSpawn4;

    private float timeToRespawn = 3;
    
    
    private float m_timeElapsed;

    [SerializeField] GameObject ghostPrefab1;
    [SerializeField] GameObject ghostPrefab2;
    [SerializeField] GameObject ghostPrefab3;
    [SerializeField] GameObject ghostPrefab4;

    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] GameObject player3Prefab;
    [SerializeField] GameObject player4Prefab;
    
    //private int player1lives = 2;
    //private int player2lives = 2;
    //private int player3lives = 2;
    //private int player4lives = 2;
    [SerializeField] HealthBar playerHealth1;
    [SerializeField] HealthBar playerHealth2;
    [SerializeField] HealthBar playerHealth3;
    [SerializeField] HealthBar playerHealth4;
	
	bool player1Moved;
    float player1MovedTimer;
    bool player2Moved;
    float player2MovedTimer;
    bool player3Moved;
    float player3MovedTimer;
    bool player4Moved;
    float player4MovedTimer;
    float timeToBeActive = 2;

    private Text winnerText;
    ScoreManager score; //get score script
    //float timer = 0;
    public enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        //locationToSpawn = new Vector3(0,0,0);
        //player2Location;
        //player3Location;
        //player4Location;

        winnerText = winPanel.GetComponentInChildren<Text>();
        score = GetComponent<ScoreManager>();
        winnerText.text = "Winner is..."; //added for debugging purposes 

        player1alive = true;
        player2alive = true;
        player3alive = true;
        player4alive = true;

        locationToSpawn1 = new Vector3(-12.5f, 0.19f, 0);
        locationToSpawn2 = new Vector3(-12.5f, -2.19f, 0);
        locationToSpawn3 = new Vector3(-12.5f, 1.45f, 0);
        locationToSpawn4 = new Vector3(-12.5f, -1.01f, 0);

        needsGhost = false;

        playersCurrentlyAlive = 4;

        //lastGhostToSpawn = -1;
        //ghostToSpawn = -1;

        m_timeElapsed = 0;

        //numberOfPlayers = player1, player2, player3, player4;
		
		player1MovedTimer = timeToBeActive;
        player2MovedTimer = timeToBeActive;
        player3MovedTimer = timeToBeActive;
        player4MovedTimer = timeToBeActive;
    }

    // Update is called once per frame
    void Update()
    {
        bossEventCounter += Time.deltaTime;

        if (bossEventCounter >= bossEventTime)
        {
            bossEventCounter = 0;

            if (Random.Range(0, 2) == 0)
            {
                StartCoroutine(waitForKrakenHead(kUpSpeed, kDownSpeed, delayK));
            }
            else
            {
                tankEvent();
            }
			
        }

        //if (numberOfPlayers.Length == 1)
        //      {
        //          Time.timeScale = 0;
        //          winPanel.SetActive(true);
        //      }
        //      else if (numberOfPlayers.Length == 0)
        //      {
        //          Time.timeScale = 0;
        //          tiePanel.SetActive(true);
        //      }

        //m_timeElapsed += Time.deltaTime;

        if (playersCurrentlyAlive == 1)
        {
            //timer++;
            Invoke("someoneWon", 1); //ths should wall someoneWon after 1 sec
            //testing 
            //if (timer >= 50)//load scene to test 
            //{
            //    SceneManager.LoadScene("SampleScene");//reload scene to test
            //    score.levelplus();
            //    timer = 0;
            //}
            //if (PlayerPrefs.GetInt("level") == 1)
            //    score.finalScore();
        }

        // Debug.Log(" curr alive " + playersCurrentlyAlive);     

        if (!player1alive)
        {
            m_timeToSpawn1 += Time.deltaTime;
        }

        if (!player2alive)
        {
            m_timeToSpawn2 += Time.deltaTime;
        }

        if (!player3alive)
        {
            m_timeToSpawn3 += Time.deltaTime;
        }

        if (!player4alive)
        {
            m_timeToSpawn4 += Time.deltaTime;
        }
		
		if (player1Moved) {
            player1MovedTimer -= Time.deltaTime;
        }
		
		if (player2Moved)
        {
            player2MovedTimer -= Time.deltaTime;
        }

        if (player3Moved)
        {
            player3MovedTimer -= Time.deltaTime;
        }

        if (player4Moved)
        {
            player4MovedTimer -= Time.deltaTime;
        }

        if (player1Moved && player1MovedTimer > 0)//blinking and moving
        {
           // Debug.Log("im being called");
            player1 = GameObject.Find("GreenPlayer(Clone)");            
            player1.transform.Translate(Vector2.right * Time.deltaTime * 2.5f);
            if (Time.fixedTime % .5 < .2)
            {
                player1.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                player1.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (player1Moved && player1MovedTimer <=0)//once its ready to play again
        {            
            player1.GetComponent<playerController>().enabled = true;
            player1.GetComponent<PolygonCollider2D>().enabled = true;
            player1.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            player1Moved = false;
            player1MovedTimer = timeToBeActive;
        }

        if (player2Moved && player2MovedTimer > 0)
        {
            //Debug.Log("im being called");
            player2 = GameObject.Find("PurplePlayer(Clone)");
            player2.transform.Translate(Vector2.right * Time.deltaTime * 2.5f);
            if (Time.fixedTime % .5 < .2)
            {
                player2.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                player2.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (player2Moved && player2MovedTimer <= 0)
        {            
            player2.GetComponent<PolygonCollider2D>().enabled = true;
            player2.GetComponent<playerController>().enabled = true;
            player2.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            player2Moved = false;
            player2MovedTimer = timeToBeActive;
        }

        if (player3Moved && player3MovedTimer > 0)
        {
            //Debug.Log("im being called");
            player3 = GameObject.Find("RedPlayer(Clone)");
            player3.transform.Translate(Vector2.right * Time.deltaTime * 2.5f);
            if (Time.fixedTime % .5 < .2)
            {
                player3.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                player3.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (player3Moved && player3MovedTimer <= 0)
        {            
            player3.GetComponent<PolygonCollider2D>().enabled = true;
            player3.GetComponent<playerController>().enabled = true;
            player3.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            player3Moved = false;
            player3MovedTimer = timeToBeActive;
        }

        if (player4Moved && player4MovedTimer > 0)
        {
            //Debug.Log("im being called");
            player4 = GameObject.Find("BlackPlayer(Clone)");
            player4.transform.Translate(Vector2.right * Time.deltaTime * 2.5f);
            if (Time.fixedTime % .5 < .2)
            {
                player4.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                player4.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (player4Moved && player4MovedTimer <= 0)
        {
            player4.GetComponent<playerController>().enabled = true;
            player4.GetComponent<PolygonCollider2D>().enabled = true;
            player4.gameObject.transform.GetChild(11).GetComponent<SpriteRenderer>().enabled = true;
            player4Moved = false;
            player4MovedTimer = timeToBeActive;
        }

        //if (m_timeToSpawn1 > timeToRespawn && player1lives > 0) finite lives
        if (m_timeToSpawn1 > timeToRespawn)//infinite lives
        {
            GameObject player1r = Instantiate(player1Prefab, locationToSpawn1, Quaternion.identity);
            player1r.GetComponent<playerController>().myManager = this; //prefabs not saving this unused variable for some reason, you have to hard code it
            player1r.GetComponent<playerController>().playerHealth = playerHealth1;            
            player1r.GetComponent<playerController>().setHealth(100);
            player1r.GetComponent<arrangeLayers>().setOilObjOff();

            //turn off controllers and collider
            player1r.GetComponent<playerController>().enabled = false;
            player1r.GetComponent<PolygonCollider2D>().enabled = false;
            player1Moved = true;           

            player1alive = true;
            m_timeToSpawn1 = 0;
            player1 = player1r;
            //player1lives--;
            playersCurrentlyAlive++;
        }

        //if (m_timeToSpawn2 > timeToRespawn && player2lives > 0) finite lives
        if (m_timeToSpawn2 > timeToRespawn)//infinite lives
        {
            GameObject player2r = Instantiate(player2Prefab, locationToSpawn2, Quaternion.identity);
            player2r.GetComponent<playerController>().myManager = this; //prefabs not saving this unused variable for some reason, you have to hard code it
            player2r.GetComponent<playerController>().playerHealth = playerHealth2;
            player2r.GetComponent<playerController>().setHealth(100);
            player2r.GetComponent<arrangeLayers>().setOilObjOff();

            player2r.GetComponent<playerController>().enabled = false;
            player2r.GetComponent<PolygonCollider2D>().enabled = false;
            player2Moved = true;                        

            player2alive = true;
            m_timeToSpawn2 = 0;
            player2 = player2r;
            //player2lives--;
            playersCurrentlyAlive++;
        }

        //if (m_timeToSpawn3 > timeToRespawn && player3lives > 0) finite lives
        if (m_timeToSpawn3 > timeToRespawn)//infinite lives
        {
            GameObject player3r = Instantiate(player3Prefab, locationToSpawn3, Quaternion.identity);
            player3r.GetComponent<playerController>().myManager = this; //prefabs not saving this unused variable for some reason, you have to hard code it
            player3r.GetComponent<playerController>().playerHealth = playerHealth3;
            player3r.GetComponent<playerController>().setHealth(100);
            player3r.GetComponent<arrangeLayers>().setOilObjOff();

            player3r.GetComponent<playerController>().enabled = false;
            player3r.GetComponent<PolygonCollider2D>().enabled = false;
            player3Moved = true;            

            player3alive = true;
            m_timeToSpawn3 = 0;
            player3 = player3r;
            //player3lives--;
            playersCurrentlyAlive++;
        }


        //if (m_timeToSpawn4 > timeToRespawn && player4lives > 0) finite lives
        if (m_timeToSpawn4 > timeToRespawn) //infinite lives
        {
            GameObject player4r = Instantiate(player4Prefab, locationToSpawn4, Quaternion.identity);
            player4r.GetComponent<playerController>().myManager = this; //prefabs not saving this unused variable for some reason, you have to hard code it
            player4r.GetComponent<playerController>().playerHealth = playerHealth4;
            player4r.GetComponent<playerController>().setHealth(100);
            player4r.GetComponent<arrangeLayers>().setOilObjOff();

            player4r.GetComponent<playerController>().enabled = false;
            player4r.GetComponent<PolygonCollider2D>().enabled = false;            
            player4Moved = true;

            player4alive = true;
            player4 = player4r;
            m_timeToSpawn4 = 0;
            //player4lives--;
            playersCurrentlyAlive++;
        }
    }
    
    void tentacleEvent()
    {

		Instantiate(tentaclePrefab, tentacleSpawnPos.position, tentaclePrefab.transform.rotation);
	//	Instantiate(krakenHead, KrakenStartPos, krakenHead.transform.rotation);//spawning in kraken
    }

    void tankEvent()
    {
        int i = Random.Range(0, 3);

        for (int j = 0; j < 3; j++)
        {
            if (i != j)
            {
                Instantiate(tankWarningPrefab, warningPos[j].position, tankWarningPrefab.transform.rotation);
                Instantiate(tankPrefab, tankPos[j].position, tankPrefab.transform.rotation);
            }
        }
    }

	//void moveKrak(GameObject kraken, Vector2 s, Vector2 e, float time)
	//{
	//	var i = 0.0f;
	//	var timer = 1.0f / time;
	//	while (i < 1.0f)
	//	{
	//		i += Time.deltaTime * timer;
	//		kraken.transform.position = Vector2.Lerp(s, e, i);
	//	}
	//}


	IEnumerator moveKrakenHead(Transform kTrans, Vector2 startPos, Vector2 endPos, float time)
	{
		var j = 0.0f;
		var timer = 1.0f / time;
		while (j < 1.0f) {
			j += Time.deltaTime * timer;
			kTrans.position = Vector2.Lerp(startPos, endPos, j);
			yield return null;
		}
	}

	IEnumerator waitForKrakenHead(float upSp,float downSP, float delay)
	{

		//move head up

		StartCoroutine(moveKrakenHead(krakenHead.transform, KrakenStartPos, KrakenEndPos, upSp));

		yield return new WaitForSeconds(delay); // time between kraken up movement and tentacle spawn
		
		//spawn tentacle
		tentacleEvent();

		yield return new WaitForSeconds(delay); //time between after tentacle spawn and kraken down movement

		//move head down
		StartCoroutine(moveKrakenHead(krakenHead.transform, KrakenEndPos, KrakenStartPos, downSP));

		yield return new WaitForSeconds(delay);

	}

	public void spawnGhost(int num)
    {
        switch (num) {
            case 0:
                GameObject obj = Instantiate(ghostPrefab1, new Vector3(0, 0, 0), Quaternion.identity);
                obj.GetComponent<ghostController>().setPlayerNum(num);
                Debug.Log("player 1 dead");
                player1alive = false;
                score.setp1(score.returnScores(playersCurrentlyAlive));
                playersCurrentlyAlive--;
                break;
            case 1:
                GameObject obj2 = Instantiate(ghostPrefab2, new Vector3(0, 0, 0), Quaternion.identity);
                obj2.GetComponent<ghostController>().setPlayerNum(num);
                Debug.Log("player 2 dead");
                player2alive = false;
                score.setp2(score.returnScores(playersCurrentlyAlive));
                playersCurrentlyAlive--;
                break;
            case 2:
                GameObject obj3 = Instantiate(ghostPrefab3, new Vector3(0, 0, 0), Quaternion.identity);
                obj3.GetComponent<ghostController>().setPlayerNum(num);
                Debug.Log("player 3 dead");
                player3alive = false;
                score.setp3(score.returnScores(playersCurrentlyAlive));
                playersCurrentlyAlive--;
                break;
            case 3:
                GameObject obj4 = Instantiate(ghostPrefab4, new Vector3(0, 0, 0), Quaternion.identity);
                obj4.GetComponent<ghostController>().setPlayerNum(num);
                Debug.Log("player 4 dead");
                player4alive = false;
                score.setp4(score.returnScores(playersCurrentlyAlive));
                playersCurrentlyAlive--;
                break;
            default:
                Debug.Log("wrong num");
                break;
        }       
    }    

    public void spawnPlayer(int num)
    {
        switch (num)
        {
            case 0:
                player1alive = false;
                playersCurrentlyAlive--;
                break;
            case 1:
                player2alive = false;
                playersCurrentlyAlive--;
                break;
            case 2:
                player3alive = false;
                playersCurrentlyAlive--;
                break;
            case 3:
                player4alive = false;
                playersCurrentlyAlive--;
                break;
            default:
                Debug.Log("wrong num");
                break;
        }
    }

    void someoneWon() {
        //if (player1alive)
        //{
        //    Debug.Log("PLAYER 1 WINS");
        //    winPanel.SetActive(true);
        //    winnerText.text = "Winner is Player1";

        //    if (PlayerPrefs.GetInt("level") == 0)//if its level 1
        //        score.setp1(score.returnScores(playersCurrentlyAlive));
        //    else if (PlayerPrefs.GetInt("level") == 1)// if its level two
        //        score.Level2setp1(score.returnScores(playersCurrentlyAlive));

        //    Time.timeScale = 0; //pause the game

        //}
        //else if (player2alive)
        //{
        //    Debug.Log("PLAYER 2 WINS");
        //    winPanel.SetActive(true);
        //    winnerText.text = "Winner is Player2";

        //    if (PlayerPrefs.GetInt("level") == 0)//if its level 1
        //        score.setp2(score.returnScores(playersCurrentlyAlive));
        //    else if (PlayerPrefs.GetInt("level") == 1)// if its level two
        //        score.Level2setp2(score.returnScores(playersCurrentlyAlive));

        //    Time.timeScale = 0;

        //}
        //else if (player3alive)
        //{
        //    Debug.Log("PLAYER 3 WINS");
        //    winPanel.SetActive(true);
        //    winnerText.text = "Winner is Player3";

        //    if (PlayerPrefs.GetInt("level") == 0)//if its level 1
        //        score.setp3(score.returnScores(playersCurrentlyAlive));
        //    else if (PlayerPrefs.GetInt("level") == 1)// if its level two
        //        score.Level2setp3(score.returnScores(playersCurrentlyAlive));

        //    Time.timeScale = 0;
        //}
        //else if (player4alive)
        //{
        //    Debug.Log("PLAYER 4 WINS");
        //    winPanel.SetActive(true);
        //    winnerText.text = "Winner is Player4";
        //    if (PlayerPrefs.GetInt("level") == 0)//if its level 1
        //        score.setp4(score.returnScores(playersCurrentlyAlive));
        //    else if (PlayerPrefs.GetInt("level") == 1)// if its level two
        //        score.Level2setp4(score.returnScores(playersCurrentlyAlive));

        //    Time.timeScale = 0;
        //}
    }
}

