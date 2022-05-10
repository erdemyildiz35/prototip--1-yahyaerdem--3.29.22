using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void AllertObservers(string message)
    {
        if (message == "ColliderDestroy")
        {
            Destroy(collider);
        }
        if (message == "ObjectDestroy")
        {
            gameObject.SetActive(false);
        }
    }
}
