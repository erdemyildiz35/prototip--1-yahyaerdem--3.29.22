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

    // Start is called before the first frame update
    void Start()
    {
        rbAngel = GetComponent<Rigidbody2D>();
        Hero = FindObjectOfType<AventurerMove>();
        Anime = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
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
}
