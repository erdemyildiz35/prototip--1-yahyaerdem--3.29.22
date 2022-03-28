using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarasalar : MonoBehaviour
{
   
    Rigidbody2D Rb;
    [SerializeField] GameObject Boommm;
    [SerializeField] GameObject Bumbum;
    Heromovement Hero;
    Vector2 Target;
    [SerializeField] float Speed=2f;
    void Start()
    {
        Hero = FindObjectOfType<Heromovement>();
        Rb = GetComponent<Rigidbody2D>();

        StartCoroutine(destroyThis());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shuruken")
        {
            Instantiate(Bumbum, transform.position, Quaternion.identity);
            destroyThis();

        }
    }

    private void Update()
    {

        Target = new Vector2(Hero.transform.position.x, Hero.transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);


    }


    private void destoyThis()
    {


        //Destroy(this.gameObject, 1f);
    }
    

    IEnumerator destroyThis()
    {
        GameObject Hero = GameObject.Find("Hero");
       

        yield return new WaitForSeconds(3f);
        destroyThis();
        Instantiate(Boommm, transform.position, Quaternion.identity);

    }
}
