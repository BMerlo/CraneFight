using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

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

    [SerializeField] Vector3 locationToSpawn;

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

    private float timeToRespawn = 5;
    

    //reference to prefabs
    [SerializeField] GameObject player1respawn;
    [SerializeField] GameObject player2respawn;
    [SerializeField] GameObject player3respawn;
    [SerializeField] GameObject player4respawn;

    private float m_timeElapsed;

    [SerializeField] GameObject ghostPrefab;
    private Text winnerText;

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

        winnerText.text = "Winner is..."; //added for debugging purposes 

        player1alive = true;
        player2alive = true;
        player3alive = true;
        player4alive = true;

        needsGhost = false;

        playersCurrentlyAlive = 4;

        //lastGhostToSpawn = -1;
        //ghostToSpawn = -1;

        m_timeElapsed = 0;
        m_timeToSpawn1 = 0;
        m_timeToSpawn2 = 0;
        m_timeToSpawn3 = 0;
        m_timeToSpawn4 = 0;

        //numberOfPlayers = player1, player2, player3, player4;
    }
	
	// Update is called once per frame
	void Update ()
    {
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
            if (player1alive)
            {
                Debug.Log("PLAYER 1 WINS");
                winPanel.SetActive(true);
                winnerText.text = "Winner is Player1";
                Time.timeScale = 0; //pause the game
            }
            else if (player2alive)
            {
                Debug.Log("PLAYER 2 WINS");
                winPanel.SetActive(true);
                winnerText.text = "Winner is Player2";
                Time.timeScale = 0;
            }
            else if (player3alive)
            {
                Debug.Log("PLAYER 3 WINS");
                winPanel.SetActive(true);
                winnerText.text = "Winner is Player3";
                Time.timeScale = 0;
            }
            else if (player4alive)
            {
                Debug.Log("PLAYER 4 WINS");
                winPanel.SetActive(true);
                winnerText.text = "Winner is Player4";
                Time.timeScale = 0;
            }

        }

        if (!player1alive) {
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

        if (m_timeToSpawn1 > timeToRespawn) {
            GameObject player1r = Instantiate(player1respawn, new Vector3(0, 0, 0), Quaternion.identity);
            player1alive = true;
            m_timeToSpawn1 = 0;
            playersCurrentlyAlive++;
        }

        if (m_timeToSpawn2 > timeToRespawn)
        {
            GameObject player2r = Instantiate(player2respawn, new Vector3(0, 0, 0), Quaternion.identity);
            player2alive = true;
            m_timeToSpawn2 = 0;
            playersCurrentlyAlive++;
        }
        if (m_timeToSpawn3 > timeToRespawn)
        {
            GameObject player3r = Instantiate(player3respawn, new Vector3(0, 0, 0), Quaternion.identity);
            player3alive = true;
            m_timeToSpawn3 = 0;
            playersCurrentlyAlive++;
        }
        if (m_timeToSpawn4 > timeToRespawn)
        {
            GameObject player4r = Instantiate(player4respawn, new Vector3(0, 0, 0), Quaternion.identity);
            player4alive = true;
            m_timeToSpawn4 = 0;
            playersCurrentlyAlive++;
        }

    }

    public void spawnGhost(int num)
    {
        GameObject obj = Instantiate(ghostPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        obj.GetComponent<ghostController>().setPlayerNum(num);
        playersCurrentlyAlive--;
    }

}

