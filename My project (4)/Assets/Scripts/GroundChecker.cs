using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    Movement PlayerMovement;
    [SerializeField] LayerMask GroundLayer;

    void Start()
    {

        PlayerMovement = FindObjectOfType<Movement>();

    }

    
    void Update()
    {
       

        if (Physics2D.OverlapCircle(transform.position, .1f, GroundLayer))
        {

            PlayerMovement.Onground = true;


        }
        else
        {

            PlayerMovement.Onground = false;
        }

    }
}
