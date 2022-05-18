using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void quitt()
    {

        Application.Quit();

    }
}
