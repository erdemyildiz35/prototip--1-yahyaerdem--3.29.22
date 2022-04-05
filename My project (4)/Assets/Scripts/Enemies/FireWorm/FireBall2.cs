using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall2 : MonoBehaviour
{
    AdventurerHealth adventurer;
    GameObject Hero;
    [SerializeField] float Speed;
    Rigidbody2D Rb;
    Vector2 Target;
    Animator animator;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Hero = GameObject.Find("HeroPos");
        animator = GetComponent<Animator>();
        Target = ((Hero.transform.position + Vector3.down) - transform.position).normalized;
        Vector2 direction = Hero.transform.position - transform.position; 
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Rb.velocity = new Vector2(Target.x, Target.y) * Speed;
        Destroy(this.gameObject, 4f);
        adventurer = FindObjectOfType<AdventurerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            adventurer.TakeDamage(10);
            Rb.bodyType = RigidbodyType2D.Static;
            animator.Play("Explosion");
        }

        if (collision.gameObject.tag == "Ground") {

            Rb.bodyType = RigidbodyType2D.Static;
            animator.Play("Explosion");
        }
    }



    void AllertObservers(string message)
    {
        if(message == "DestroyFireBall")
        {
            Destroy(this.gameObject);
        }
    }

}
