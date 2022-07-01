using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliyingeye : MonoBehaviour
{
    AventurerMove Hero;
    Animator Anime;
    Rigidbody2D rbAngel;
    [SerializeField] float Speed = 5f;
    Vector2 Up = new Vector2(0, 2f);
    Vector3 LocalScale;
    // Start is called before the first frame update
    void Start()
    {
        rbAngel = GetComponent<Rigidbody2D>();
        Hero = FindObjectOfType<AventurerMove>();
        Anime = GetComponent<Animator>();
        LocalScale = transform.localScale;
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        Move();
        if (Hero.transform.position.x - transform.position.x > 0)
        {
            FlipRight();
        }
        else
        {
            FlipLeft();
        }

    }

    private void Move()
    {

        transform.Translate(Up * (Speed / 100));

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {


            Up = -Up;

        }


    }
    private void FlipRight()
    {
        transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
    }

    private void FlipLeft()
    {
        transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
    }

}
