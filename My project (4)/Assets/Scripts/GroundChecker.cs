using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    AventurerMove PlayerMovement;
    [SerializeField] LayerMask GroundLayer;

    void Start()
    {

        PlayerMovement = FindObjectOfType<AventurerMove>();

    }

    
    void Update()
    {
       

        if (Physics2D.OverlapCircle(transform.position, .1f, GroundLayer))
        {

            PlayerMovement.IsGround = true;


        }
        else
        {

            PlayerMovement.IsGround = false;
        }

    }
}
