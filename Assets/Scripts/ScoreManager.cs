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

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setScore() {
     PlayerPrefs.SetInt("first",4);
     PlayerPrefs.SetInt("second",6);
     PlayerPrefs.SetInt("third",8);
     PlayerPrefs.SetInt("winner", 10);
    }
    //debug by print the scores 
    public void print() {
        Debug.Log("player 1 " + PlayerPrefs.GetInt("p1"));
        Debug.Log("player 2 " + PlayerPrefs.GetInt("p2"));
        Debug.Log("player 3 " + PlayerPrefs.GetInt("p3"));
        Debug.Log("player 4 " + PlayerPrefs.GetInt("p4"));
    }

    public int returnScores(int numPlayers)
    {
        switch (numPlayers)
        {
            case 4:
                return PlayerPrefs.GetInt("first");//first to die get 4 points
            case 3:
                return PlayerPrefs.GetInt("second");//second to die get 6 points
            case 2:
                return PlayerPrefs.GetInt("third");//third to die get 8 points
            case 1:
                return PlayerPrefs.GetInt("winner");
        }
        return 0;
    }

    // saved all four players score
    public void setp1(int num) {
        p1Score = num;
        PlayerPrefs.SetInt("p1", p1Score);
    }

    public void setp2(int num)
    {
        p2Score = num;
        PlayerPrefs.SetInt("p2", p2Score);
    }

    public void setp3(int num)
    {
        p3Score = num;
        PlayerPrefs.SetInt("p3", p3Score);
    }

    public void setp4(int num)
    {
        p4Score = num;
        PlayerPrefs.SetInt("p4", p4Score);
    }
}
