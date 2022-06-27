using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerGenerator : MonoBehaviour
{
    public List<GameObject> OrbGO;
    public GameObject Pointer;
    public Transform PointerTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Orbs")!=null)
        {
            PointerTransform.position = new Vector3(PointerTransform.position.x + 3, PointerTransform.position.y, 0);
            var Orbs = GameObject.Find("Orbs");

            foreach (Transform child in Orbs.transform)
            {
                var go = Instantiate(Pointer, PointerTransform.position, Quaternion.identity);
                go.transform.parent = gameObject.transform;
                Pointer.GetComponent<Pointer>().Target = child;
            }
        } 
    }
}
