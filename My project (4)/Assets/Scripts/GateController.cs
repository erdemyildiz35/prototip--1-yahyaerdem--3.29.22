using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public bool IceGate, CastleGate, DesertGate,bigCastlemap,bigIceMap,Mainlevel;
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
                bigCastlemap = false;
                bigIceMap = false;
                Mainlevel = false;
            }
            else if (collision.name == "DesertGate")
            {
                IceGate = false;
                DesertGate = true;
                CastleGate = false;
                bigCastlemap = false;
                bigIceMap = false;
                Mainlevel = false;
            }
            else if (collision.name == "CastleGate")
            {
                IceGate = false;
                DesertGate = false;
                CastleGate = true;
                bigCastlemap = false;
                Mainlevel = false;
                bigIceMap = false;
            }
            else if (collision.name == "BigCastle")
            {
                IceGate = false;
                DesertGate = false;
                CastleGate = false;
                bigCastlemap = true;
                bigIceMap = false;
                Mainlevel = false;
            }
            else if (collision.name == "BigIce")
            {
                IceGate = false;
                DesertGate = false;
                CastleGate = false;
                bigCastlemap = false;
                bigIceMap = true;
                Mainlevel = false;
            }else if(collision.name== "Mainlevel")
            {

                IceGate = false;
                DesertGate = false;
                CastleGate = false;
                bigCastlemap = false;
                bigIceMap = false;
                Mainlevel = true;
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
            bigCastlemap = false;
            bigIceMap = false;
            Mainlevel = false;
        }
    }

    void GateSceneLoader()
    {
        if (IceGate)
        {
            IceGate = false;
            SceneManager.LoadScene("map3v2");
        }
        else if (CastleGate)
        {
            CastleGate = false;
            SceneManager.LoadScene("CastleV2");
        }
        else if (DesertGate)
        {
            DesertGate = false;
            SceneManager.LoadScene("Desert");
        }
        else if (bigCastlemap)
        {
            bigCastlemap = false;
            SceneManager.LoadScene("Castle");
        }
        else if (bigIceMap)
        {
            bigIceMap = false;
            SceneManager.LoadScene("Map3");
        }
        else if (Mainlevel)
        {
            Mainlevel = false;
            SceneManager.LoadScene("MainLevel");
        }
    }

}
