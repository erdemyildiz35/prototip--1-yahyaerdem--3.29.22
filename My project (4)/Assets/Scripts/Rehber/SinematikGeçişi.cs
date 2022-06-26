using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SinematikGeçişi : MonoBehaviour
{
    public bool sinematikGeç=false;
    public bool TutorialGeç = false;
    void Start()
    {
        if (sinematikGeç)
        {
            sinematiğeGeç();
        }

        if (TutorialGeç)
        {
            TurtorialGec();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void sinematiğeGeç()
    {

        SceneManager.LoadScene("TimelineArasahne");
    }
    public void TurtorialGec()
    {
        SceneManager.LoadScene("Tutorial");

    }

}
