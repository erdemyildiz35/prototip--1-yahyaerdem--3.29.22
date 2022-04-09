using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainPlayGroundSW : MonoBehaviour
{
    [SerializeField] GameObject CAnvas;

    void Start()
    {
        CAnvas.SetActive(false);
    }

    void Update()
    {
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            CAnvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {

                SceneManager.LoadScene("MainLevel");
            }


        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                SceneManager.LoadScene("MainLevel");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            CAnvas.SetActive(false);
        }
    }

}
