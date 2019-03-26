using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePosition : MonoBehaviour
{
    [SerializeField] Transform TL;
    [SerializeField] Transform TR;
    [SerializeField] Transform BL;
    [SerializeField] Transform BR;//here in case needed for 2nd map
    [SerializeField] Transform Top1;
    [SerializeField] Transform Top2;
    [SerializeField] Transform Mid;
    [SerializeField] Transform Bot1;
    [SerializeField] Transform Bot2;
    public Kraken krakenObj = null;
    private bool krakenFound = false;
    private bool playerSet = false; 

    void Start()
    {
        krakenFound = false;
        playerSet = false;
        //Debug.Log("TL: " + TL.position);
        //Debug.Log("TR: " + TL.position);
        //Debug.Log("BL: " + TL.position);
        //Debug.Log("BR: " + TL.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!krakenFound) { //it will only search until kraken is on
            krakenObj = FindObjectOfType<Kraken>();
            krakenObj.GetComponent<Kraken>().setRandomLx(TL.position.x);
            krakenObj.GetComponent<Kraken>().setRandomRx(TR.position.x);
            krakenFound = true;
        }
    }

    public float getReferenceTL() {

        return TL.position.y;
    }

    public float getReferenceT1()
    {

        return Top1.position.y;
    }

    public float getReferenceT2()
    {

        return Top2.position.y;
    }

    public float getReferenceMid()
    {
        return Mid.position.y;
    }

    public float getReferenceB1()
    {
        return Bot1.position.y;
    }

    public float getReferenceB2()
    {
        return Bot2.position.y;
    }

    public float getReferenceBL()
    {
        return BL.position.y;
    }
}

