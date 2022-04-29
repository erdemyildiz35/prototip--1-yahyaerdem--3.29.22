using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliyngJumperObstacle : MonoBehaviour
{
    [SerializeField] float Speed=10;
    Rigidbody2D rbAngel;
    Vector2 Right = new Vector2 (1f, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }
    private void Move()
    {

        transform.Translate(Right * (Speed / 100));

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {


            Right = -Right;

        }


    }

}
