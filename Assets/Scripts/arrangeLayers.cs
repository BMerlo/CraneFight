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
    }
}
