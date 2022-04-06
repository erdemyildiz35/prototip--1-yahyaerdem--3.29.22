using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mushroom : MonoBehaviour
{
    AdventurerHealth Adventurer;
    Animator animator;
    Rigidbody2D Rb;
    Vector3 LocalScale;
    [SerializeField] float Speed=3f;

    [SerializeField] float AttackDistance;
    [SerializeField] float HitDistance;

    [SerializeField] float AttackTimer = 0;

    private bool AbleTorun=true;

    private void Start()
    {
        Adventurer = FindObjectOfType<AdventurerHealth>();
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LocalScale = transform.localScale;


    }


    private void Update()
    {
        AttackTimer += Time.deltaTime*5;

        if (AbleTorun)
        {
            Movement();
        }

    }

    void Attack()
    {
        if (AttackTimer > 20)
        {
            AttackTimer = 0;
            animator.Play("Attack");
            StartCoroutine(AbleTorunAgain());
        }
    }
    public void DealDamage()
    {
        var vectorToTarget = Adventurer.transform.position - transform.position;
        vectorToTarget.y = 0;
        var distanceToTarget = vectorToTarget.magnitude;

        if (distanceToTarget < HitDistance)
            Adventurer.TakeDamage(15);




    }



    private void FlipRight()
    {
        transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
    }

    private void FlipLeft()
    {
        transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
    }

    private void Movement()
    {

        if (Adventurer.transform.position.x - transform.position.x > 0)
        {
            FlipRight();
        }
        else
        {
            FlipLeft();
        }

        
        {
            var vectorToTarget = Adventurer.transform.position - transform.position;
            vectorToTarget.y = 0;
            var distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget <= AttackDistance && distanceToTarget >= HitDistance)
            {
                Vector2 target = new Vector2(Adventurer.transform.position.x, Rb.position.y);

                Vector2 MovePos = Vector2.MoveTowards(Rb.position, target, Speed * Time.fixedDeltaTime);
                Rb.MovePosition(MovePos);
                animator.SetBool("runing", true);
            }
            else if (distanceToTarget < HitDistance)
            {
                animator.SetBool("runing", false);
                Attack();
                Debug.Log("vurması gerek");

            }

            else
            {


                animator.SetBool("runing", false);


            }
        }





    }


    IEnumerator AbleTorunAgain()
    {

        AbleTorun = false;

        yield return new WaitForSeconds(3f);
        AbleTorun = true;

    }

}
