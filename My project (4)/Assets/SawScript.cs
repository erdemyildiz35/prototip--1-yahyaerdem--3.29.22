using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public Transform SawCollider;
    public float smooth = 50f;
    public Quaternion Target;
    // Start is called before the first frame update

    void Start()
    {
        SawCollider = transform.GetChild(0).gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(SawCollider.localRotation.z);

        //SawRotate(smooth);
    }

    void SawRotate(float speed)
    {


        if (SawCollider.transform.localRotation.z<=-90)
        {
            Debug.Log("<=90");
            Target = Quaternion.Euler(0, 0, 90);
            SawCollider.transform.rotation = Quaternion.RotateTowards(SawCollider.transform.rotation, Target, speed * Time.deltaTime);
        }
        else if(SawCollider.transform.localRotation.z >= 90)
        {
            Debug.Log(">=90");
            Target = Quaternion.Euler(0, 0, -90);
            SawCollider.transform.rotation = Quaternion.RotateTowards(SawCollider.transform.rotation, Target, -speed * Time.deltaTime);
        }
    }
}
