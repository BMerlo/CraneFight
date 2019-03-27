using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrangeLayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 100 - Mathf.FloorToInt(this.transform.position.y * 10);

        Transform SmellCloudF = transform.Find("SmellCloudF");
        Transform smellChildB = transform.Find("SmellCloudB");
       //Transform oilChild = transform.Find("OilyPlaceHolder");//Oily Child not implemented on the Player prefab yet.
        if (SmellCloudF != null)
        {
            SmellCloudF.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 10;
        }
        if (smellChildB != null)
        {
            smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) - 10;
        }
       // if (oilChild != null)
       // {
      //      smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 10;
       // }
    }
}
