using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    Heromovement Hero;
    [SerializeField] LayerMask Layer;
    

    private void Start()
    {
        Hero = FindObjectOfType<Heromovement>();
    }
    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, .15f, Layer))
        {
            Hero.IsGround = true;
        }
        else
        {
            Hero.IsGround = false;
        }


    }

}
