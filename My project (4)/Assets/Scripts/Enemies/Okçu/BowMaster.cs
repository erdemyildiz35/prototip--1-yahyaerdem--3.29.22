using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMaster : MonoBehaviour
{

    [SerializeField] GameObject Arrow;
    [SerializeField] Transform BowPoint;
    [SerializeField] float BowSpeed;
    [SerializeField] float RangeOfPlayer;
    [SerializeField] LayerMask PlayerLayer;


    AventurerMove Aventurer;
    Animator AnimatorOfBowMan;
    Rigidbody2D RbArcher;


    bool isRight;
    Vector3 DefaultLocalScale;

    void Start()
    {
        Aventurer = FindObjectOfType<AventurerMove>();
        RbArcher = GetComponent<Rigidbody2D>();
        AnimatorOfBowMan = GetComponent<Animator>();
        DefaultLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (Aventurer.transform.position.x <= transform.position.x)
        {
            isRight = false;

        }
        else if (Aventurer.transform.position.x > transform.position.x)
        {
            isRight = true;

        }


        if (Physics2D.OverlapCircle(transform.position, RangeOfPlayer * 2, PlayerLayer) && Mathf.Abs((100 - transform.position.y) - (100 - Aventurer.transform.position.y)) <= 5)
        {

            if (isRight )
            {
                transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
            }

            if (!isRight )
            {
                transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);

            }

            AnimatorOfBowMan.SetBool("Attack", true);


        }
        else
        {
            AnimatorOfBowMan.SetBool("Attack", false);

        }

    }




    public void insBow()
    {

       GameObject Go= Instantiate(Arrow, BowPoint.position, Quaternion.identity);

        if (isRight)
        {
            Go.GetComponent<SpriteRenderer>().flipX = true;
            Go.GetComponent<Rigidbody2D>().velocity = new Vector2(BowSpeed, 0);

        }
        else
        {
            Go.GetComponent<SpriteRenderer>().flipX = true;
            Go.GetComponent<Rigidbody2D>().velocity = new Vector2(-BowSpeed, 0);
        }

        Destroy(Go, 5f);


    }
}



