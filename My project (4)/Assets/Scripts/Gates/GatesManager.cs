using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GatesManager : MonoBehaviour
{
    public bool ismainGate;

    public ButtonEvent Buttonevent;

    public int Level;


    void Start()
    {
        if(SceneManager.GetActiveScene().name== "MainLevelArk")
        {
            ismainGate = true;
        }
        else
        {
            ismainGate = false;
        }
        Buttonevent = GameObject.Find("EventKey").GetComponent<ButtonEvent>();
    }

 
    void Update()
    {
    
        
    }

    public void MainScenePass()
    {

        SceneManager.LoadScene("MainLevelArk");

    }

    public void PassLevelSelection()
    {
        SceneManager.LoadScene("AraLevel");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

          

            if (Buttonevent.keydown)
            {
                if (!ismainGate)
                {
                    MainScenePass();
                }
                else
                {
                   PlayerPrefs.SetInt("LevelOfGame", Level);

                    PassLevelSelection();
                }
                


            }

        }
        else
        {
           

        }
    }

}
