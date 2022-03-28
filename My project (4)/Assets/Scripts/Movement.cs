using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Hız
     float MySpeedX ;
    [SerializeField] float Speed = 1;


    //Zıplama
    public bool Onground = true;
    [SerializeField] float JumpForce=3f;
    [SerializeField] private bool CanJump=true;


    //compenler
    Rigidbody2D rb;
    Animator Animator;
    BoxCollider2D colider;

    //saldırı
    private bool Attack;
    private bool Attack2;
    private bool CanAttack=true;

    //crouch
    private bool Crouch;
   



    Vector3 DefaultLocalScale;


    // Start is called before the first frame update
    void Start()
    {
        //oyun başladığında tanımlanması gerekenler
        DefaultLocalScale = transform.localScale;



        //compen alma kısmı
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        colider = GetComponent<BoxCollider2D>();



    }

    // Update is called once per frame
    void Update()
    {
       
        EventChecker();
        
        Animasyon();
        GroundController();
        MovementPlayer();
    }


    void GroundController()
    {

        if (Onground)

        { Animator.SetBool("Onground", true); }
        else
        {
            Animator.SetBool("Onground", false);
        }

    }

   void Animasyon()
    {
       

        if (Mathf.Abs(MySpeedX) > 0.1f)
        {
            Animator.SetBool("Running", true);


        }else if(Mathf.Abs(MySpeedX) <.1f)
        {

            Animator.SetBool("Running", false);
        }

        if (!Onground)
        {


            if (rb.velocity.y < 0)
            {

                Animator.Play("Fall");

            }else if (rb.velocity.y > 0)
            {
                Animator.Play("Rise");
            }

        }

        if (Attack)
        {
            Animator.Play("Attack");
            

        }

        if (Attack2)
        {
            Animator.Play("Attack2");

        }

        if (Crouch == true)
        {

            if (Mathf.Abs(rb.velocity.x) > 3f)
            {

                Animator.Play("Slide");

               




            }
            else
            {


                Animator.Play("Crouch");


            }



        }

        



    }

    void EventChecker()
    {
        //Movement
        MySpeedX = Input.GetAxis("Horizontal");



        //Jump
        if (Input.GetKeyDown(KeyCode.Space)&&Onground)
        {
            Jump();

        }


        //attack

        if (Input.GetMouseButtonDown(0)&&Onground)
        {
            AttackFuction();


        }
        if (Input.GetMouseButtonDown(1) && Onground)
        {
            SecondAttack();


        }


        if (Input.GetKeyDown(KeyCode.LeftControl)&&Onground)
        {

         

                  CrocuhFonc();

          



          
          
        }

         if(Input.GetKeyUp(KeyCode.LeftControl)&&Onground)
        {

            GetUp();  
          
        }

         

    }

    void Jump()
    {
        if(CanJump)
        rb.velocity = new Vector2(rb.velocity.x, JumpForce); 

    }



    void CrocuhFonc()
    {
        
           

        Crouch = true;
        colider.size = new Vector2(colider.size.x, colider.size.y / 2);

        CanJump = false;
        CanAttack = false;


      

    }
    void GetUp()
    {


        colider.size = new Vector2(colider.size.x, colider.size.y * 2);
        Crouch = false;
        CanJump = true;
        CanAttack = true;

    }


    void SecondAttack()
    {

        Attack2 = true;
        CanJump = false;

        StartCoroutine(AttackSlowDown());
    }

    void AttackFuction()
    {

        if (CanAttack)
        {
            Attack = true;
            CanJump = false;
            StartCoroutine(AttackSlowDown());
        }
    }

    void MovementPlayer()
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

    void AlertObserver(string Komut)
    {

        if(Komut == "AttackFinish")
        {
            Attack = false;


            CanJump = true;
            //düşman yakınsa damage yedircez



        }
        else if( Komut== "Attack2Finish")
        {

            Attack2 = false;


            CanJump = true;

            //düşman yakınsa damage yediricez ama özel

        }


       


    }

    IEnumerator AttackSlowDown()
    {

        Speed = Speed / 10;

        yield return new WaitForSeconds(.7f);

        Speed *= 10;

    }

}
