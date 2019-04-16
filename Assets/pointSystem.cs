using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //[SerializeField] TextMeshProUGUI p1pointText;
    //[SerializeField] TextMeshProUGUI p2pointText;
    //[SerializeField] TextMeshProUGUI p3pointText;
    //[SerializeField] TextMeshProUGUI p4pointText;

    [SerializeField] RectTransform p1pointTrans, p2pointTrans, p3pointTrans, p4pointTrans;



    public Sprite p1Sprite, p2Sprite, p3Sprite, p4Sprite;
	[SerializeField] Image winnerSprite;
	[SerializeField] GameObject winPanel;
	[SerializeField] GameObject rocketPanel;


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
        //p1pointText.text = ((int)p1points).ToString();
        //p2pointText.text = ((int)p2points).ToString();
        //p3pointText.text = ((int)p3points).ToString();
        //p4pointText.text = ((int)p4points).ToString();
        p1pointTrans.transform.localScale = new Vector3(1.0f - (p1points / 100f), p1pointTrans.transform.localScale.y, p1pointTrans.transform.localScale.z);
        p2pointTrans.transform.localScale = new Vector3(1.0f - (p2points / 100f), p2pointTrans.transform.localScale.y, p2pointTrans.transform.localScale.z);
        p3pointTrans.transform.localScale = new Vector3(1.0f - (p3points / 100f), p3pointTrans.transform.localScale.y, p3pointTrans.transform.localScale.z);
        p4pointTrans.transform.localScale = new Vector3(1.0f - (p4points / 100f), p4pointTrans.transform.localScale.y, p4pointTrans.transform.localScale.z);

        if (p1points > 100 || p2points > 100 || p3points > 100 || p4points > 100)
        {
			//winPanel.SetActive(true);
			Time.timeScale = 0;
			//winPanel.GetComponent<Image>().enabled = true;
			winPanel.SetActive(true);
			rocketPanel.GetComponent<Image>().enabled = true;

			if (p1points > 10)
			{
				winnerSprite.sprite = p1Sprite;
			}
			else if (p2points > 10)
			{
				winnerSprite.sprite = p2Sprite;
			}
			else if (p3points > 10)
			{
				winnerSprite.sprite = p3Sprite;
			}
			else if (p4points > 10)
			{
				winnerSprite.sprite = p4Sprite;
			}

			if (ReInput.players.SystemPlayer.GetButtonDown("Restart"))//restart level when Enter/Return key is pressed
			{
				Invoke("reloadLevel", 0);
			}
			
		}
    }

    void reloadLevel()
    {
     	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
