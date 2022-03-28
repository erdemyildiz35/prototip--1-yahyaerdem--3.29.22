using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private bool OnGround;
    private float Width;
    private Rigidbody2D fireBallBody;
    [SerializeField] float Speed;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask PlaYerLayer;

    Heromovement Hero;
    Vector2 MovePlayer;
    Animator anime;
    bool isRight;
    private Vector3 DefaultLocalScale;
    [SerializeField] Vector3 Offset = new Vector3();
    [SerializeField] float Damage=10;
    float AttackTimer = 0f;
    void Start()
    {
        Width = GetComponent<SpriteRenderer>().bounds.extents.x;
        fireBallBody = GetComponent<Rigidbody2D>();
        DefaultLocalScale = transform.localScale;
        Hero = FindObjectOfType<Heromovement>();
        anime = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        MovePlayer = Vector2.MoveTowards(transform.position, Hero.transform.position, Speed * Time.fixedDeltaTime);
        AttackTimer += Time.fixedDeltaTime;

        if (Physics2D.OverlapCircle(transform.position, 15f, PlaYerLayer))
        {
            fireBallBody.MovePosition(MovePlayer);

            if (isRight)
            {
                transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            }
         
            if (!isRight)
            {
                transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);

            }


            if (Physics2D.OverlapCircle(transform.position, 3f, PlaYerLayer))
            {
                if (AttackTimer > 5f)
                {

                    StartCoroutine(attack());
                    AttackTimer = -2f;
                }

            }



        }
    }

    void Update()
    {


        if (Hero.transform.position.x <= transform.position.x)
        {
            isRight = false;
        }
        else if(Hero.transform.position.x > transform.position.x)
        {
            isRight = true;
        }
        
        
      
    }
    

    IEnumerator attack()
    {
        anime.Play("Attack");


        yield return new WaitForSeconds(.5f);


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3f, PlaYerLayer);
        foreach (Collider2D hero in hitEnemies)
        {
            hero.GetComponent<HeroHealth>().TakeDamage(Damage,0);

        }
       

    }

}
