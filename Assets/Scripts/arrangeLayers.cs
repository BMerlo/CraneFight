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
		Transform Rthrow = transform.Find("RThrowC");
		Transform Lthrow = transform.Find("LThrowC");
		Transform Uthrow = transform.Find("UThrow");
		Transform Dthrow = transform.Find("DThrow");
		Transform RUthrow = transform.Find("RUThrow");
		Transform LUthrow = transform.Find("LUThrow");
		Transform RDthrow = transform.Find("RDThrow");
		Transform LDthrow = transform.Find("LDThrow");
		Transform craneA = transform.Find("CraneActual"); // can take this out if we dont use Crane Actual

		if (SmellCloudF != null)
        {
            SmellCloudF.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 10;
        }
        if (smellChildB != null)
        {
            smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) - 10;
        }

		if (craneA != null)
		{
			craneA.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}

		if (Rthrow != null)
		{
			Rthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (Lthrow != null)
		{
			Lthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (Uthrow != null)
		{
			Uthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (Dthrow != null)
		{
			Dthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (RUthrow != null)
		{
			RUthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (LUthrow != null)
		{
			LUthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (RDthrow != null)
		{
			RDthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		if (LDthrow != null)
		{
			LDthrow.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 15;
		}
		// if (oilChild != null)
		// {
		//      smellChildB.GetComponent<SpriteRenderer>().sortingOrder = (GetComponent<SpriteRenderer>().sortingOrder) + 10;
		// }
	}
}
