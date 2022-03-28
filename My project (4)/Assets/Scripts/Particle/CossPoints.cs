using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CossPoints : MonoBehaviour
{

    float Timer = 0f;
    [SerializeField] GameObject Cosss;

    
    void Start()
    {
        
    }

   
    void Update()
    {
        Timer += Time.fixedDeltaTime;


        if (Timer >= 60f)
        {
            Timer = 0f;
            Instantiate(Cosss, transform.position, Quaternion.identity);


        }

        
    }
}
