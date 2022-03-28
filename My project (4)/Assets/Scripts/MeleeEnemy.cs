using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    Heromovement Hero;
    Animator animator;
    Rigidbody2D Rb;
    Enemy Enemy;
    CapsuleCollider2D boxcollider;
    [SerializeField] LayerMask GroundLayer;
    public Transform attackPoint;
    [SerializeField] bool IsGrounded  = true;
    [SerializeField] LayerMask HeroLayer;
    [SerializeField] float Damage = 10;


    [SerializeField] float Speed;
    private bool isAttacking = false;
    private float SaldiriArasiBeklemeSuresi = 3;
    private bool isWaiting;

    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        Hero = FindObjectOfType<Heromovement>();
        Rb = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enemy.isDead && !Enemy.isTakingDamage)
        {
            Grounded();
            Movement();
        }
            
    }

    void Grounded()
    {
        float extraHeight = .05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider.bounds.center, Vector2.down, boxcollider.bounds.extents.y + extraHeight*2f, GroundLayer);
        Color rayColor;
        
        if(raycastHit.collider != null)
        {
            if (raycastHit.collider.tag == "Ground")
            {
                rayColor = Color.green;
                IsGrounded = true;
            }
            else
            {
                rayColor = Color.red;
                IsGrounded = false;
            }
        }
        else
        {
            rayColor = Color.red;
            IsGrounded = false;
        }


        if(IsGrounded == false)
        {
            animator.Play("Fall");
        }
        Debug.DrawRay(boxcollider.bounds.center, Vector2.down *2f* (boxcollider.bounds.extents.y + extraHeight));

    }



    void Movement()
    {
        

        if (Hero.transform.position.x - transform.position.x > 0)
        {
            Enemy.FlipRight();
        }
        else
        {
            Enemy.FlipLeft();
        }

        if (IsGrounded)
        {
            var vectorToTarget = Hero.transform.position - transform.position;
            vectorToTarget.y = 0;
            var distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget <= 15f && distanceToTarget >= 3f)
            {
                Vector2 target = new Vector2(Hero.transform.position.x, Rb.position.y);

                Vector2 MovePos = Vector2.MoveTowards(Rb.position, target, Speed * Time.fixedDeltaTime);
                Rb.MovePosition(MovePos);
                animator.SetBool("runing", true);
            }
            else
            {
                animator.SetBool("runing", false);
                Attack();
                
                
            }
        }
       

        
    }




    void Attack()
    {
        if(!isAttacking && !isWaiting)
        {
            isAttacking = true;
            StartCoroutine(Attack1());
        }
        
    }


    IEnumerator Attack1()
    {
        animator.SetTrigger("Attack");
        
        yield return new WaitForSeconds(SaldiriArasiBeklemeSuresi);
        
        isAttacking = false;
        
    }


    void DealDamage()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1.6f, HeroLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HeroHealth>().TakeDamage(Damage, 0);
        }
    }

}
