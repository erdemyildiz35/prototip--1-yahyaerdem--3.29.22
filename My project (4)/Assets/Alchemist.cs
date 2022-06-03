using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alchemist : MonoBehaviour
{
    public GameObject GoSkillMenu;
    public bool isTrigger;
    public ButtonEvent Buttonevent;
    // Start is called before the first frame update
    void Start()
    {
        Buttonevent = GameObject.Find("EventKey").GetComponent<ButtonEvent>();
        GoSkillMenu = GameObject.Find("GoSkillMenu");
        GoSkillMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    void KeyInput()
    { 
        if (Buttonevent.keydown && isTrigger)
        {
            SceneManager.LoadScene("SkillMenu");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
        GoSkillMenu.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        GoSkillMenu.SetActive(false);
    }
}
