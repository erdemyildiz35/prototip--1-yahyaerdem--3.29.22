using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiyalogSistemi : MonoBehaviour
{
    public Image row1, row3, row5, row7;
    public Canvas DialogCanvas;
    [SerializeField] private TextMeshProUGUI TextLabel;
    AventurerMove Hero;
    GameObject TextObject;
    [SerializeField] private GameObject UIElement;
    [SerializeField] private string[] Text = { "", "", "" };
    private float mesafe = 2.5f;
    public string TriggerMessage;
    public int i = 0, TextLenght;
    public bool canSpeak, DialogEnd;
    public int LineCount;


    private void Start()
    {
        row1 = GameObject.Find("Row1").GetComponent<Image>();
        row3 = GameObject.Find("Row3").GetComponent<Image>();
        row5 = GameObject.Find("Row5").GetComponent<Image>();
        row7 = GameObject.Find("Row7").GetComponent<Image>();

        DialogCanvas = GameObject.Find("Dialog").GetComponent<Canvas>();

        TextObject = GameObject.Find("DialogCanvas");

        TextLabel = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        DialogCanvas.enabled = false;
        row1.enabled = false;
        row3.enabled = false;
        row5.enabled = false;
        row7.enabled = false;

        TextLenght = Text.Length;

        Hero = GameObject.Find("Hero").GetComponent<AventurerMove>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            row1.enabled = false;
            Dialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TextObject.transform.position = new Vector3(row1.transform.position.x + 58f, row1.transform.position.y + 123f);
            row1.enabled = true;
            DialogCanvas.enabled = true;
            TextLabel.text = TriggerMessage;
            i = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogCanvas.enabled = false;
        }
    }

    void Dialog()
    {
        DialogCanvas.enabled = true;
        DialogEnd = false;

        

        if (!(i == TextLenght))
        {
            TextLabel.text = Text[i];
            Debug.Log("Satır sayısı " + TextLabel.textInfo.lineCount);

            if (TextLabel.textInfo.lineCount > 0 && TextLabel.textInfo.lineCount < 2 && !DialogEnd)
            {
                TextObject.transform.position = new Vector3(row1.transform.position.x + 58f, row1.transform.position.y + 123f);
                row3.enabled = false;
                row5.enabled = false;
                row1.enabled = true;
            }
            else if (TextLabel.textInfo.lineCount >= 2 && TextLabel.textInfo.lineCount <= 4 && !DialogEnd)
            {
                TextObject.transform.position = new Vector3(row1.transform.position.x + 58f, row1.transform.position.y + 147f);
                row1.enabled = false;
                row5.enabled = false;
                row3.enabled = true;
            }
            else if (TextLabel.textInfo.lineCount > 4 && !DialogEnd)
            {
                TextObject.transform.position = new Vector3(row1.transform.position.x + 58f, row1.transform.position.y + 173f);
                row1.enabled = false;
                row3.enabled = false;
                row5.enabled = true;
            }
            i++;
        }

        else
        {
            i = 0;
            DialogCanvas.enabled = false;
            row3.enabled = false;
            row5.enabled = false;
            row1.enabled = false;
            DialogEnd = true;
        }
    }
}
