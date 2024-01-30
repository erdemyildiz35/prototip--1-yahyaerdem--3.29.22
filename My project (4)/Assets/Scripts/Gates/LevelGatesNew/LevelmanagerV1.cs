using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelmanagerV1 : MonoBehaviour
{
    [SerializeField] string[] LevelNames;
    
    void Start()
    {
        
    }

   
   void isThatLevelOpend(string levelName)
    {
        if(levelName == "1")
        {
            SceneManager.LoadScene("level-1-A1");//Değişecek Level İsmi
        }

        if(PlayerPrefs.HasKey(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
    }

    void PassMainlevel(string levelName)
    {
        PlayerPrefs.SetInt(levelName, 1);
        SceneManager.LoadScene("MainLevelArk");
    }

    void PassArkSelection()
    {
        SceneManager.LoadScene("AraLevel");

    }
}
