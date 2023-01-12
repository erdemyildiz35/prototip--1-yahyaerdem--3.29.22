using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public EnemyhealthBar healthbar;
    public Skills skills;
    public bool TakingDamage = false;

    [SerializeField] GameObject BloodParticle;
    [SerializeField] GameObject slashParticle;
    [SerializeField] GameObject ExpParticle;

    private Vector3 Scale;
    public AventurerMove Hero;
    public string LayerOfThisObject;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Scale = transform.localScale;
        healthbar.SetHealth(Health, MaxHealth);
        Hero = FindObjectOfType<AventurerMove>();
        skills = GameObject.Find("Hero").GetComponent<Skills>();
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
        if (!isDead)
        {
            TakingDamage = true;
            animator.Play("TakeHit");
            Instantiate(BloodParticle, transform.position, Quaternion.identity);
            GameObject go = Instantiate(slashParticle, transform.position, Quaternion.identity);
            go.transform.localScale *= Damage / 25;
            Health -= Damage;
            if (Health <= 0)
            {   
                if(gameObject.name == "FireWorm")
                {
                    skills.GainExp(15);
                }
                else if (gameObject.name == "Zombie")
                {
                    skills.GainExp(15);
                }
                else if (gameObject.name == "Ghoul")
                {
                    skills.GainExp(15);
                }
                else if (gameObject.name == "Mushroom")
                {
                    skills.GainExp(15);
                }
                else if (gameObject.name == "EyePos")
                {
                    skills.GainExp(15);
                }
                else if (gameObject.name == "boss")
                {
                    skills.GainExp(100);
                }else if (gameObject.name== "evil Wizard")
                {
                    skills.GainExp(150);
                    GatesManager Manager = FindObjectOfType<GatesManager>();
                    Manager.iswizardDestryed = true;

                }
                else
                {
                    skills.GainExp(15);
                }
                skills.Gold += 50;
                skills.CalculateGold();
                Instantiate(ExpParticle, transform.position, Quaternion.identity);
                Die();
            }

            healthbar.SetHealth(Health, MaxHealth);

        }


        StartCoroutine(TakingDamageIE());
    }

    IEnumerator TakingDamageIE()
    {
        yield return new WaitForSeconds(0.3f);
        TakingDamage = false;
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
            if (GetComponent<Rigidbody2D>()) { GetComponent<Rigidbody2D>().isKinematic = false; }
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
            TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Fireball")
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.tag == "Lava")
        {
            TakeDamage(100);
        }
    }

}
