using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour
{
    public bool keydown = false;
    public void buttondown()
    {
        keydown = true;
    }

    public void buttonup()
    {
        keydown=false;
    }
}
