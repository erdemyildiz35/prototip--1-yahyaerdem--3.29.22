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


    public bool DamageCanBeTakenBool = true;

    void Start()
    {
       
        skills = FindObjectOfType<Skills>();
        MaxHealth += (skills.sta*MaxHealth)/10;
        Health = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        HealThBarStatus();
        healthSlider.gameObject.SetActive(false);
        AdventurerAnimator = GetComponent<Animator>();
        
    }

    void HealThBarStatus()
    {

        healthSlider.gameObject.SetActive(Health < MaxHealth);
        healthSlider.value = Health;
        healthSlider.maxValue = MaxHealth;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
        

    }

   
    void Update()
    {
       
     
    }

    public void TakeDamage(float Damage)
    {
        if (DamageCanBeTakenBool)
        {

            Health -= Damage;
            healthSlider.value = Health;
            StartCoroutine(PlusMinusShowHide());
            healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
            AdventurerAnimator.Play("KnockDown");
            StartCoroutine(damageCanBeTakeen());
        }
        if (Health <= 0)
        {
            Die();


        }

    }

    private void Die()
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

        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        DamageCanBeTakenBool = true;
    }


    IEnumerator PlusMinusShowHide()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = Health;
        yield return new WaitForSeconds(5f);
        healthSlider.gameObject.SetActive(false);
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            TakeDamage(15);


        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
     
    }
    
  


    IEnumerator DieTime()
    {

        Time.timeScale = .3f;

        AdventurerAnimator.Play("KnockDown");
     

        yield return new WaitForSeconds(.6f);
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
