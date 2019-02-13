using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePusher : MonoBehaviour
{
    public float ExplosionRadius;
    public float m_ExplosionForce;
    private float counter;

    void Start()
    {
        findExplosiones();
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //counter += Time.deltaTime;

        ////every 2 seconds will push other objects
        //if (counter >= 2)
        //{
        //    findExplosiones();
        //    counter = 0;
        //}
    }

    private void findExplosiones()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody2D targetRigidbody = colliders[i].GetComponent<Rigidbody2D>();

            //Debug.Log("Collided with " + targetRigidbody.gameObject);

            if (!targetRigidbody)
                continue;

            AddExplosionForce(targetRigidbody, m_ExplosionForce, transform.position, ExplosionRadius);
        }
    }

    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        Vector3 dir = body.transform.position - explosionPosition;
        body.AddForce(dir.normalized * explosionForce);
    }
}
