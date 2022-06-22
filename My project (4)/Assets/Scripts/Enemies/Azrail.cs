using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azrail : MonoBehaviour
{
    AdventurerHealth Adventurer;
    Rigidbody2D rbofAzazel;
    [SerializeField] float Speed=3f;
    BoxCollider2D Collider;

    private void Awake()
    {
        rbofAzazel = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        Adventurer = FindObjectOfType<AdventurerHealth>();

        if (Adventurer.transform.position.x <= transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rbofAzazel.velocity = new Vector2(-Speed, 0);

        }
        else if (Adventurer.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            rbofAzazel.velocity = new Vector2(Speed, 0);
        }

    }

    private void Update()
    {
 
    }
  public  void TriggerTheCollider()
    {

        Collider.isTrigger = true;

    }


    public void DestroyThis() {


        Destroy(this.gameObject);
    
    }
}
