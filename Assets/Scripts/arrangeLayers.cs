using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrangeLayers : MonoBehaviour
{
    [SerializeField] GameObject spriteHolder;
    [SerializeField] SpriteRenderer mainSprite;
    [SerializeField] SpriteRenderer oilySprite;

    [SerializeField] SpriteRenderer shadow;


    [SerializeField] SpriteRenderer[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        //Transform SmellCloudF = transform.Find("SmellCloudF");
        //Transform smellChildB = transform.Find("SmellCloudB");

        sprites = GetComponentsInChildren<SpriteRenderer>(true);
        //setOilObjOff();

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



        //Transform oilChild = transform.Find("OilyPlaceHolder");//Oily Child not implemented on the Player prefab yet.
        //if (SmellCloudF != null)
        //{
        //    SmellCloudF.GetComponent<SpriteRenderer>().sortingOrder = (mainSprite.sortingOrder) + 5;
        //}
        //if (smellChildB != null)
        //{
        //    smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (mainSprite.sortingOrder) - 10;
        //}
        if (GetComponent<playerController>() != null)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite != oilySprite && sprite != shadow && sprite != mainSprite)
                {
                    sprite.sortingOrder = (mainSprite.sortingOrder) + 2;
                }
            }
        }
        else {
            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite != oilySprite && sprite != shadow && sprite != mainSprite)
                {
                    sprite.sortingOrder = (mainSprite.sortingOrder) + 2;
                }
            }
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

    public void jump()
    {
        mainSprite.sortingLayerName = "Air";

        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite != shadow)
            {
                sprite.sortingLayerName = "Air";
            }         
        }

        oilySprite.sortingLayerName = "Air";
    }

    public void unJump()
    {
        mainSprite.sortingLayerName = "Default";

        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite != shadow)
            {
            sprite.sortingLayerName = "Default";
            }
        }

        oilySprite.sortingLayerName = "Default";
    }

    public void setOilObjOff()
    {
        oilySprite.gameObject.SetActive(false);
    }
}
