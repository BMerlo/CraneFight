using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;

    //[SerializeField] GameObject winPanel;
    //[SerializeField] GameObject tiePanel;

    [SerializeField] GameObject[] numberOfPlayers;

    //public Transform player1Location;
    //public Transform player2Location;
    //public Transform player3Location;
    //public Transform player4Location;

    [SerializeField] Vector3 locationToSpawn;

    bool player1alive;
    bool player2alive;
    bool player3alive;
    bool player4alive;

    [SerializeField] GameObject ghostPrefab;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;        
        //locationToSpawn = new Vector3(0,0,0);
        //player2Location;
        //player3Location;
        //player4Location;

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
    }

    void spawnGhost()
    {
        Instantiate(ghostPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

}

