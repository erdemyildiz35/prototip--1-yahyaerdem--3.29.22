using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AdventurerHealth : MonoBehaviour
{
    
    public float Health;
    public float MaxHealth;
    [SerializeField] Slider healthSlider;
    Skills skills;
    public Color low;
    public Color Highh;
    public Vector3 offset;
    Animator AdventurerAnimator;
   
    int Rand;
    AventurerMove aventurerMove;
    public LayerMask Obstaclelayer;

    [SerializeField] GameObject playerBloodParticle;

    public bool DamageCanBeTakenBool = true;


    AudioSource source;
    [SerializeField] AudioClip takeDamage; 
    void Start()
    {
        source = GetComponent<AudioSource>();
        aventurerMove = GameObject.Find("Hero").GetComponent<AventurerMove>();
        skills = FindObjectOfType<Skills>();
        AdventurerAnimator = GetComponent<Animator>();

        healthSlider = GameObject.Find("Health").GetComponent<Slider>();

       

        MaxHealth += (skills.sta * MaxHealth) / 10;
        Health = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        HealThBarStatus();
        //healthSlider.gameObject.SetActive(false); 
        
    }

    void HealThBarStatus()
    {

        //healthSlider.gameObject.SetActive(Health < MaxHealth);
        healthSlider.value = Health;
        healthSlider.maxValue = MaxHealth;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);


    }


    void Update()
    {
        //die Part
        if (Physics2D.OverlapCircle(transform.position, .5f, Obstaclelayer))
        {
            Die();

        }


    }

    public void TakeDamage(float Damage)
    {
        Debug.Log("TakeDamage");
        aventurerMove.AllertObserver("AttackEnd");
        Instantiate(playerBloodParticle, transform.position, Quaternion.identity);
        Damage -= (Damage * skills.ArmorUpgradeLevel) / 10;
        if (DamageCanBeTakenBool)
        {
            source.PlayOneShot(takeDamage);
            Rand = Random.Range(0, 100);
            if (Rand <= (70 + ((70 * skills.agi) / 50)) || !aventurerMove.IsGround)
            {
                Health -= Damage;
            }
            else
            {
                Health -= Damage * 1.2f;
                AdventurerAnimator.Play("KnockDown");
                StartCoroutine(damageCanBeTakeen());
            }
            healthSlider.value = Health;
            StartCoroutine(PlusMinusShowHide());
            healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
        }
        if (Health <= 0)
        {
           
                Die();
           
          
          
        }

    }

 

    public void Die()
    {
        StartCoroutine(DieTime());
    }
    public void Heal(float HealthAmounth)
    {

        Health += HealthAmounth;
        healthSlider.value = Health;
        StartCoroutine(PlusMinusShowHide());
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
    }
    IEnumerator damageCanBeTakeen()
    {

        DamageCanBeTakenBool = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        DamageCanBeTakenBool = true;
    }


    IEnumerator PlusMinusShowHide()
    {
        //healthSlider.gameObject.SetActive(true);
        healthSlider.value = Health;
        yield return new WaitForSeconds(5f);
        //healthSlider.gameObject.SetActive(false);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            TakeDamage(15);
        }

        if (collision.gameObject.tag == "Dead")
        {
            Die();
        }
        if (collision.gameObject.tag == "Heal")
        {

            Heal(15);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Exp")
        {
            GetComponent<Skills>().GainExp(15);

            Destroy(collision.gameObject, .2f);
        }
        if (collision.gameObject.tag == "Gold")
        {
            GetComponent<Skills>().Gold += 50;
            Destroy(collision.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShowAndDestroy")
        {
            
        }
    }

    IEnumerator DieTime()
    {
        AdventurerAnimator.Play("KnockDown");
        Time.timeScale = .3f;

        yield return new WaitForSeconds(.6f);
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
