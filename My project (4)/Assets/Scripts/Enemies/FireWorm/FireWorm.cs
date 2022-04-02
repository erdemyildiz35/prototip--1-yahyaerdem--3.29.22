using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorm : MonoBehaviour
{

    AventurerMove Hero;


  [SerializeField]  GameObject FireWormBall;//wormun attığı ateşkusmuğu

    [SerializeField] Transform WormPoint;

    Vector3 LocalScale;

   [SerializeField] float AttackSpeed = 2f;
    private bool CanAttack=true;
    Animator animator;
    Rigidbody2D Rb;

    //attack
    [SerializeField] float attackSıklık = 5;
    float timer = 0;
   [SerializeField] int counter=0;
    bool OnWait = false;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Hero = FindObjectOfType<AventurerMove>();

        LocalScale = transform.localScale;

    }

 
    void Update()
    {
        if(GetComponent<Enemy>().isDead)
        {

            return;
        }

        timer++;

        WormBall();
        if (Hero.transform.position.x - transform.position.x > 0)
        {
            FlipRight();
        }
        else
        {
            FlipLeft();
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


    void WormBall()
    {
        if (Vector2.Distance(transform.position, Hero.transform.position) <= 25f)
        {


            if (Vector2.Distance(transform.position, Hero.transform.position) <= 15f)
            {
                if (CanAttack)
                {

                    if (counter < 10)
                    {
                        animator.Play("Attack1");

                    }else if(!OnWait)
                    {
                        animator.Play("idle");

                        StartCoroutine(WaitTimer());

                    }
                     

                        
                    
                  
                }
            }else if (Vector2.Distance(transform.position, Hero.transform.position) > 15f)
            {

                Vector2 MovePos = Vector2.MoveTowards(Rb.position, Hero.transform.position, 1.2f * Time.fixedDeltaTime);
                Rb.MovePosition(MovePos);
                animator.SetBool("runing", true);
            }
        }
    }

    IEnumerator ThrowBall()
    {

        counter++;

        if (Vector2.Distance(Hero.transform.position, transform.position) < 2f)
        {

            StartCoroutine(CloseAttack());


        }
        else
        {

            Instantiate(FireWormBall, WormPoint.transform.position, Quaternion.identity);


            yield return new WaitForSeconds(AttackSpeed);

            CanAttack = true;
        }



    }


    IEnumerator CloseAttack()
    {
        yield return new WaitForSeconds(.3f);
        
              if (Vector2.Distance(Hero.transform.position, transform.position) < 7f)
                {
                    //heroya damage vericez
                }
    }

    IEnumerator WaitTimer()
    {
        OnWait = true;
        yield return new WaitForSeconds(4f);
        counter = 0;
        OnWait = false;



    }


     public void AllertObservers(string message)
    {
        if (message == "Attack1" )
        {
            StartCoroutine(ThrowBall());
            

        }     
    }




}
