using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiyalogSistemi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextLabel;
    AventurerMove Hero;
    [SerializeField] private GameObject UIElement;
    [SerializeField] private string[] Text = { "", "", "" };
    private float mesafe = 2.5f;
    int i = 0, a;
    private void Start()
    {
        UIElement.SetActive(false);


       
        a = Text.Length;
        Hero = FindObjectOfType<AventurerMove>();

    }
    private void Update()
    {

        if (Vector2.Distance(Hero.transform.position, transform.position) < mesafe)
        {
            UIElement.active = true;


            
            if (Input.GetKeyDown(KeyCode.E))
            {
                TextLabel.text =  Text[i];

                i++;
                if (i == a)
                {
                    i = 0;
                }
            }



        }
        else
        {

            UIElement.active=false;

            TextLabel.text = "Speak with elf";
            i = 0;

        }
    }
}
