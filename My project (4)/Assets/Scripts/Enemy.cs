using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Pathfinding;
public class Enemy : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float Health = 100;
    Animator animator;
    private bool isBurning = false;
    public bool isDead = false;
    private bool isFlying = false;
    public bool isTakingDamage = false;
    public HealthBar healthBar;
    public GameObject HealthBarObject;
    public int maxHealth = 100;
    public int currentHealth;

    private Vector3 Scale;


    private void Awake()
    {
        HealthBarObject = Instantiate(Resources.Load("Prefabs/HealthBar")) as GameObject;
        HealthBarObject.transform.parent = GameObject.Find("EnemyHealthBars").transform;
        HealthBarObject.transform.localScale = new Vector3(1, 1, 1);
        healthBar = HealthBarObject.GetComponent<HealthBar>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Scale = transform.localScale;
        healthBar.SetMaxHealth(maxHealth);

    }

    private void FixedUpdate()
    {
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 3.2f);
    }

    public void FlipRight()
    {
        transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
    }

    public void FlipLeft()
    {
        transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
    }

    public void TakeDamage(float Damage)
    {
       if(!isDead)
        {
            if (!isTakingDamage)
            {
                StartCoroutine(TakeDamageIE(Damage));
            }
            else
            {
                animator.Play("TakeHit");

                Health -= Damage;
                if (Health <= 0)
                {
                    Die();
                }
            }
        }
      
       

    }

   
    IEnumerator TakeDamageIE(float Damage)
    {
        isTakingDamage = true;
        GetComponent<EnemyAıAdvance>().isTakenDamage = true;

        animator.Play("TakeHit");
        Health -= Damage;
        if (Health <= 0)
        {
            GetComponent<EnemyAıAdvance>().isdead = true;

            Die();
        }
        yield return new WaitForSeconds(1);
        isTakingDamage = false;
        GetComponent<EnemyAıAdvance>().isTakenDamage = false;

    }

   
    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            gameObject.layer = 10;
            if (GetComponent<Rigidbody2D>()) { GetComponent<Rigidbody2D>().isKinematic = false;}
            if (GetComponent<CapsuleCollider2D>()) { GetComponent<CapsuleCollider2D>().isTrigger = false; }

            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
       animator.Play("Death");
        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
         // TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Tornado")
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Lava")
        {
            TakeDamage(100);
        }
    }

}
