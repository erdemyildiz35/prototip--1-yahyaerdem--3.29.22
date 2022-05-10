using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D collider;
    public Vector2 MaxY, MinY;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        collider.enabled = false;

        StartCoroutine(Fire());

    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            animator.Play("Fire", -1, 0);
        }
    }

    void AllertObservers(string message)
    {
        if (message == "FireStart")
        {
            collider.enabled = true;
        }

        if (message == "FireEnd")
        {
            collider.enabled = false;
        }
    }
}
