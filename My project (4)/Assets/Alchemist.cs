using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Alchemist : MonoBehaviour
{
    public GameObject GoSkillMenu;
    public bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
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
