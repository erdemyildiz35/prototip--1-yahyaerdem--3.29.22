using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float Health = 100;
    [SerializeField] GameObject TakedamageParticle;
    [SerializeField] Slider slider;
    private bool isBurning = false;
    public bool isGuarded = false;
    GameObject BurnEffects;
    Animator animator;
    float Timer = 0;
    [SerializeField] private bool OnGuard = false;

    private void Awake()
    {
        BurnEffects = GameObject.Find("HeroBurningEffects");
        BurnEffects.SetActive(false);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Timer++;
        slider.value = Health;
    }

    public void Guard(bool Onguard)
    {
        if (Onguard)
        {
            isGuarded = true;
        }
        else
        {
            isGuarded = false;
        }
    }

    private void Burn(float Damage)
    {
        if (!isBurning)
        {
            StartCoroutine(BurnCour(Damage));
        }
    }

    IEnumerator BurnCour(float Damage)
    {
        isBurning = true;
        BurnEffects.SetActive(true);
        // Yanma Efekti
        // GetComponent<SpriteRenderer>().color = new Color(255, 108, 0, 255);
        for (int i = 0; i < 5; i++)
        {
            TakeDamage(Damage/3, 1);
            yield return new WaitForSeconds(1);
        }
        //Týs sesi

        isBurning = false;
        BurnEffects.SetActive(false);
        //  GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(float Damage, int DamageTuru)  // DamageTürü: 0 == Kesikli saldýrýlar , 1 == Yanma hasarý, Daha eklenebilir.
    {
        if(gameObject.layer == 11)
        {

        }
        else
        {
            switch (DamageTuru)
            {
                case 0:
                    if (isGuarded)
                    {
                        Damage = Damage / 2;
                    }
                    else
                    {
                        animator.Play("TakeHit");
                    }
                    Instantiate(TakedamageParticle, transform.position, Quaternion.identity);
                    break;
                case 1:
                    Burn(Damage);
                    break;
                default:
                    break;
            }

            Health -= Damage;
        }
       
    }

    public void Heal(float HealAmount)
    {
        Health += HealAmount;

        if (Health <= MaxHealth)
        {
            Health = MaxHealth;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (Timer > 1)
            {
                TakeDamage(20, 0);
                Timer = 0;
            }
        }

        else if (collision.gameObject.tag == "FireBall")
        {
            TakeDamage(20, 1);
        }

        else if (collision.gameObject.tag == "Can")
        {
            Heal(20);
        }

        else if (collision.gameObject.tag == "Lava")
        {
            TakeDamage(100, 1);
        }
    }
}
