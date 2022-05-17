using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Canvas KeyEventCanvas;
    public OrbsControl orbsControl;
    // Start is called before the first frame update
    void Start()
    {
        orbsControl = transform.parent.GetComponent<OrbsControl>();
        KeyEventCanvas = transform.GetChild(0).gameObject.GetComponent<Canvas>();
        KeyEventCanvas.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = true;
        KeyEventCanvas.transform.position = gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            orbsControl.DestroyedOrbs++;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = false;
    }
}