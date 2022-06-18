using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTrapWithEffecktro : MonoBehaviour
{

    [SerializeField] GameObject BİgTrapEffect;

    void Start()
    {
        BİgTrapEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            BİgTrapEffect.SetActive(true);

        }
    }
}
