using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer : MonoBehaviour
{
    [Header("Değerler")]
    [SerializeField] Transform AttackPoint;
    [SerializeField] float radius;
    [SerializeField] LayerMask Playerlayer;
    [SerializeField] float RangeOfPlayer, Speed, Damage, HitDamageRange;
   
    
    Rigidbody2D rb;
    AdventurerHealth aventurer;
    Animator Animator;

    bool isRight;
    Vector2 moveplayer;

    void Start()
    {
        aventurer = FindObjectOfType<AdventurerHealth>();
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(aventurer.transform.position.x, rb.position.y);
        moveplayer = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);

        if (aventurer.transform.position.x <= transform.position.x)
        {
            isRight = true;

        }
        else if (aventurer.transform.position.x > transform.position.x)
        {
            isRight = false;

        }


        if (Physics2D.OverlapCircle(transform.position, RangeOfPlayer, Playerlayer) && Mathf.Abs((100 - transform.position.y) - (100 - aventurer.transform.position.y)) <= 20)
        {

            Animator.Play("SuperSkilll");
        }


    }



    public void İshit()
    {

        if (Vector2.Distance(AttackPoint.position, aventurer.transform.position) < HitDamageRange)
        {
            aventurer.TakeDamage(Damage);

        }


    }

    public void EndOfTask()
    {


        Destroy(this.gameObject);
    }
}


