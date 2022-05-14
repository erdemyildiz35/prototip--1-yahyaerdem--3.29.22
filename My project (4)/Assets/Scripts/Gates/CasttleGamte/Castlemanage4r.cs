using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castlemanage4r : MonoBehaviour
{
    [SerializeField] Canvas ToTheMainGateCanvas;

    int orbs=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }


    public void orbClear()
    {

        orbs++;


    }

    public void orbsAreCleard()
    {
       


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToTheMainGateCanvas.gameObject.SetActive(false);


        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (orbs >= 3)
            {
                ToTheMainGateCanvas.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    SceneManager.LoadScene("MainLevel");
                   
                }

            }
           
            


        }

    }


}
