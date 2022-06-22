using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    AdventurerHealth Adventurer;
    [SerializeField] GameObject PuffEffeckts;
    [SerializeField] Transform blowPoint;

    [SerializeField] List<GameObject> chestIndex;
    [SerializeField] Sprite OpendSprite;

    int Number;
    public bool isChestOppend=false;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

   public void OpenChest()
    {
        GetComponent<SpriteRenderer>().sprite = OpendSprite;
        Instantiate(PuffEffeckts, blowPoint.position, Quaternion.identity);
        Number = Random.Range(0, 2);
        Invoke("CratefromChest", .5f);

        isChestOppend = true;
        Invoke("Destroy", 10f);

    }

    private void Destroy()
    {
        Destroy(this.gameObject);
        
    }
    void CratefromChest()
    {
        Instantiate(chestIndex[Number], blowPoint.position, Quaternion.identity);


    }
}
