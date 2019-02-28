using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    public float delayToLoadScene = 3;
        
    public bool P1Ready, P2Ready, P3Ready, P4Ready;
       
    // Start is called before the first frame update
    void Start()
    {
        P1Ready = false;
        P2Ready = false;
        P3Ready = true;//I don't have 4 controllers x)
        P4Ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (P1Ready && P2Ready/*&&P3Ready&&P4Ready*/)
        {
            Invoke("startGame", delayToLoadScene); 
        }
    }

    void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
