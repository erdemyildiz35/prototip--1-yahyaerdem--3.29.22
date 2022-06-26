using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mushroom : MonoBehaviour
{
    AdventurerHealth Adventurer;
    AventurerMove AdventurerMove;
    Animator animator;
    Rigidbody2D Rb;
    Vector3 LocalScale;
    [SerializeField] float Speed=3f;

    [SerializeField] float AttackDistance;
    [SerializeField] float HitDistance;

    [SerializeField] float AttackTimer = 0;

    private bool AbleTorun=true;

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float YDegeri;
    private void Start()
    {
        AdventurerMove = FindObjectOfType<AventurerMove>();
        Adventurer = FindObjectOfType<AdventurerHealth>();
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LocalScale = transform.localScale;


    }


    private void FixedUpdate()
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
           
            var distanceToTarget = vectorToTarget.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * transform.localScale.x / 2), Vector2.down, 1f, GroundLayer);

            if (!hit)
            {

                if (distanceToTarget <= AttackDistance && distanceToTarget >= HitDistance && !AdventurerMove.Hide && Mathf.Abs((100-transform.position.y) - (100-Adventurer.transform.position.y)) <= YDegeri)
                {
                    Vector2 target = new Vector2(Adventurer.transform.position.x, Rb.position.y);

                    Vector2 MovePos = Vector2.MoveTowards(Rb.position, target, Speed * Time.fixedDeltaTime);
                    Rb.MovePosition(MovePos);
                    animator.SetBool("runing", true);
                }
                else if (distanceToTarget < HitDistance && Mathf.Abs(transform.position.y - Adventurer.transform.position.y) <= 3)
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





    }


    IEnumerator AbleTorunAgain()
    {

        AbleTorun = false;

        yield return new WaitForSeconds(2f);
        AbleTorun = true;

    }

}
