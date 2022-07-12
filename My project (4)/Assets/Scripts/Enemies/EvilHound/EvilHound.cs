using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHound : MonoBehaviour
{

    //compenent ve oyun içerikleri
    Animator AnimatorBoss;
    Rigidbody2D Rb2d;
    AdventurerHealth Adventurer;

    [Header("Attiributes")]
   [SerializeField] public Transform AttackPos;
    [SerializeField] public int Damage;
    [SerializeField] public float WaitTime;
    [SerializeField] public float RangeOfPlayer;
    [SerializeField] public float AttackRangePlayer;
    [SerializeField] public float HitDamageRange;
    [SerializeField] public float Speed;
    [SerializeField] public LayerMask PlayerLayer;
    [SerializeField] public float DashAttackSpeed;

    Vector3 DefaultLocalScale;
    bool isRight;
    [SerializeField] bool İsattacking = false;
    Vector2 MovePlayer;

    void Start()
    {
        DefaultLocalScale = transform.localScale;
        Adventurer = FindObjectOfType<AdventurerHealth>();
        AnimatorBoss = GetComponent<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
     

    }


    void Update()
    {

        Vector2 target = new Vector2(Adventurer.transform.position.x, Rb2d.position.y);
        MovePlayer = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);

        if (Adventurer.transform.position.x <= transform.position.x)
        {
            isRight = true;

        }
        else if (Adventurer.transform.position.x > transform.position.x)
        {
            isRight = false;

        }


        if (Physics2D.OverlapCircle(transform.position, RangeOfPlayer * 2, PlayerLayer) && Mathf.Abs((100 - transform.position.y) - (100 - Adventurer.transform.position.y)) <= 20 && !İsattacking)
        {
            AnimatorBoss.SetBool("Run", true);




            Rb2d.MovePosition(MovePlayer);

            if (isRight && !İsattacking)
            {
                transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            }

            if (!isRight && !İsattacking)
            {
                transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);

            }

            if (Vector2.Distance(AttackPos.position, Adventurer.transform.position) < AttackRangePlayer)
            {
                Attack();
            }
        }

    }


    void Attack()
    {

        AnimatorBoss.Play("Attack");
        StartCoroutine(yieldAttackWaittime());

    }

    public void Attackend()
    {


        if (Vector2.Distance(AttackPos.position, Adventurer.transform.position) < HitDamageRange)
        {
            Adventurer.TakeDamage(Damage);

        }

   
        İsattacking = false;
    }


    IEnumerator yieldAttackWaittime()
    {

        İsattacking = true;

        if (isRight)
        {

            gameObject.layer = 11;
            Rb2d.velocity = Vector2.right * -DashAttackSpeed;

        }
        else
        {
            gameObject.layer = 11;
            Rb2d.velocity = Vector2.right * -DashAttackSpeed;

        }
        AnimatorBoss.Play("idle");

        yield return new WaitForSeconds(2f);

        gameObject.layer = 8;
        İsattacking = false;
    }



}
