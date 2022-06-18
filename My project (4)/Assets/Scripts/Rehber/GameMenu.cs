using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    [SerializeField] GameObject Menu;
   
    public void Stop()
    {

        Time.timeScale=0;

        Menu.SetActive(true);

    }

    public void Contunieo()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);

    }

    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");


    }

    public void quitt()
    {

        Application.Quit();

    }
}
