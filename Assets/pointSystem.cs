using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointSystem : MonoBehaviour
{
    [SerializeField] Transform upLeft, downRight;
    float MaxX, MinX, MaxY, MinY;

    List<playerController> playersHere;
    playerController playerHere;

    float p1points = 0;
    float p2points = 0;
    float p3points = 0;
    float p4points = 0;

    float pointMultiplier = 3.5f;

    [SerializeField] TextMeshProUGUI p1pointText;
    [SerializeField] TextMeshProUGUI p2pointText;
    [SerializeField] TextMeshProUGUI p3pointText;
    [SerializeField] TextMeshProUGUI p4pointText;


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
            player.setCrown(false);
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
            switch (playerHere.getPlayerNum())
            {
                case 1:
                    p1points += Time.deltaTime * pointMultiplier;
                    break;
                case 2:
                    p2points += Time.deltaTime * pointMultiplier;
                    break;
                case 3:
                    p3points += Time.deltaTime * pointMultiplier;
                    break;
                case 4:
                    p4points += Time.deltaTime * pointMultiplier;
                    break;
                default:
                    break;
            }
            updatePoints();
            playerHere.setCrown(true);
        }

    }

    void updatePoints()
    {
        p1pointText.text = ((int)p1points).ToString();
        p2pointText.text = ((int)p2points).ToString();
        p3pointText.text = ((int)p3points).ToString();
        p4pointText.text = ((int)p4points).ToString();


        if (p1points > 100 || p2points > 100 || p3points > 100 || p4points > 100)
        {
            Time.timeScale = 0;
            Invoke("reloadLevel", 2.0f);
        }
    }

    void reloadLevel()
    {
        Application.LoadLevel(0);
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
