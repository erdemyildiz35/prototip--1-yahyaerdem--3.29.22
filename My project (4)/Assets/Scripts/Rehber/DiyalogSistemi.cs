using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiyalogSistemi : MonoBehaviour
{
    [SerializeField] private Text TextLabel;
    Heromovement Hero;
    [SerializeField] private GameObject UIElement;
    [SerializeField] private string[] Text = { "", "", "" };
    private float mesafe = 2.5f;
    int i = 0, a;
    private void Start()
    {
        UIElement.SetActive(false);


        TextLabel.text = "hello Samurai";
        a = Text.Length;
        Hero = FindObjectOfType<Heromovement>();

    }
    private void Update()
    {

        if (Vector2.Distance(Hero.transform.position, transform.position) < mesafe)
        {
            UIElement.active = true;
            


            if (Input.GetKeyDown(KeyCode.F))
            {
                TextLabel.text = (i + 1).ToString() + "-" + Text[i];

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
        }
    }
}
