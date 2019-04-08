using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrangeLayers : MonoBehaviour
{
    [SerializeField] GameObject spriteHolder;
    SpriteRenderer mainSprite;

    // Start is called before the first frame update
    void Start()
    {
        //mainSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if (GetComponent<SpriteRenderer>()) {
            mainSprite = GetComponent<SpriteRenderer>();
        }
        else
        {
            mainSprite = spriteHolder.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainSprite)
        {
            mainSprite.sortingOrder = 100 - Mathf.FloorToInt(this.transform.position.y * 10);
        }
        else
        {
            Debug.Log("Wtf");
        }
        

        Transform SmellCloudF = transform.Find("SmellCloudF");
        Transform smellChildB = transform.Find("SmellCloudB");
        Transform oilySprite = transform.Find("OilyPlaceHolder");
        //Transform oilChild = transform.Find("OilyPlaceHolder");//Oily Child not implemented on the Player prefab yet.
        if (SmellCloudF != null)
        {
            SmellCloudF.GetComponent<SpriteRenderer>().sortingOrder = (mainSprite.sortingOrder) + 5;
        }
        if (smellChildB != null)
        {
            smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (mainSprite.sortingOrder) - 10;
        }
        if (oilySprite != null)
        {
            oilySprite.GetComponent<SpriteRenderer>().sortingOrder = (mainSprite.sortingOrder) + 1;
        }
        // if (oilChild != null)
        // {
        //      smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 10;
        // }
    }
}
