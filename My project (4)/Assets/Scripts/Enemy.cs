using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float Health = 100;
    Animator animator;
    private bool isBurning = false;
    public bool isDead = false;
    private bool isFlying = false;
    public bool isTakingDamage = false;

    private Vector3 Scale;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Scale = transform.localScale;

    }
    
    public void FlipRight()
    {
        transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
    }

    public void FlipLeft()
    {
        transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
    }

    public void TakeDamage(float Damage)
    {
       if(!isDead)
        {
            if (!isTakingDamage)
            {
                StartCoroutine(TakeDamageIE(Damage));
            }
            else
            {
                animator.Play("TakeHit");
                Health -= Damage;
                if (Health <= 0)
                {
                    Die();
                }
            }
        }
      
       

    }

   
    IEnumerator TakeDamageIE(float Damage)
    {
        isTakingDamage = true;
        animator.Play("TakeHit");
        Health -= Damage;
        if (Health <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(2);
        isTakingDamage = false;
        
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            gameObject.layer = 10;
            if (GetComponent<Rigidbody2D>()) { GetComponent<Rigidbody2D>().isKinematic = false;}
            if (GetComponent<CapsuleCollider2D>()) { GetComponent<CapsuleCollider2D>().isTrigger = false; }

            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        animator.SetTrigger("Die");
        
        yield return new WaitForSeconds(2);
        Destroy(gameObject,4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
         // TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Tornado")
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Lava")
        {
            TakeDamage(100);
        }
    }

}
