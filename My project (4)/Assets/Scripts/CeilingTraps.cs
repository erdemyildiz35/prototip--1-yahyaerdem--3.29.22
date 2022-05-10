using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTraps : MonoBehaviour
{
    public GameObject LeftCollider, RightCollider;
    public float smooth = 0.1f;
    public Transform LeftTargetRotation, RightTargetRotation;
    public bool Rotating;
    // Start is called before the first frame update
    void Start()
    {
        LeftCollider = transform.GetChild(0).gameObject;
        RightCollider = transform.GetChild(1).gameObject;

        LeftTargetRotation = GetComponent<Transform>();
        RightTargetRotation = GetComponent<Transform>();

        LeftTargetRotation.localRotation = Quaternion.Euler(0, 0, 0);
        RightTargetRotation.localRotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if(Rotating)
        {
            Rotate();
        }
    }

    void AllertObservers(string message)
    {
        if (message == "Rotate")
        {
            Rotating = true;
        }

        if(message == "RotateEnd")
        {
            Rotating = false;
        }

        if (message == "Closed")
        {
            LeftCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
            RightCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void Rotate()
    {
        LeftCollider.transform.rotation = Quaternion.RotateTowards(LeftCollider.transform.rotation, LeftTargetRotation.localRotation, smooth * Time.deltaTime);
        RightCollider.transform.rotation = Quaternion.RotateTowards(RightCollider.transform.rotation, RightTargetRotation.localRotation, smooth * Time.deltaTime);
    }

}
