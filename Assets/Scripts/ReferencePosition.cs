using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePosition : MonoBehaviour
{
    [SerializeField] Transform TL;
    [SerializeField] Transform TR;
    [SerializeField] Transform BL;
    [SerializeField] Transform BR;
    public Kraken krakenObj;
    public bool krakenFound;

    void Start()
    {
        krakenFound = false;
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
}

