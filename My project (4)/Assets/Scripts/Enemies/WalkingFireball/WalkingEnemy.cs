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

    void Start()
    {
        Width = GetComponent<SpriteRenderer>().bounds.extents.x;
        fireBallBody = GetComponent<Rigidbody2D>();



    }

    
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * Width / 2), Vector2.down, 2f,GroundLayer);

        if (Physics2D.OverlapCircle(transform.position, 2f, PlayerLayer))
        {

            StartCoroutine(Buwww());


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
        thissp.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.5f);
        thissp.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5f);
        thissp.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.5f);
        thissp.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5f);
        Instantiate(BuwBuwpow, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

}
