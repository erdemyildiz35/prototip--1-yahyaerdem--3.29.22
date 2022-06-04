using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public bool IceGate, CastleGate, DesertGate;
    public Button EventButton;


    private void Start()
    {
        EventButton = GameObject.Find("EventKey").GetComponent<Button>();
        EventButton.onClick.AddListener(delegate { GateSceneLoader(); }) ;
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

    void GateSceneLoader()
    {
        if (IceGate)
        {
            IceGate = false;
            SceneManager.LoadScene("Map3");
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
