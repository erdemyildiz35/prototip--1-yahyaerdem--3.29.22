using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField] float Speed=3f;
    Rigidbody2D rb;
    private bool OnGround;
    private float Width;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] float DownSize=16f;
    AdventurerHealth Adventurer;

    Animator DevilAnimator;

    void Start()
    {
        Width = GetComponent<SpriteRenderer>().bounds.extents.x;
        rb = GetComponent<Rigidbody2D>();
        Adventurer = FindObjectOfType<AdventurerHealth>();

        DevilAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * Width / 2), Vector2.down, DownSize, GroundLayer);
        
        if (Physics2D.OverlapCircle(transform.position, 1f, PlayerLayer))
        {

            Adventurer.TakeDamage(200);
            DevilAnimator.Play("Attack");


        }

        if (hit.collider != null)
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

        rb.velocity = new Vector2(transform.right.x * Speed, 0f);
    }

}
