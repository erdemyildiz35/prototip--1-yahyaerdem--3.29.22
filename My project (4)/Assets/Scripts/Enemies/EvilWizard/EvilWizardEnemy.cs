using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EvilWizardEnemy : MonoBehaviour
{

    public AIPath aiPath;
    Animator animator;
    public Transform attackPoint;
    private bool isMoving = false;
    private bool isAttacking = false;
    public float SaldiriArasiBeklemeSuresi = 2;
    Enemy enemy;

    [SerializeField] LayerMask HeroLayer;

    private void Start()
    {
       enemy = GetComponent<Enemy>();
       animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(enemy.isDead)
        {
            aiPath.canMove = false;
            StopAllCoroutines();
            return;
        }

        if(enemy.isTakingDamage)
        {
            aiPath.canMove = false;
        }
        else
        {
            aiPath.canMove = true;
        }

        if (Mathf.Abs(aiPath.desiredVelocity.x) > 0 && !isAttacking && !enemy.isDead) // Varýþ yerine ulaþmadýysa
        {
            Moving();
        }

        else
        {
            NotMoving();
        }
        
        if(aiPath.desiredVelocity.x >= 0.01f) // Varýþ yeri saðda ise
        {
            enemy.FlipRight();
        }

        else if(aiPath.desiredVelocity.x <= -0.01f) // Varýþ yeri solda ise
        {
            enemy.FlipLeft();
        }

        if(aiPath.reachedDestination) // Varýþ yerine ulaþtýysa
        {
            if(!isAttacking && !enemy.isTakingDamage)
            {
                Attack();
            }
            else
            {
                DealDamage();
            }

        }
    }

    private void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1.6f, HeroLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HeroHealth>().TakeDamage(0.1f, 1);
        }
    }


    void Moving()
    {
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
    }

    void NotMoving()
    {
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
    }

    void Attack()
    {
        isAttacking = true;
        StartCoroutine(Attack1());
    }

    
    IEnumerator Attack1()
    {
        
        
        aiPath.canMove = false;
        animator.SetBool("isAttacking", isAttacking);
        yield return new WaitForSeconds(SaldiriArasiBeklemeSuresi);
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        aiPath.canMove = true;
        
    }

    
}
