using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ShowAndHideGrid : MonoBehaviour
{
    
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<TilemapRenderer>().sortingOrder = -25;

        }
        else
        {
            this.gameObject.GetComponent<TilemapRenderer>().sortingOrder = 50;
        }
    }
    

}
