using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{




    Rigidbody2D rbAngel;
    [SerializeField] float Speed = 5f;
    Heromovement Hero;
    Animator Anime;
    float AttackTimer = 0f;
    [SerializeField] private GameObject Fireball;

    Vector2 Up = new Vector2(0, 2f);
    void Start()
    {
        rbAngel = GetComponent<Rigidbody2D>();
        Hero = FindObjectOfType<Heromovement>();
        Anime = GetComponent<Animator>();
    }

   
    void Update()
    {
        AttackTimer += Time.deltaTime;

        if (Vector2.Distance(transform.position, Hero.transform.position) < 12f&&AttackTimer>10f)
        {
            Attack();




        }
        else
        {

            Anime.SetBool("Attack", false);

        }



    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

       transform.Translate(Up*(Speed/100));
        
    }


    private void Attack()
    {

        Anime.SetBool("Attack", true);
        AttackTimer=0;
        Instantiate(Fireball, transform.position, Quaternion.identity);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger") {


            Up = -Up;
        
        }


    }
}
