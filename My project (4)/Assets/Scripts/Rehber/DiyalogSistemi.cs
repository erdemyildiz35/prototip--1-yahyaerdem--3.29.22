using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiyalogSistemi : MonoBehaviour
{
    Canvas DialogCanvas;
    [SerializeField] private TextMeshProUGUI TextLabel;
    GameObject TextObject;
    [SerializeField] private GameObject UIElement;
    [SerializeField] private string[] Text = { "", "", "" };
    bool canSpeak;
    string TriggerMessage;
    int i = 0, TextLenght;


    private void Start()
    {
        DialogCanvas = GameObject.Find("Dialog").GetComponent<Canvas>();
        TextObject = GameObject.Find("DialogCanvas");
        TextLabel = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        DialogCanvas.enabled = false;

        TextLenght = Text.Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSpeak)
        {
            DialogCanvas.enabled = false;
            Dialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSpeak = true;
            DialogCanvas.enabled = true;
            TextLabel.text = TriggerMessage;
            i = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
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
