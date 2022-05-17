using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public GameObject SawCollider;
    public Quaternion target;
    public float smooth = 100f;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        SawCollider = transform.GetChild(0).gameObject;
        SawCollider.transform.rotation = Quaternion.Euler(0, 0, 1);
        target = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SawRotate();
    }

    void SawRotate()
    {
        if (!active)
        {
            target = Quaternion.Euler(0, 0, 0);
            SawCollider.transform.rotation = Quaternion.RotateTowards(SawCollider.transform.rotation, target, smooth * Time.deltaTime);
        }
        else
        {
            target = Quaternion.Euler(0, 0, 180);
            SawCollider.transform.rotation = Quaternion.RotateTowards(SawCollider.transform.rotation, target, smooth * Time.deltaTime);

        }

    }

    public void Allertobservers(string message)
    {
        if (message == "AnimationStart")
        {
            SawCollider.transform.rotation = Quaternion.Euler(0, 0, 1);
            active = true;
        }
        if (message == "AnimationEnd")
        {
            SawCollider.transform.rotation = Quaternion.Euler(0, 0, 179);
            active = false;
        }
    }
}