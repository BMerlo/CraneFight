using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour
{
    PressStart readyManager;

    enum PlayerNumber
    {
        P1,
        P2,
        P3,
        P4
    }

    [SerializeField] PlayerNumber playerNum;

    private int getPlayerNum()
    {
        switch (playerNum)
        {
            case PlayerNumber.P1:
                return 1;
            case PlayerNumber.P2:
                return 2;
            case PlayerNumber.P3:
                return 3;
            case PlayerNumber.P4:
                return 4;
            default:
                break;
        }
        return 0;
    }

    void Start()
    {
        readyManager = FindObjectOfType<PressStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getOwnButtonDown("A")) {
            
            switch (getPlayerNum()) {
                case 1:
                    GetComponent<Image>().color = Color.green;
                    readyManager.P1Ready = true;
                    PlayerPrefs.SetInt("numberPlayer1", 1);
                    PlayerPrefs.Save();
                    Debug.Log(PlayerPrefs.GetInt("numberPlayer1"));
                    break;
                case 2:
                    GetComponent<Image>().color = Color.yellow;
                    readyManager.P2Ready = true;
                    PlayerPrefs.SetInt("numberPlayer2", 2);
                    PlayerPrefs.Save();
                    Debug.Log(PlayerPrefs.GetInt("numberPlayer2"));
                    break;
                case 3:
                    GetComponent<Image>().color = Color.red;
                    readyManager.P3Ready = true;
                    PlayerPrefs.SetInt("numberPlayer3", 3);
                    PlayerPrefs.Save();
                    Debug.Log(PlayerPrefs.GetInt("numberPlayer3"));
                    break;
                case 4:
                    GetComponent<Image>().color = Color.black;
                    readyManager.P4Ready = true;
                    PlayerPrefs.SetInt("numberPlayer4", 4);
                    PlayerPrefs.Save();
                    Debug.Log(PlayerPrefs.GetInt("numberPlayer4"));
                    break;
                default:
                    break;
            }
                
        }
    }

    bool getOwnButtonDown(string i)
    {
        return Input.GetButtonDown(playerNum.ToString() + i);
    }
}
