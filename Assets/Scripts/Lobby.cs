using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    //check if players are ready
     bool p1ready;
     bool p2ready;
     bool p3ready;
     bool p4ready;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p1ready && p2ready && p3ready && p4ready)
        {
            Invoke("loadscene", 1.0f);
            PlayerPrefs.DeleteAll();
        }
        overrides();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        //check if the collision object is a pickable

        //if it has layer "p1thrown"
        if (other.gameObject.tag == "Pickable" && other.gameObject.layer == 8) {

            p1ready = true;
           // animator.Play();
        }
        //if it has layer "p2thrown"
        if (other.gameObject.tag == "Pickable" && other.gameObject.layer == 9)
        {
            p2ready = true;
            // animator.Play();
        }
        //if it has layer "p3thrown"
        if (other.gameObject.tag == "Pickable" && other.gameObject.layer == 10)
        {
            p3ready = true;
            // animator.Play();
        }
        //if it has layer "p4thrown"
        if (other.gameObject.tag == "Pickable" && other.gameObject.layer == 11)
        {
            p4ready = true;
            // animator.Play();
        }
    }
    //start game with button
    public void overrides() {
        if (Input.GetButtonDown("p")) {
            p1ready = true;
            p2ready = true;
            p3ready = true;
            p4ready = true;
        }
    }
    //load scene
    public void loadscene() {
        SceneManager.LoadScene("SampleScene");
    }
}
