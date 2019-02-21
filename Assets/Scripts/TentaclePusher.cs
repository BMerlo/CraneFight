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
        Debug.Log("pusher is here~");
        findExplosiones();
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //counter += Time.deltaTime;

        ////every 2 seconds will push other objects NO WE ONLY DO IT ONCE
        //if (counter >= 2)
        //{
        //    findExplosiones();
        //    counter = 0;
        //}
    }

    private void findExplosiones()
    {

        Debug.Log("trying to explode");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody2D targetRigidbody = colliders[i].GetComponent<Rigidbody2D>();

            //Debug.Log("PUSHER Collided with " + targetRigidbody.gameObject);

            if (!targetRigidbody)
                continue;

            float perc = Vector2.Distance(transform.position, targetRigidbody.transform.position) / ExplosionRadius;
            AddExplosionForce(targetRigidbody, m_ExplosionForce, transform.position, perc);
        }
    }

    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float perc)
    {
        Vector3 dir = body.transform.position - explosionPosition;
        body.AddForce(dir.normalized * explosionForce * perc);
    }
}
