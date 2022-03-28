using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float Health = 100;
    [SerializeField] GameObject shrink;
    [SerializeField] GameObject Dyie;
    

  
    void Start()
    {
        
    }

  
    void Update()
    {
        Die();
    }

    public void Takedamage(float Sayi) {

        Health -= Sayi;

        if (Sayi <= 3)
        {

            GameObject go = Instantiate(shrink, transform.position, Quaternion.identity);
            go.transform.localScale = go.transform.localScale ;
        }
        else if (Sayi >= 3)
        {

         GameObject go=   Instantiate(shrink, transform.position, Quaternion.identity);
            go.transform.localScale = go.transform.localScale * 2;

        }
        else if (Sayi > 20)
        {

            GameObject go = Instantiate(shrink, transform.position, Quaternion.identity);
            go.transform.localScale = go.transform.localScale * 4;
        }

      

       }



   
    void Die()
    {
        if (Health < 0)
        {

            StartCoroutine(Dying());
        }

    }

    IEnumerator Dying()
    {

        yield return new WaitForSecondsRealtime(.1f);
        Instantiate(Dyie, transform.position,Quaternion.identity);
        Destroy(this.gameObject);
        
      

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {

            Health -= 100;

        }

        if (collision.gameObject.tag == "Player")
        {

            Takedamage(25);

        }
        if (collision.gameObject.tag == "Shuruken")
        {
            Takedamage(15);

        }
    }
}
