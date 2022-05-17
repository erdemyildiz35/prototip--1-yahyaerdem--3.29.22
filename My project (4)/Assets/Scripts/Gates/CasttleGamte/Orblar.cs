using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orblar : MonoBehaviour
{

    [SerializeField] GameObject CanvasObject;
    Castlemanage4r Castlemanager;

    void Start()
    {
        Castlemanager = FindObjectOfType<Castlemanage4r>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            CanvasObject.SetActive(false);


        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            CanvasObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {

                StartCoroutine(WaitAndDestroy());

            }


        }
    }



    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2f);

        if (Input.GetKey(KeyCode.E))
        {
            Castlemanager.orbClear();
            Destroy(this.gameObject);
        }
    }

}
