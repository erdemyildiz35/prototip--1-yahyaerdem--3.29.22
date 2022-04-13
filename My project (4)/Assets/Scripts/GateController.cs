using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public bool IceGate, CastleGate, DesertGate;

    private void Update()
    {
        KeyEvent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gates")
        {
            if (collision.name == "IceGate")
            {
                IceGate = true;
                DesertGate = false;
                CastleGate = false;
            }
            else if (collision.name == "DesertGate")
            {
                IceGate = false;
                DesertGate = true;
                CastleGate = false;
            }
            else if (collision.name == "CastleGate")
            {
                IceGate = false;
                DesertGate = false;
                CastleGate = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Gates")
        {
            IceGate = false;
            DesertGate = false;
            CastleGate = false;
        }
    }

    public void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GateSceneLoader();
        }
    }

    void GateSceneLoader()
    {
        if (IceGate)
        {
            IceGate = false;
            SceneManager.LoadScene("Ice");
        }
        else if (CastleGate)
        {
            CastleGate = false;
            SceneManager.LoadScene("Castle");
        }
        else if (DesertGate)
        {
            DesertGate = false;
            SceneManager.LoadScene("Desert");
        }
    }

}
