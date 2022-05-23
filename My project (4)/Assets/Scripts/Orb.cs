using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Canvas KeyEventCanvas;
    public OrbsControl orbsControl;
    Castlemanage4r manager;

    // Start is called before the first frame update
    void Start()
    {
        orbsControl = transform.parent.GetComponent<OrbsControl>();
        KeyEventCanvas = transform.GetChild(0).gameObject.GetComponent<Canvas>();
        KeyEventCanvas.enabled = false;
        manager = FindObjectOfType<Castlemanage4r>();
        KeyEventCanvas.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = true;
        KeyEventCanvas.transform.position = gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.E)&& collision.tag == "Player")
        {
            orbsControl.DestroyedOrbs++;
            gameObject.SetActive(false);
            manager.orbClear();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = false;
    }
}