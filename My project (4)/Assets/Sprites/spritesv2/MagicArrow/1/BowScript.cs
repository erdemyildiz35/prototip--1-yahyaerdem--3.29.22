using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    [SerializeField] float Damage = 10;
    [SerializeField] GameObject BuwDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().Takedamage(Damage);
           Instantiate(BuwDamage,transform.position,Quaternion.identity);
            Destroy(this.gameObject, .5f);
        }
    }
}
