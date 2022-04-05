using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventurerHealth : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    [SerializeField] Slider healthSlider;
    Skills skills;
    public Color low;
    public Color Highh;
    public Vector3 offset;


    void Start()
    {
       
        skills = FindObjectOfType<Skills>();
        MaxHealth += (skills.sta*MaxHealth)/10;
        Health = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        HealThBarStatus();
        healthSlider.gameObject.SetActive(false);

        
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

        Health -= Damage;
        healthSlider.value = Health;
        StartCoroutine(PlusMinusShowHide());
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
    }
    public void Heal(float HealthAmounth)
    {

        Health += HealthAmounth;
        healthSlider.value = Health;
        StartCoroutine(PlusMinusShowHide());
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, healthSlider.normalizedValue);
    }


    IEnumerator PlusMinusShowHide()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = Health;
        yield return new WaitForSeconds(5f);
        healthSlider.gameObject.SetActive(false);
       

    }

}
