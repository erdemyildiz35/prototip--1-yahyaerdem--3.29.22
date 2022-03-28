using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heromovement : MonoBehaviour
{


    private float MySpeedX;
    Rigidbody2D rb;
    CapsuleCollider2D colider;
    public GameObject tornado;

    //editör değerleri
    [SerializeField] float Speed;
    [SerializeField] float JumpForce = 3f;
    [SerializeField] Transform AttackPoint;
    [SerializeField] float DashForce = 6f;

    //default local scale
    private Vector3 DefaultLocalScale;
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] LayerMask fireballLayer;
    [SerializeField] float AttackDamage = 10;

    //particles
    [SerializeField] GameObject Dashparticle;
    [SerializeField] GameObject JumpParticle;
    [SerializeField] float Stamina = 20;
    [SerializeField] private GameObject Shuriken;
    [SerializeField] LayerMask enemyLayer;


    SpriteRenderer Renderer;

    HeroHealth HeroHealth;
    Animator HeroAnimator;

    //Checker
    public bool IsGround;
    public bool isCrouching = false;
    public bool isGuard = false;
    public bool isAttack = false;
    public bool CanAttack;
    public bool CanJump = true;
    public bool CanAirAttack = true;
    public bool isSlowMotion = false;

    [SerializeField] Slider AttackTimerSlider;

    private float GuardSpeed = 1;//guard anında normal hız ile çarpılıp 0 lanmasını sağlamak için koyulan alan

    //timers
    private float DashTimer;
    private float AttackTimer;
    private float TempSpeed;
    private float TempGravity = 0;


    private bool TakeDam = false;

    private float ShurikenTimer = 0f;
    private bool TornadoActive;

    void Start()
    {
        Stamina = 0;
        HeroHealth = GetComponent<HeroHealth>();
        rb = GetComponent<Rigidbody2D>();
        HeroAnimator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        DefaultLocalScale = transform.localScale;
        colider = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        if (Stamina <= 100)
        {
            if (!isGuard && !isSlowMotion)
            {
                Stamina += Time.fixedDeltaTime;
            }
        }
        DashTimer += Time.fixedDeltaTime;
        AttackTimer += Time.fixedDeltaTime;
        ShurikenTimer += Time.fixedDeltaTime;

        Fall();
        Movement();
        KeyEvent();
        EventChecker();

        AttackTimerSlider.value = Stamina;
        HeroAnimator.SetBool("Onground", IsGround);
    }

    void EventChecker()
    {
        if (isGuard)
        {
            MySpeedX = 0;
        }
        else
        {
            MySpeedX = Input.GetAxis("Horizontal");//-1 ile 1 arasında bağlı olarak 
        }

        if (AttackTimer > 8f)
        {
            CanAttack = true;
        }

        if (isSlowMotion)
        {
            Stamina -= Time.deltaTime * 15;
        }

        if (Stamina <= 0 && isSlowMotion)
        {
            StopSlowMotion();
        }
    }

    void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching && !isGuard)
        {
            StartCoroutine(CrouchIE());
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGround && !isGuard && !isCrouching && !isAttack && CanJump)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Stamina > 15f && IsGround)
        {
            Dash();
        }

        if (Input.GetMouseButton(1) && ShurikenTimer > 20f)
        {
            ShurikenAttack();
        }

        if (Input.GetKey(KeyCode.Q) && IsGround)
        {
            Guard();
        }

        if (Input.GetKeyDown(KeyCode.Z) && CanAttack && !isGuard)
        {
            Attack(KeyCode.Z);
        }

        if (Input.GetKeyDown(KeyCode.X) && CanAttack && IsGround && !isGuard)
        {
            Attack(KeyCode.X);
        }

        if (Input.GetKeyDown(KeyCode.C) && CanAttack && IsGround && !isGuard)
        {
            Attack(KeyCode.C);
            
        }

        if (Input.GetKeyDown(KeyCode.E) && CanAttack && IsGround && !isGuard)
        {
            HeroAnimator.Play("TornadoAttack");
        }

        if (Input.GetKeyDown(KeyCode.R) )
        {

            if (!isSlowMotion)
            {
                SlowMotion();
            }else if (isSlowMotion)
            {

                StopSlowMotion();
            }




        }

       

    }

    void SlowMotion()
    {
        isSlowMotion = true;
        Time.timeScale = Time.timeScale / 2;
        HeroAnimator.speed = HeroAnimator.speed * 2;
        Speed = Speed * 2;
    }

    void StopSlowMotion()
    {
        isSlowMotion = false;
        Time.timeScale = Time.timeScale * 2;
        HeroAnimator.speed = HeroAnimator.speed / 2;
        Speed = Speed / 2;
    }

    void Tornado()
    {
        GameObject Tornado = Instantiate(tornado, AttackPoint.transform.position + Vector3.up, Quaternion.identity);

        if (AttackPoint.transform.position.x > transform.position.x)
        {
            Tornado.GetComponent<Rigidbody2D>().velocity = Vector2.right * 16f;
        }
        else if (AttackPoint.transform.position.x < transform.position.x)
        {
            Tornado.GetComponent<Rigidbody2D>().velocity = Vector2.left * 16f;
        }
        Stamina -= 30;
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

    void Dash()
    {
        StartCoroutine(DashIE());
        StartCoroutine(DashEnemyColliderIE());
        Stamina = Stamina - 15;
    }

    void ShurikenAttack()
    {
        GameObject Shurikenn = Instantiate(Shuriken, AttackPoint.transform.position, Quaternion.identity);

        if (MySpeedX > 0)
        {
            Shurikenn.GetComponent<Rigidbody2D>().velocity = Vector2.right * 8f;
        }
        else if (MySpeedX < 0)
        {
            Shurikenn.GetComponent<Rigidbody2D>().velocity = Vector2.left * 8f;
        }
        Shurikenn.GetComponent<Rigidbody2D>().angularVelocity = 2000f;
        ShurikenTimer = 0;
        Destroy(Shurikenn.gameObject, 3f);
    }

    void Guard()
    {
        HeroAnimator.Play("Guard");
        isGuard = true;
        HeroHealth.isGuarded = true;
        GetComponent<HeroHealth>().Guard(isGuard);
    }

    void Jump()
    {
        CanAirAttack = true;
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        HeroAnimator.Play("Jump");
        StartCoroutine(JumpDelayIE(0.5f));
    }

    public void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, .5f, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Enemy>().isDead)
            {
                    AttackDamage = 20;
                    enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
                    Stamina = 0;
            }
        }
    }

    void Attack(KeyCode key)
    {
        if (key == KeyCode.Z)
        {
            if (Mathf.Abs(rb.velocity.x) > 0 && IsGround)
            {
                HeroAnimator.Play("RunAttack");
                StartCoroutine(RunAttackIE());
                Debug.Log("Run attack");
            }

            else if (IsGround)
            {
                HeroAnimator.Play("Attack1");
                AttackDamage = 10;
                Debug.Log("Attack1");
            }
            else if (!IsGround && CanAirAttack)
            {
                CanAirAttack = false;
                rb.AddForce(new Vector2(rb.velocity.x, 100f));
                HeroAnimator.Play("AirAttack");
                AttackDamage = 10;
                Debug.Log("Air Attack");
            }
        }

        else if (key == KeyCode.X && Mathf.Abs(rb.velocity.x) == 0)
        {
            HeroAnimator.Play("Attack2");
            AttackDamage = 20;
        }

        else if (key == KeyCode.C)
        {
            SuperSkillShot();
        }

        StartCoroutine(AttackIE());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, .5f, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Enemy>().isDead)
            {
                if (Stamina <= 20)
                {
                    AttackDamage = AttackDamage * Stamina / 50;
                    enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
                    Stamina = 0;
                }
                else if (Stamina > 20)
                {
                    AttackDamage = AttackDamage * 20 / 50;
                    enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
                    Stamina -= 20;
                }
            }
        }


        Collider2D[] fireBall = Physics2D.OverlapCircleAll(AttackPoint.position, 1f, fireballLayer);

        foreach (Collider2D ball in fireBall)
        {
            ball.GetComponent<FireBall>().BlowUp();
        }
    }

    void Fall()
    {
        HeroAnimator.SetFloat("Speed", Mathf.Abs(MySpeedX));

        if (IsFalling() && !isAttack)
        {
            HeroAnimator.Play("Fall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            StartCoroutine(TakeDamageIE());
        }

        if (collision.gameObject.tag == "Jumper")
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * 3f);
            Instantiate(JumpParticle, transform.position, Quaternion.identity);
        }
    }

    private bool IsRising()
    {
        if (rb.velocity.y > 0 && !IsGround && !TakeDam)
        {
            return true;
        }
        return false;
    }

    private bool IsFalling()
    {
        if (rb.velocity.y < 0.01f && !IsGround && !TakeDam)
        {
            return true;
        }
        return false;
    }

    IEnumerator RunAttackIE()
    {
        /*  TornadoActive = true;

          if (Stamina <= 20)
          {
              Stamina = 0;
          }
          else if (Stamina > 20)
          {
              Stamina -= 20;
          }

          while (TornadoActive)
          {
              Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, .5f, EnemyLayer);

              foreach (Collider2D enemy in hitEnemies)
              {
                  if (!enemy.GetComponent<Enemy>().isDead)
                  {
                      AttackDamage = AttackDamage * 20 / 50;
                      enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
                  }
              }
              */
        yield return new WaitForSeconds(0.01f);
        //}
        
    }

    void SuperSkillShot()
    {
        HeroAnimator.Play("Attack4");

        StartCoroutine(SuperSkillShoty());

        Stamina -= 30;


    }
    
    IEnumerator SuperSkillShoty()
    {
       // rb.bodyType = RigidbodyType2D.Static;
        gameObject.layer = 11;//layeri pasive çektim

        Collider2D[] Enemyrange1 = Physics2D.OverlapCircleAll(transform.position, 7f, EnemyLayer);
        if (Enemyrange1 != null)
        {
            foreach (Collider2D Eneny in Enemyrange1)
            {
                transform.position = Eneny.transform.position + Vector3.up*2f;
            }
        }

        CanJump = false;
        CanAttack = false;
        CanAirAttack = false;
        isGuard = true;

        
        yield return new WaitForSeconds(3f);
      //  rb.bodyType = RigidbodyType2D.Dynamic;

        gameObject.layer = 9;//layeri tekrar player layer yaptım
        CanJump = true;
        CanAttack = true;
        CanAirAttack = true;
        isGuard = false;


    }

    IEnumerator DashIE()
    {
        gameObject.layer = 11;
        Speed += DashForce;
        HeroAnimator.Play("Dash");
       

        yield return new WaitForSecondsRealtime(.4f);
        gameObject.layer = 9;

        Speed -= DashForce;
    }

    IEnumerator AttackIE()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }

    IEnumerator TakeDamageIE()
    {
        TakeDam = true;
        yield return new WaitForSeconds(1.5f);
        TakeDam = false;
    }

    IEnumerator JumpDelayIE(float delay)
    {
        CanJump = false;
        yield return new WaitForSeconds(delay);
        CanJump = true;
    }

    IEnumerator CrouchIE()
    {
        gameObject.layer = 11;

        Speed *= 1.5f;
        isCrouching = true;
        colider.size = new Vector2(colider.size.x, colider.size.y / 2);
        colider.offset = new Vector2(colider.offset.x, colider.offset.y - 0.40f);
        HeroAnimator.Play("Crouch");


        yield return new WaitForSeconds(0.25f);

        gameObject.layer = 9;

        colider.offset = new Vector2(colider.offset.x, colider.offset.y + 0.40f);
        colider.size = new Vector2(colider.size.x, colider.size.y * 2);
        isCrouching = false;
        Speed /= 1.5f;
    }

    IEnumerator DashEnemyColliderIE()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10.0f, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<CapsuleCollider2D>().isTrigger = true;
            TempGravity = enemy.GetComponent<Rigidbody2D>().gravityScale;
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        yield return new WaitForSeconds(1.2f);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<CapsuleCollider2D>().isTrigger = false;
            enemy.GetComponent<Rigidbody2D>().gravityScale = TempGravity;
        }
    }

    public void AllertObservers(string message)
    {
        if (message == "Guard" && Input.GetKey(KeyCode.Q))
        {
            HeroAnimator.Play("Guard", 0, 0.5f);
        }

        else if (message == "GuardEnded")
        {
            isGuard = false;
            HeroHealth.isGuarded = false;
            GuardSpeed = 1f;

        }

        else if (message == "TornadoEnded")
        {
            TornadoActive = false;
        }
    }
}