using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AventurerMove : MonoBehaviour
{
    Animator AnimatorAdventurer;
    Rigidbody2D rb;



    //Başlangıç Boyutu
    Vector3 DefaultLocalScale;

    //Checkerlar
    public bool IsGround;
    private bool isDash=false;



    //speed
    float MySpeedX;
    [SerializeField] float Speed;
    [SerializeField] float DashSpeed;
    [SerializeField] float SuperSpeed;
    float TempSpeed;


    //JumpForce
    [SerializeField] float JumpForce = 3f;



    


    //SwordOrNot
    private bool HandOrSword = false;



    void Start()
    {
        AnimatorAdventurer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        DefaultLocalScale = transform.localScale;
        TempSpeed = Speed;

    }


    void Update()
    {

        KeyInputs();
        Movement();
        AnimationControl();
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

        //düşüş Kontrol

        if (IsGround)
        {
            AnimatorAdventurer.SetBool("IsGround", true);

        }
        else
        {
            AnimatorAdventurer.SetBool("IsGround", false);

        }

        //DÜŞÜŞ KONTROLCÜSÜ
        if (!IsGround && rb.velocity.y < 0)
        {
            AnimatorAdventurer.Play("Fall");

        }

      


    }


    void SwordOnOff()
    {
        StartCoroutine(SlowDown(.1f));
        if (HandOrSword)
        {

            AnimatorAdventurer.Play("SwordShte");
            HandOrSword = false;
        }
        else
        {
            AnimatorAdventurer.Play("SwordDraw");
            HandOrSword = true;
        }
    }



  




    void KeyInputs()
    {
        //speed alan kısım
        MySpeedX = Input.GetAxis("Horizontal");


        //Jump aldığı kesim
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();


        }

        //Attack



        // dash and FastRun
        if (Input.GetKey(KeyCode.LeftShift))
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

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {


            if (!HandOrSword)
            {

                Speed = TempSpeed;
                AnimatorAdventurer.SetBool("FastRun", false);
            }

        }

        //Crouch
        if (Input.GetKey(KeyCode.LeftControl)&&IsGround)
        {
         
            AnimatorAdventurer.SetBool("Crouch", true);
            Speed =TempSpeed/2;

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
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            Speed = TempSpeed;
            AnimatorAdventurer.SetBool("Crouch", false);

        }


            //pıçakçekme

            if (Input.GetKeyDown(KeyCode.F))
        {
            SwordOnOff();
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



    void AllertObserver(string message)
    {


        if (message == "Jump")
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpForce);

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
