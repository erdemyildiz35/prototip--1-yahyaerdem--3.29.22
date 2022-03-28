using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Heromovement Hero;
    Animator animator;
    Rigidbody2D Rb;
    Enemy Enemy;
    CapsuleCollider2D boxcollider;
    [SerializeField] LayerMask GroundLayer;
 
    [SerializeField] bool IsGrounded = true;
    [SerializeField] LayerMask HeroLayer;
    [SerializeField] float Damage = 10;


    [SerializeField] float Speed;
    private bool isAttacking = false;
    private float SaldiriArasiBeklemeSuresi = 3;
    private bool isWaiting;




    float BossMana;

    Vector3 LocalScale;


    void Start()
    {
        animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Hero = FindObjectOfType<Heromovement>();
        boxcollider = GetComponent<CapsuleCollider2D>();

        LocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {


        Grounded();


        Movement();


        
    }


    void Grounded()
    {
        float extraHeight = .15f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider.bounds.center, Vector2.down, boxcollider.bounds.extents.y + extraHeight, GroundLayer);
        Color rayColor;

        if (raycastHit.collider != null)
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


        if (IsGrounded == false)
        {
            animator.Play("Fall");
        }
        Debug.DrawRay(boxcollider.bounds.center, Vector2.down * (boxcollider.bounds.extents.y + extraHeight));

    }

    void Movement()
    {


        if (Hero.transform.position.x - transform.position.x > 0)
        {
            FlipRight();
        }
        else
        {
           FlipLeft();
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
            else if(distanceToTarget <3f)
            {
                animator.SetBool("runing", false);
                Attack();


            }

            else
            {


                //idle


            }
        }



    }


    private void FlipRight()
    {
        transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
    }

    private void FlipLeft()
    {
        transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
    }


    void Attack()
    {
        if (!isAttacking && !isWaiting)
        {
            isAttacking = true;
            StartCoroutine(Attack1());
            StartCoroutine(AttacTime());
        }

    }


    IEnumerator Attack1()
    {
        animator.Play("Attack2");

        yield return new WaitForSeconds(SaldiriArasiBeklemeSuresi);

        isAttacking = false;

    }


    void DealDamage()
    {
        StartCoroutine(AttacTime());
       
    }

    IEnumerator AttacTime()
    {

        //vurduktan sonra burdamı hala 


        yield return new WaitForSeconds(.3f);

        if (Vector2.Distance(Hero.transform.position, transform.position) < 4f)
        {
            Hero.GetComponent<HeroHealth>().TakeDamage(Damage, 0);
        }
    }


}
