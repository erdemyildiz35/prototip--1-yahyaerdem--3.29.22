using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTraps2 : MonoBehaviour
{
    //componentler

    Rigidbody2D RbOfSaws;

    [SerializeField] float Speed;
    bool Rightleft = false;

    void Start()
    {
        RbOfSaws = GetComponent<Rigidbody2D>();
        RbOfSaws.velocity = new Vector2(Speed, 0);

    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {
          

            if (Rightleft)
            {
                RbOfSaws.velocity = new Vector2(Speed, 0);
                Rightleft = false;
            }
            else
            {
                RbOfSaws.velocity = new Vector2(-Speed, 0);
                Rightleft = true;
            }

        }
    }
}
