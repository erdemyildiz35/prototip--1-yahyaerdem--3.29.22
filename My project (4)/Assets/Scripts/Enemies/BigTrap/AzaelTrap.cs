using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzaelTrap : MonoBehaviour
{
    [SerializeField] GameObject Azael;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Invoke("ActiveTrap", 1f);

        }
    }

    private void ActiveTrap()
    {

        Instantiate(Azael, transform.position, Quaternion.identity);

    }
}
