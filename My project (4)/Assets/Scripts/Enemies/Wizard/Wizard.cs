using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    Heromovement Hero;
    [SerializeField] GameObject FireBall;
    Animator anime;
    float attackTimer=0;
    void Start()
    {

        Hero = FindObjectOfType<Heromovement>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.fixedDeltaTime;
        if (Vector2.Distance(transform.position, Hero.transform.position) < 8f&&attackTimer>=25f)
        {
            attackTimer = 0;
            StartCoroutine(Fire());


        }

    }
    IEnumerator Fire()
    {
        anime.Play("Fireball");
        Instantiate(FireBall, transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(1.5f);
       

    }

}
