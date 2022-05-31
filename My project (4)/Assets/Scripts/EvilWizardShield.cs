using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardShield : MonoBehaviour
{
    public AdventurerHealth HeroHealth;
    public bool Trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        HeroHealth = GameObject.Find("Hero").GetComponent<AdventurerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Trigger = true;
            StartCoroutine(GiveDamage());
        }     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Trigger = false;
        }
    }

    IEnumerator GiveDamage()
    {
        while (Trigger)
        {
            yield return new WaitForSeconds(0.5f);
            HeroHealth.TakeDamage(5);
        }
    }
}
