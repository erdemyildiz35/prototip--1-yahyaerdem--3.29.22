using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcsUsual : MonoBehaviour
{
    Animator AnimatorOfNpc;
    Vector3 localScale;

    [Header("Değişkenler")]
   [SerializeField] public float Speed=3f;
  [SerializeField]  public Transform pointa, pointb;

   [SerializeField] bool Pos = false;

    void Start()
    {
        AnimatorOfNpc = GetComponent<Animator>();
        localScale = transform.localScale;
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointa.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NpcTurn")
        {
            Debug.Log("çarptı");

            StartCoroutine(waitTrunWalk());
          
        }
    }
    void TrunAround()
    {

        transform.localScale = new Vector3(-localScale.x,localScale.y,localScale.z) ;

    }

    IEnumerator waitTrunWalk()
    {
        AnimatorOfNpc.SetBool("Walk", false);
        TrunAround();
        Debug.Log("girdi");

        yield return new WaitForSeconds(10f);
        AnimatorOfNpc.SetBool("Walk", true);
        if (Pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointa.position, Speed * Time.deltaTime);

       
            Pos = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointb.position, Speed * Time.deltaTime);

          
            Pos = true;
        }

    }


}
