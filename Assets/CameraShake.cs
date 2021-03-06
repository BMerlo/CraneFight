﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orgPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(orgPos.x + x, orgPos.y + y, orgPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orgPos;


    }
}
