using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    /*[SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;*/

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject tiePanel;

    [SerializeField] GameObject[] numberOfPlayers;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;

        //numberOfPlayers = player1, player2, player3, player4;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (numberOfPlayers.Length == 1)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
        else if (numberOfPlayers.Length == 0)
        {
            Time.timeScale = 0;
            tiePanel.SetActive(true);
        }
    }
}
