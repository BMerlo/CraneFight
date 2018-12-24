using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {
    [SerializeField] float scrollSpeed;
    [SerializeField] float tileSize;

    Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPos + -Vector3.right * newPos;
	}
}
