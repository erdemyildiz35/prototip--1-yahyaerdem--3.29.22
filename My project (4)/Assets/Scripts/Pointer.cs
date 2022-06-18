using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform Target;
    public float HideDistance;
    public float distance, maxDistance;
    public SpriteRenderer Arrow;

    // Update is called once per frame
    private void Start()
    {
        Arrow = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        var dir = Target.position - transform.position;

        distance = Vector2.Distance(transform.position, Target.position);

        if (distance > maxDistance)
        {
            Arrow.color = new Color(1, 1, 1, 0);
        }
        else
        {
            Arrow.color = new Color(1, 1, 1, ((maxDistance - distance) * 5.1f) / 255);
        }

        if (dir.magnitude < HideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
