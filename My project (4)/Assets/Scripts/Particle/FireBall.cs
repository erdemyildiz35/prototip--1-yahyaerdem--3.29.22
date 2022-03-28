using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Heromovement Hero;
    [SerializeField] float Speed;
    Rigidbody2D Rb;
    Vector2 Target;
    [SerializeField] GameObject Bowww;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Hero = FindObjectOfType<Heromovement>();
        Target = ((Hero.transform.position+Vector3.down) - transform.position).normalized ;
        Rb.velocity = new Vector2(Target.x, Target.y)*Speed;
        Destroy(this.gameObject, 4f);
    }

    public void BlowUp()
    {

        Destroy(this.gameObject);
        Instantiate(Bowww, transform.position, Quaternion.identity);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BlowUp();

        }

           
    }

}
