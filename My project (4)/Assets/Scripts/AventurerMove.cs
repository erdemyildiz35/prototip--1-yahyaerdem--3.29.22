using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AventurerMove : MonoBehaviour
{
    Animator AnimatorAdventurer;
    [SerializeField] Rigidbody2D rb;
    CapsuleCollider2D colider;
    Skills skills;
    public Image RopeKeyImage;
    Vector3 Rope;
    [SerializeField] Slider ExpSlider;
    [SerializeField] Slider StaSlider;
    public Text LevelText;
    public float Stamina;
    public float calculatedDamage;
    public bool canCalculate;


    //Başlangıç Boyutu
    Vector3 DefaultLocalScale;
    Vector2 crouchSize;
    Vector2 crouchOffset;

    //Checkerlar
    public bool IsGround;
    public bool isAttacking;
    public bool isDash = false;
    public bool isCrouch = false;
    public bool HidePlace = false;
    public bool Hide = false;
    public bool DoubleJump = false;
    public bool canClimb = false;
    public bool isClimbing = false;
    public bool FlipRight = false;
    public bool canWallSlide = false;
    public bool isWallSlide = false;
    public bool WallSlideJump = false;
    public bool isFalling = false;
    public bool isOnRope = false;
    AdventurerHealth adventurerhealth;

    //speed
    public float MySpeedX;
    public float MySpeedY;
    [SerializeField] public float Speed;
    [SerializeField] float DashSpeed;
    [SerializeField] float SuperSpeed;
    public float TempSpeed;
    public float TempAnimatorSpeed;


    //JumpForce
    [SerializeField] float JumpForce = 3f;

    //attack
    float AttackDamage;
    [SerializeField] Transform SwordAttackPoint;
    [SerializeField] Transform HandAttackPoint;
    [SerializeField] Transform LeftPoint;
    [SerializeField] Transform RightPoint;
    Collider2D[] hitEnemies;

    //SwordOrNot
    private bool HandOrSword = false;

    //Layermask
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] LayerMask HideLayer;
    [SerializeField] LayerMask WallLayer;

    SaveSystem saveSystem;


    //soundsSystem

    AudioSource source;
    [SerializeField] AudioClip attack,  jump,punch,dash;


    //particle System
    [SerializeField] GameObject SwordOnParticle;
    [SerializeField] GameObject SwordOffParticle;
    [SerializeField] GameObject DashParticle;
    [SerializeField] GameObject FastRunParticle;
    [SerializeField] GameObject kickParticle;
    [SerializeField] GameObject PunchParticle;
    [SerializeField] GameObject SwordParticle;
 

    void Start()
    {
        source = GetComponent<AudioSource>();
        AnimatorAdventurer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();
        skills = GetComponent<Skills>();
        adventurerhealth = GetComponent<AdventurerHealth>();

        StaSlider = GameObject.Find("Stamina").GetComponent<Slider>();
        ExpSlider = GameObject.Find("Exp").GetComponent<Slider>();
        saveSystem = GetComponent<SaveSystem>();

        if (GameObject.Find("RopeKeyImage"))
        {
            RopeKeyImage = GameObject.Find("RopeKeyImage").GetComponent<Image>();
            RopeKeyImage.enabled = false;
        }
        saveSystem.Load();

        DefaultLocalScale = transform.localScale;

        Speed += ((skills.agi * Speed) / 50);
        AnimatorAdventurer.speed += ((skills.agi * AnimatorAdventurer.speed) / 50);

        TempAnimatorSpeed = AnimatorAdventurer.speed;
        TempSpeed = Speed;

        Stamina = 100 + ((float)skills.sta * 10);
        StaSlider.maxValue = Stamina;

    }

    void Update()
    {
        KeyInputs();
        Movement();
        isitOnHidePoints();
        AnimationControl();
        StaSlider.value = Stamina;

        if (Stamina < 100)
        {
            StartCoroutine(StaCalculateIE());
            if (canCalculate)
            {
                StaCalculate();
            }
        }

        if (IsGround && DoubleJump)
        {
            DoubleJump = false;
        }
    }

    void isitOnHidePoints()
    {

        if (Physics2D.OverlapCircle(transform.position, .5f, HideLayer))
        {
            HidePlace = true;
        }
        else
        {
            HidePlace = false;
        }

        if (HidePlace && isCrouch)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);
            Hide = true;
            gameObject.layer = 10;

        }
        else if (!HidePlace)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 255);
            Hide = false;
            gameObject.layer = 9;
        }
        else if (!isCrouch)
        {
            Hide = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 255);
            gameObject.layer = 9;
        }
    }

    void Movement()
    {
        if (!isWallSlide && !WallSlideJump)
        {
            rb.velocity = new Vector2((MySpeedX * Speed), rb.velocity.y);
        }


        if (MySpeedX > 0 && Speed > 0 && !isWallSlide)
        {
            transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            FlipRight = true;
        }
        else if (MySpeedX < 0 && Speed > 0 && !isWallSlide)
        {
            transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            FlipRight = false;
        }
    }

    void AnimationControl()
    {
        //Animasyon koşuş Kontrolü
        if (IsGround && Mathf.Abs(MySpeedX) > .1 && Speed > 0)
        {

            AnimatorAdventurer.SetBool("Running", true);
        }
        else if (IsGround && Mathf.Abs(MySpeedX) ==0 && Speed > 0)
        {
            AnimatorAdventurer.SetBool("Running", false);
        }
        else if(Speed == 0)
        {
            AnimatorAdventurer.SetBool("Running", false);
        }

        //Düşüş Kontrol
        if (IsGround)
        {
            AnimatorAdventurer.SetBool("IsGround", true);
            AnimatorAdventurer.SetBool("isJumping", false);
            WallSlideJump = false;
            isFalling = false;
        }
        else
        {
            AnimatorAdventurer.SetBool("IsGround", false);
        }

        if (isClimbing)
        {
            Climb();
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            AnimatorAdventurer.speed = TempAnimatorSpeed;
        }

        //Fall Kontrol
        if (!IsGround && rb.velocity.y < 0.1 && !isAttacking && !isClimbing && !isWallSlide)
        {
            AnimatorAdventurer.SetBool("isJumping", false);
            isFalling = true;
            AnimatorAdventurer.Play("Fall");
            WallSlideJump = false;
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

        if (Physics2D.OverlapCircle(transform.position, 0.5f, WallLayer) && !IsGround && !WallSlideJump && isFalling)
        {
            WallSlide();
            isWallSlide = true;
        }
        else
        {
            isWallSlide = false;
        }
    }

    void KeyInputs()
    {
        //Hareket ve hız
        if (!isClimbing)
        {
            MySpeedX = Input.GetAxisRaw("Horizontal");
        }
        else if (isClimbing)
        {
            MySpeedY = Input.GetAxisRaw("Vertical");
        }


        //Zıplama
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && !isCrouch)
        {
            Jump();
        }

        //Attack
        if (Input.GetMouseButtonDown(0) && !isAttacking && !isCrouch)
        {
            Attack(0);
        }

        if (Input.GetMouseButtonDown(1) && !isAttacking && !isCrouch)
        {
            Attack(1);
        }

        //Dash ve FastRun
        if (Input.GetKey(KeyCode.LeftShift) && IsGround && !isAttacking)
        {
            if (HandOrSword && Stamina > 20)
            {
                Dash();
                //partikül
               
            }
            else if (!HandOrSword && Stamina > 0)
            {
                FastRun();
            }
            else
            {

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canClimb)
            {
                if (isOnRope)
                {
                    isClimbing = true;
                }
             
            }
            else if (isClimbing)
            {
                isClimbing = false;
                AnimatorAdventurer.SetBool("isClimbing", false);
                rb.bodyType = RigidbodyType2D.Dynamic;
                AnimatorAdventurer.speed = 1;
            }
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
            //Kılıç sokma
            Instantiate(SwordOffParticle, transform.position, Quaternion.identity);
        }
        else
        {
            AnimatorAdventurer.Play("SwordDraw");
            AnimatorAdventurer.SetBool("SwordOn", true);
            HandOrSword = true;
            //kılıç çekme
            Instantiate(SwordOnParticle, transform.position, Quaternion.identity);
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

    void Attack(int key)
    {

       
        //havada yumruk animasyonu olmadığı için hata veriyordu o yüzden koşul ekledim
        if (!HandOrSword && key == 1 && !IsGround || !IsGround) { }
        else
        {
            StartCoroutine(AttacStartWait());
        }
        if (!HandOrSword && key == 1 && !IsGround) { }
        else
        {
            isAttacking = true;
        }

        if (HandOrSword)
        {
            if (key == 0)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Attack1");
                    AttackDamage = 20;
                    //saldırı 
                    source.PlayOneShot(attack);
                }
                else
                {
                    AnimatorAdventurer.Play("AirAttack");
                    AttackDamage = 25;
                    Debug.Log("AirAttack");
                    source.PlayOneShot(attack);
                    //Saldırı

                }
            }
            else if (key == 1)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Attack2");
                    AttackDamage = 25;
                    source.PlayOneShot(attack);
                    //Saldırı
                }
                else
                {
                    AnimatorAdventurer.Play("AirAttack2");
                    AttackDamage = 25;
                    Debug.Log("AirAttack2");
                    source.PlayOneShot(attack);
                    //Saldırı
                }
            }

            hitEnemies = Physics2D.OverlapCircleAll(SwordAttackPoint.position, .5f, EnemyLayer);
        }
        else if (!HandOrSword)
        {
            if (key == 0)
            {
                if (IsGround)
                {
                    AnimatorAdventurer.Play("Kick");
                    AttackDamage = 10;
                    source.PlayOneShot(punch);
                    //Saldırı -kick
                }
                else if (!IsGround)
                {
                    AnimatorAdventurer.Play("DropKick");
                    AttackDamage = 10;
                    Debug.Log("DropKick");
                    source.PlayOneShot(punch);
                    //Saldırı -kick
                }
                else if (IsGround && AnimatorAdventurer.GetBool("FastRun") == true)
                {
                    AnimatorAdventurer.Play("Slide");
                    AttackDamage = 10;
                }
            }
            else if (key == 1 && IsGround)
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

        calculatedDamage = AttackDamage + ((AttackDamage * skills.str) / 50) + ((AttackDamage*skills.SwordUpgradeLevel)/10);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Enemy>().isDead)
            {
                if (Stamina >= 20)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(calculatedDamage);
                    Stamina -= AttackDamage/4;
                }
                else
                {
                    enemy.GetComponent<Enemy>().TakeDamage(calculatedDamage * Stamina / 20);
                    Stamina = 0;
                }
            }
        }
    }

    void Dash()
    {
        if (!isDash)
        {
            Stamina -= 10;
            AnimatorAdventurer.Play("smrlt");
            Instantiate(DashParticle, transform.position, Quaternion.identity);
            StartCoroutine(DashIE());
        }
    }

    void FastRun()
    {
        Debug.Log("Stamina=" + Stamina);
        Stamina -= Time.deltaTime * 30;
        if (Stamina > 10)
        {
            Speed = SuperSpeed;
            AnimatorAdventurer.Play("Run2");
            AnimatorAdventurer.SetBool("FastRun", true);
            //tüktükUfakAdımlar
        }
    }

    void Climb()
    {
        isClimbing = true;
        AnimatorAdventurer.SetBool("isClimbing", true);
        canClimb = false;

        if (FlipRight)
        {
            transform.position = new Vector3(Rope.x - 0.3f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Rope.x + 0.3f, transform.position.y, transform.position.z);
        }

        AnimatorAdventurer.Play("LadderClimb");
        AnimatorAdventurer.speed = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (MySpeedY > 0)
        {
            AnimatorAdventurer.speed = TempAnimatorSpeed;
            rb.velocity = new Vector2(rb.velocity.x, (MySpeedY * Speed));
        }
        else if (MySpeedY < 0 && !IsGround)
        {
            AnimatorAdventurer.speed = TempAnimatorSpeed;
            rb.velocity = new Vector2(rb.velocity.x, (MySpeedY * Speed));
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void WallSlide()
    {
        if (isWallSlide)
        {
            rb.velocity = new Vector2(0, -3);
            AnimatorAdventurer.Play("WallSlide");
            if (transform.localScale.x < 0)
            {
                FlipRight = false;
            }
            else if (transform.localScale.x > 0)
            {
                FlipRight = true;
            }
        }
    }

    void Jump()
    {
        AnimatorAdventurer.SetBool("isJumping", true);
        if (IsGround)
        {
            AnimatorAdventurer.Play("Jump");
            source.PlayOneShot(jump);
        }
        else if (rb.velocity.y < 0 && !IsGround && !isWallSlide && !DoubleJump)
        {
            Debug.Log("DoubleJump");
            AnimatorAdventurer.Play("smrlt");
            AllertObserver("Jump");
            DoubleJump = true;
        }
        else if (isWallSlide)
        {
            WallSlideJump = true;
            transform.localScale = new Vector3(-transform.localScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            AnimatorAdventurer.Play("smrlt");
            AllertObserver("Jump");
        }
    }

    public void StaCalculate()
    {
        Stamina += Time.deltaTime * 5;
    }

    public void AllertObserver(string message)
    {
        if (message == "Jump")
        {
            if (!WallSlideJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
            else if (WallSlideJump)
            {
                if (FlipRight)
                {
                    rb.velocity = new Vector2(-5, JumpForce);
                }
                else
                {
                    rb.velocity = new Vector2(5, JumpForce);
                }
            }
        }

        if (message == "AttackEnd")
        {
            Debug.Log("AttackEnd");
            Debug.Log("Speed = " + TempSpeed);
            StartCoroutine(AttackWaitTime());
            Speed = TempSpeed;
        }

    }


    IEnumerator AttacStartWait()
    {
        yield return new WaitForSeconds(0.2f);
        Speed *= 0.1f;
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
        //dashParticle
        source.PlayOneShot(dash);

        yield return new WaitForSeconds(.4f);

        isDash = false;


    }
    IEnumerator AttackWaitTime()
    {
        yield return new WaitForSeconds(.5f);
        isAttacking = false;
    }

    IEnumerator StaCalculateIE()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isAttacking && !isDash && !AnimatorAdventurer.GetBool("FastRun") && adventurerhealth.DamageCanBeTakenBool)
        {
            canCalculate = true;
        }
        else
        {
            canCalculate = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Rope")
        {
            canClimb = true;
            //  RopeKeyImage.transform.position = new Vector3(collision.transform.position.x + 0.2f, collision.transform.position.y, collision.transform.position.z);
            RopeKeyImage.enabled = true;
            Rope = collision.transform.position;
            isOnRope = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jumper")
        {

            AnimatorAdventurer.Play("Jump");
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * 3.2f);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            RopeKeyImage.enabled = false;
            canClimb = false;
            if (isClimbing)
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
                RopeKeyImage.enabled = false;    
                isClimbing = false;
                AnimatorAdventurer.SetBool("isClimbing", false);
                Debug.Log("Rope Trigger Exit");
            }
        }
    }


}
