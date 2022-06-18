using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiyalogSistemi : MonoBehaviour
{
    [SerializeField] Canvas DialogCanvas;
    [SerializeField] private TextMeshProUGUI TextLabel;
    [SerializeField] Image KeyEventImage;
    GameObject TextObject;
    [SerializeField] private GameObject UIElement;
    [SerializeField] private string[] Text = { "", "", "" };
    bool canSpeak;
    string TriggerMessage;
    int i = 0, TextLenght;
    Button EventKey;


    private void Start()
    {
        /* if (GameObject.Find("DialogCanvas"))
          {
              DialogCanvas = GameObject.Find("Dialog").GetComponent<Canvas>();

          }
         if(GameObject.Find("KeyEventImage"))
          {
              KeyEventImage = GameObject.Find("KeyEventImage").GetComponent<Image>();

          }
        */
        EventKey = GameObject.Find("EventKey").GetComponent<Button>();
        TextLenght = Text.Length;
        DialogCanvas.enabled = false;
        KeyEventImage.enabled = false;
        EventKey.onClick.AddListener(delegate { KeyInputs("EventKey"); });
    }

   

    void KeyInputs(string doEvent)
    {
        if(doEvent == "EventKey")
        {
            if(canSpeak)
            {
                Dialog();

            }


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSpeak = true;
            KeyEventImage.enabled = true;
            TextLabel.text = TriggerMessage;
            i = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            KeyEventImage.enabled = false;
            canSpeak = false;
            DialogCanvas.enabled = false;
            i = 0;
        }
    }

    void Dialog()
    {
        DialogCanvas.enabled = true;

        if (!(i == TextLenght))
        {
            TextLabel.text = Text[i];
            i++;
        }
        else
        {
            canSpeak = false;
            i = 0;
            DialogCanvas.enabled = false;
        }
    }
}
