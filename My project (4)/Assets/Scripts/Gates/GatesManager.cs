using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GatesManager : MonoBehaviour
{

   

    public bool iswizardDestryed = false;
   
    void Start()
    {
        
    }

 
    void Update()
    {
    
        
    }

    public void MainScenePass()
    {

        SceneManager.LoadScene("MainLevel");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

          

            if (Input.GetKeyDown(KeyCode.E)&&iswizardDestryed)
            {
                MainScenePass();


            }

        }
        else
        {
           

        }
    }

}
