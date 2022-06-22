using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform tippoint;

    BoxCollider2D Colider;
    private void Awake()
    {
        Colider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Physics2D.OverlapCircle(tippoint.position, .3f, PlayerLayer))
        {
            StartCoroutine(triggerTheArrow());

            Debug.Log("buldu arrowuu");
          
        }
        
    }

    IEnumerator triggerTheArrow()
    {


        yield return new WaitForSeconds(.1f);
        Colider.isTrigger = true;
    }
}
