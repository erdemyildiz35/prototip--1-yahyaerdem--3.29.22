using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2MpvementPattern : MonoBehaviour
{

    //compenent ve oyun içerikleri
    Animator AnimatorBoss;
    Rigidbody2D Rb2d;
    AdventurerHealth Adventurer;

    [Header("Attiributes")]
    public Transform AttackPos;
    public int Damage;
    public float WaitTime;
    public float RangeOfPlayer;
    public float AttackRangePlayer;
    public float HitDamageRange;
    public float Speed;
    public float DashAttackSpeed = 10f;
    public LayerMask PlayerLayer;
    public GameObject AttackParticle;

    Vector3 DefaultLocalScale;
    bool isRight;
  [SerializeField]  bool İsattacking=false;
    Vector2 MovePlayer;
    [SerializeField] int attackcounter = 0;
    bool attackController = true;
    void Awake()
    {
        DefaultLocalScale = transform.localScale;
        Adventurer = FindObjectOfType<AdventurerHealth>();
        AnimatorBoss = GetComponent<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
        AttackParticle.SetActive(false);

    }

  
    void FixedUpdate()
    {

      

        if (Adventurer.transform.position.x <= transform.position.x)
        {
            isRight = true;
           
        }
        else if (Adventurer.transform.position.x > transform.position.x)
        {
            isRight = false;
            
        }


        if (Physics2D.OverlapCircle(transform.position, RangeOfPlayer*2, PlayerLayer)&& Mathf.Abs((100- transform.position.y)-(100-Adventurer.transform.position.y))<=2f && !İsattacking)
        {
            AnimatorBoss.SetBool("Run", true);
            Vector2 target = new Vector2(Adventurer.transform.position.x, Rb2d.position.y);
            MovePlayer = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);
            Rb2d.MovePosition(MovePlayer);




            if (isRight&& !İsattacking)
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
        İsattacking = true;
        AnimatorBoss.Play("Attack");

        StartCoroutine(yieldAttackWaittime());

     }

    public void Attackend()
    {
  

        if (Vector2.Distance(AttackPos.position,Adventurer.transform.position)< HitDamageRange)
        {
            Adventurer.TakeDamage(Damage);

        }

        AttackParticle.SetActive(false);
        İsattacking = false;
    }

   
    IEnumerator yieldAttackWaittime()
    {
        if (attackController)
        {
            attackController = false;

            İsattacking = true;
            AttackParticle.SetActive(true);

            if (attackcounter == 3)
            {
                attackcounter = 0;

                if (isRight)
                {
                    gameObject.layer = 11;
                    Rb2d.velocity = Vector2.right * -DashAttackSpeed;

                }
                else
                {
                    gameObject.layer = 11;
                    Rb2d.velocity = Vector2.right * DashAttackSpeed;
                }
            }



            gameObject.layer = 8;
            AttackParticle.SetActive(false);
            İsattacking = true;
            attackcounter++;
            yield return new WaitForSeconds(2f);
            İsattacking = false;
            attackController = true;
        }
    }



}
