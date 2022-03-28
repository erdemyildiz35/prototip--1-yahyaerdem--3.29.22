using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCloseiestEnemy : MonoBehaviour
{
    //süper yetenetk

    LayerMask EnemyLayer;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


   void SuperSkillShot()
    {
     



        Collider2D[] Enemyrange1 = Physics2D.OverlapCircleAll(transform.position, 3.2f, EnemyLayer);
      if(Enemyrange1!=null)
        {
            foreach (Collider2D Eneny in Enemyrange1)
            {
               transform.position= Eneny.transform.position;
                Eneny.GetComponent<Enemy>().TakeDamage(20);
            }


        }




       


    }

}
