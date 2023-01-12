using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPassManager : MonoBehaviour
{
    [SerializeField] string GecilenLevel;
    [SerializeField] string GecilmisLevel;

    public ButtonEvent Buttonevent;
    public Button EventButton;
    public Image EventButtonImage;

  
    void Start()
    {
     
        GecilmisLevel=SceneManager.GetActiveScene().name;
        Buttonevent = GameObject.Find("EventKey").GetComponent<ButtonEvent>();
        EventButton = GameObject.Find("EventKey").GetComponent<Button>();
        EventButtonImage = GameObject.Find("EventKey").GetComponent<Image>();
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventButton.interactable=true;
            EventButtonImage.enabled=true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventButton.interactable = true;
            EventButtonImage.enabled = true;


            if (Buttonevent.keydown)
            {
                PlayerPrefs.SetInt(GecilmisLevel, 1);
                SceneManager.LoadScene("MainLevel");//main menu next level?
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            EventButton.interactable = false;
            EventButtonImage.enabled = false;
        }
    }


}
