using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    private bool OnGround;
    private float Width;
    private Rigidbody2D fireBallBody;
    [SerializeField] float Speed;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] GameObject BuwBuwpow;
    AventurerMove Adventurer;
    Vector3 MovePlayer;
    void Start()
    {
        Width = GetComponent<SpriteRenderer>().bounds.extents.x;
        fireBallBody = GetComponent<Rigidbody2D>();
        Adventurer = GetComponent<AventurerMove>();


    }

    
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * Width / 2), Vector2.down, 2f,GroundLayer);

      
        if (Physics2D.OverlapCircle(transform.position, 2f, PlayerLayer))
        {

            StartCoroutine(Buwww());
            Vector2 target = new Vector2(Adventurer.transform.position.x, fireBallBody.position.y);
            MovePlayer = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);

        }

        if (hit.collider!=null)
        {

            OnGround = true;

        }
        else
        {
            OnGround = false;
        }

        if (!OnGround)
        {

            transform.eulerAngles += new Vector3(0, 180f, 0);
        }

        fireBallBody.velocity = new Vector2(transform.right.x * Speed, 0f);
    }


    IEnumerator Buwww()
    {
       
        SpriteRenderer thissp = GetComponent<SpriteRenderer>();
        thissp.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5f);
        thissp.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.5f);
        thissp.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5f);
        
        
        //patladığında xp vericek kısım

        Instantiate(BuwBuwpow, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

}
