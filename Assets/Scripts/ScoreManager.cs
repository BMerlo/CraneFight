using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    ////
    int p1Score;
    int p2Score;
    int p3Score;
    int p4Score;
    int level = 0;
    void Start()
    {

        Debug.Log("start--------------------------------- ");
        Debug.Log("player 1 " + PlayerPrefs.GetInt("p1"));
        Debug.Log("player 2 " + PlayerPrefs.GetInt("p2"));
        Debug.Log("player 3 " + PlayerPrefs.GetInt("p3"));
        Debug.Log("player 4 " + PlayerPrefs.GetInt("p4"));

        Debug.Log("Level 2 player 1 " + PlayerPrefs.GetInt("Level2p1"));
        Debug.Log("Level 2 player 2 " + PlayerPrefs.GetInt("Level2p2"));
        Debug.Log("Level 2 player 3 " + PlayerPrefs.GetInt("Level2p3"));
        Debug.Log("Level 2 player 4 " + PlayerPrefs.GetInt("Level2p4"));
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //debug by print the scores 
    public void print()
    {
        Debug.Log("player 1 " + PlayerPrefs.GetInt("p1"));
        Debug.Log("player 2 " + PlayerPrefs.GetInt("p2"));
        Debug.Log("player 3 " + PlayerPrefs.GetInt("p3"));
        Debug.Log("player 4 " + PlayerPrefs.GetInt("p4"));

        Debug.Log("Level " + PlayerPrefs.GetInt("level"));

        Debug.Log("Level 2 player 1 " + PlayerPrefs.GetInt("Level2p1"));
        Debug.Log("Level 2 player 2 " + PlayerPrefs.GetInt("Level2p2"));
        Debug.Log("Level 2 player 3 " + PlayerPrefs.GetInt("Level2p3"));
        Debug.Log("Level 2 player 4 " + PlayerPrefs.GetInt("Level2p4"));

        Debug.Log("final player 1 " + PlayerPrefs.GetInt("finalp1"));
        Debug.Log("final player 2 " + PlayerPrefs.GetInt("finalp2"));
        Debug.Log("final player 3 " + PlayerPrefs.GetInt("finalp3"));
        Debug.Log("final player 4 " + PlayerPrefs.GetInt("finalp4"));

    }

    public int returnScores(int numPlayers)
    {
        switch (numPlayers)
        {
            case 4:
                return 4;//first to die get 4 points
            case 3:
                return 6;//second to die get 6 points
            case 2:
                return 8;//third to die get 8 points
            case 1:
                return 10;
        }
        return 0;
    }

    // saved all four players score
    public void setp1(int num)
    {
        PlayerPrefs.SetInt("p1", num);
    }

    public void setp2(int num)
    {
        PlayerPrefs.SetInt("p2", num);
    }

    public void setp3(int num)
    {
        PlayerPrefs.SetInt("p3", num);
    }

    public void setp4(int num)
    {
        PlayerPrefs.SetInt("p4", num);
    }

    //call this function when open level two
    public void levelplus()
    {
        PlayerPrefs.SetInt("level", 1);
    }
    //call this when level one is load
    public void resetlevel()
    {
        PlayerPrefs.SetInt("level", 0);
    }
    //Option A
    public void Level2setp1(int num)
    {
        //num += PlayerPrefs.GetInt("p1");
        PlayerPrefs.SetInt("Level2p1", num);
    }

    public void Level2setp2(int num)
    {
        //num += PlayerPrefs.GetInt("p2");
        PlayerPrefs.SetInt("Level2p2", num);
    }

    public void Level2setp3(int num)
    {
        //num += PlayerPrefs.GetInt("p3");
        PlayerPrefs.SetInt("Level2p3", num);
    }

    public void Level2setp4(int num)
    {
       // num += PlayerPrefs.GetInt("p4");
        PlayerPrefs.SetInt("Level2p4", num);
    }

    //add the final score
    public void finalScore() {
        p1Score = PlayerPrefs.GetInt("p1") + PlayerPrefs.GetInt("Level2p1");
        p2Score = PlayerPrefs.GetInt("p2") + PlayerPrefs.GetInt("Level2p2");
        p3Score = PlayerPrefs.GetInt("p3") + PlayerPrefs.GetInt("Level2p3");
        p4Score = PlayerPrefs.GetInt("p4") + PlayerPrefs.GetInt("Level2p4");

        PlayerPrefs.SetInt("finalp1", p1Score);
        PlayerPrefs.SetInt("finalp2", p2Score);
        PlayerPrefs.SetInt("finalp3", p3Score);
        PlayerPrefs.SetInt("finalp4", p4Score);

    }
    // option B
    //Level 2 save scores
    //public void Level2setp1(int num)
    //{
    //    num += PlayerPrefs.GetInt("p1");
    //    PlayerPrefs.SetInt("Level2p1", num);
    //}

    //public void Level2setp2(int num)
    //{
    //    num += PlayerPrefs.GetInt("p2");
    //    PlayerPrefs.SetInt("Level2p2", num);
    //}

    //public void Level2setp3(int num)
    //{
    //    num += PlayerPrefs.GetInt("p3");
    //    PlayerPrefs.SetInt("Level2p3", num);
    //}

    //public void Level2setp4(int num)
    //{
    //    num += PlayerPrefs.GetInt("p4");
    //    PlayerPrefs.SetInt("Level2p4", num);
    //}

}
