using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzaelTrap : MonoBehaviour
{
    [SerializeField] GameObject Azael;
    bool TimePass = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Invoke("ActiveTrap", 1f);

        }
    }

    private void ActiveTrap()
    {
        if (TimePass)
        {
            Instantiate(Azael, transform.position, Quaternion.identity);
            TimePass = false;
            Invoke("OpenAgain", 15f);
        }
       
   
    }

    void OpenAgain()
    {

        TimePass = true;

    }

}
