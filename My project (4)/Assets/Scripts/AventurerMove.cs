﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AventurerMove : MonoBehaviour
{
    Animator AnimatorAdventurer;
    Rigidbody2D rb;
    CapsuleCollider2D colider;


    //Başlangıç Boyutu
    Vector3 DefaultLocalScale;
    Vector2 crouchSize;
    Vector2 crouchOffset;

    //Checkerlar
    public bool IsGround;
    public bool isAttacking;
    public bool isDash = false;
    public bool isCrouch = false;

    //speed
    public float MySpeedX;
    [SerializeField] float Speed;
    [SerializeField] float DashSpeed;
    [SerializeField] float SuperSpeed;
    float TempSpeed;

    //JumpForce
    [SerializeField] float JumpForce = 3f;

    //attack
    float AttackDamage;
    [SerializeField] Transform SwordAttackPoint;
    [SerializeField] Transform HandAttackPoint;
    Collider2D[] hitEnemies;

    //SwordOrNot
    private bool HandOrSword = false;

    //Layermask
    [SerializeField] LayerMask EnemyLayer;


    void Start()
    {
        AnimatorAdventurer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();

        DefaultLocalScale = transform.localScale;
        TempSpeed = Speed;



    }

    private void Awake()
    {
     
    }

    void Update()
    {
        KeyInputs();
        Movement();
        AnimationControl();
    }

    void Movement()
    {
        rb.velocity = new Vector2((MySpeedX * Speed), rb.velocity.y);

        if (MySpeedX > 0)
        {
            transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
        }
        else if (MySpeedX < 0)
        {
            transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
        }
    }

    void AnimationControl()
    {
        //Animasyon koşuş Kontrolü
        if (IsGround && Mathf.Abs(MySpeedX) > .1)
        {
            AnimatorAdventurer.SetBool("Running", true);
        }
        else
        {
            AnimatorAdventurer.SetBool("Running", false);
        }

        //Düşüş Kontrol
        if (IsGround)
        {
            AnimatorAdventurer.SetBool("IsGround", true);
        }
        else
        {
            AnimatorAdventurer.SetBool("IsGround", false);
        }

        //Fall Kontrol
        if (!IsGround && rb.velocity.y < 0 && !isAttacking)
        {
            AnimatorAdventurer.Play("Fall");
        }

        //Crouch Kontrol
        if (!Input.GetKey(KeyCode.LeftControl) && AnimatorAdventurer.GetBool("Crouch"))
        {
            Speed = TempSpeed;
            AnimatorAdventurer.SetBool("Crouch", false);

            if (isCrouch)
            {
                colider.offset = new Vector2(colider.offset.x, colider.offset.y + 0.40f);
                colider.size = new Vector2(colider.size.x, colider.size.y * 2);
                isCrouch = false;
            }


        }

        //Fast Run Kontrol
        if (!Input.GetKey(KeyCode.LeftShift) && AnimatorAdventurer.GetBool("FastRun"))
        {
            if (!HandOrSword)
            {
                Speed = TempSpeed;
                AnimatorAdventurer.SetBool("FastRun", false);


            }
        }
    }

    void KeyInputs()
    {
        //Hareket ve hız
        MySpeedX = Input.GetAxis("Horizontal");

        //Zıplama
        if (Input.GetKey(KeyCode.Space) && IsGround && !isAttacking && !isCrouch)
        {
            Jump();
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && !isCrouch)
        {
            Attack(KeyCode.Z);
        }

        if (Input.GetKeyDown(KeyCode.X) && !isAttacking && !isCrouch)
        {
            Attack(KeyCode.X);
        }

        //Dash ve FastRun
        if (Input.GetKey(KeyCode.LeftShift) && IsGround && !isAttacking)
        {
            if (HandOrSword)
            {
                Dash();
            }
            else
            {
                FastRun();
            }
        }

        //Crouch
        if (Input.GetKey(KeyCode.LeftControl) && IsGround)
        {
            Crouch();
        }

        //Pıçak Çekme
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwordOnOff();
        }
    }

    void SwordOnOff()
    {
        StartCoroutine(SlowDown(.1f));

        if (HandOrSword)
        {
            AnimatorAdventurer.Play("SwordShte");
            AnimatorAdventurer.SetBool("SwordOn", false);
            HandOrSword = false;
        }
        else
        {
            AnimatorAdventurer.Play("SwordDraw");
            AnimatorAdventurer.SetBool("SwordOn", true);
            HandOrSword = true;
        }
    }

    void Crouch()
    {
        AnimatorAdventurer.SetBool("Crouch", true);
        Speed = TempSpeed / 2;
        if (!isCrouch)
        {
            colider.size = new Vector2(colider.size.x, colider.size.y / 2);
            colider.offset = new Vector2(colider.offset.x, colider.offset.y - 0.40f);

            isCrouch = true;
        }

        if (Mathf.Abs(MySpeedX) > .1f)
        {
            AnimatorAdventurer.SetBool("CrouchWalk", true);
        }
        else
        {
            AnimatorAdventurer.Play("Crouch");
            AnimatorAdventurer.SetBool("CrouchWalk", false);
        }
    }

    void Attack(KeyCode key)
    {
        TempSpeed = Speed;
        //havada yumruk animasyonu olmadığı için hata veriyordu o yüzden koşul ekledim
        if (!HandOrSword && key == KeyCode.X && !IsGround || !IsGround) { }
        else
        {
            Speed *= 0.1f;
        }
        if (!HandOrSword && key == KeyCode.X && !IsGround) { }
        else
        {
            isAttacking = true;
        }

        if (HandOrSword)
        {
            if (key == KeyCode.Z)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Attack1");
                    AttackDamage = 10;
                }
                else
                {
                    AnimatorAdventurer.Play("AirAttack");
                    AttackDamage = 10;
                    Debug.Log("AirAttack");
                }
            }
            else if (key == KeyCode.X)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Attack2");
                    AttackDamage = 10;
                }
                else
                {
                    AnimatorAdventurer.Play("AirAttack2");
                    AttackDamage = 10;
                    Debug.Log("AirAttack2");
                }
            }

            hitEnemies = Physics2D.OverlapCircleAll(SwordAttackPoint.position, .5f, EnemyLayer);
        }
        else if (!HandOrSword)
        {
            if (key == KeyCode.Z)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Kick");
                    AttackDamage = 10;
                }
                else if (!IsGround)
                {
                    AnimatorAdventurer.Play("DropKick");
                    AttackDamage = 10;
                    Debug.Log("DropKick");
                }
                else if (IsGround && AnimatorAdventurer.GetBool("FastRun") == true)
                {
                    AnimatorAdventurer.Play("Slide");
                    AttackDamage = 10;
                }
            }
            else if (key == KeyCode.X && IsGround)
            {
                if (AnimatorAdventurer.GetBool("FastRun") == false)
                {
                    AnimatorAdventurer.Play("Punch");
                    AttackDamage = 10;
                }
                else
                {
                    AnimatorAdventurer.Play("RunPunch");
                    AttackDamage = 10;
                }
            }
            hitEnemies = Physics2D.OverlapCircleAll(HandAttackPoint.position, .5f, EnemyLayer);
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Enemy>().isDead)
            {
                enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }
        }
    }

    void Dash()
    {
        if (!isDash)
        {
            AnimatorAdventurer.Play("smrlt");
            StartCoroutine(DashIE());
        }
    }

    void FastRun()
    {
        Speed = SuperSpeed;
        AnimatorAdventurer.Play("Run2");
        AnimatorAdventurer.SetBool("FastRun", true);

    }

    void Jump()
    {
        AnimatorAdventurer.Play("Jump");
    }


    void AllertObserver(string message)
    {
        if (message == "Jump")
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        if (message == "AttackEnd")
        {
            isAttacking = false;
            Speed = TempSpeed;
        }
    }


    IEnumerator SlowDown(float WaitTime)
    {
        Speed *= .1f;

        yield return new WaitForSeconds(WaitTime);

        Speed *= 10;
    }

    IEnumerator DashIE()
    {
        isDash = true;
        Speed += DashSpeed;

        yield return new WaitForSeconds(.3f);

        AnimatorAdventurer.SetBool("IsDash", false);
        Speed -= DashSpeed;

        yield return new WaitForSeconds(.4f);

        isDash = false;
    }

}
