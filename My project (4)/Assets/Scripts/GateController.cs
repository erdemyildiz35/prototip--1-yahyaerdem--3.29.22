using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public CapsuleCollider2D Gate1Collider, Gate2Collider;
    bool canGate1Event, canGate2Event;
    [SerializeField] LayerMask GateLayer;
    public GameObject Hero;

    // Start is called before the first frame update
    void Start()
    {
        Gate1Collider = GameObject.Find("Gate1").GetComponent<CapsuleCollider2D>();
        Gate2Collider = GameObject.Find("Gate2").GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == Gate1Collider)
        {
            canGate1Event = true;
        }
        else if (collision == Gate2Collider)
        {
            canGate2Event = true;
        }
    }

    public void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canGate1Event)
            {
                Gate1Event();
            }

            else if (canGate2Event)
            {
                Gate2Event();
            }
        }
    }

    public void Gate1Event()
    {
        //Gate1 iþlemleri

        canGate1Event = false;
    }

    public void Gate2Event()
    {
        //Gate2 iþlemleri

        canGate2Event = false;
    }
}
