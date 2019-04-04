using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSystem : MonoBehaviour
{
    [SerializeField] Transform upLeft, downRight;
    float MaxX, MinX, MaxY, MinY;

    List<playerController> playersHere;
    playerController playerHere;

    // Start is called before the first frame update
    void Start()
    {
        MaxX = downRight.position.x;
        MaxY = upLeft.position.y;
        MinX = upLeft.position.x;
        MinY = downRight.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        playerController[] players = FindObjectsOfType<playerController>();
        int numHere = 0;
        foreach (playerController player in players)
        {
            if (isInTheZone(player.transform))
            {
                numHere++;
                playerHere = player;
            }
            else
            {   //hide crown

            }
        }

        if (numHere == 1)
        {   //show crown, give points

        }

    }


    bool isInTheZone(Transform transf)
    {
        if (transf.position.x < MaxX && transf.position.x > MinX )
        {
            if (transf.position.y > MinY && transf.position.y < MaxY)
            {
                Debug.Log("Ypu are in the zone!");
                return true;
            }
        }
        return false;
    }
}
