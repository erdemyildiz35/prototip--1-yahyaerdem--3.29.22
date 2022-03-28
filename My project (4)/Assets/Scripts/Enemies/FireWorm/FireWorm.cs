using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorm : MonoBehaviour
{

    Heromovement Hero;


  [SerializeField]  GameObject FireWormBall;//wormun attığı ateşkusmuğu

    [SerializeField] Transform WormPoint;

    Vector3 LocalScale;

   [SerializeField] float AttackSpeed = 2f;
    private bool CanAttack=true;
    Animator animator;
    Rigidbody2D Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Hero = FindObjectOfType<Heromovement>();

        LocalScale = transform.localScale;

    }

 
    void Update()
    {
        if(GetComponent<Enemy>().isDead)
        {
            return;
        }

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
                    animator.Play("Attack");
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

      

        if (Vector2.Distance(Hero.transform.position, transform.position) < 7f)
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
                    Hero.GetComponent<HeroHealth>().TakeDamage(10, 0);
                }
    }


     public void AllertObservers(string message)
    {
        if (message == "Attack" )
        {
            StartCoroutine(ThrowBall());
        }     
    }
}
