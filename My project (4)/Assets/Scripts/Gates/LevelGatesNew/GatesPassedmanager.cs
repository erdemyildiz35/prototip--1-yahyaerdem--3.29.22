using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GatesPassedmanager : MonoBehaviour
{
     public List<Button> levelButtons;

    public List<string> levelNames;
    public List<int> AreTheyPast;
    int i;
    void CleanWaitToHash()
    {
        for(i = 0; i < levelButtons.Count; i++)
        {
            AreTheyPast[i] = 0;
        }

    }

    void GetGatesState()
    {
        for (i = 0; i < levelButtons.Count; i++)
        {
            AreTheyPast[i] = PlayerPrefs.GetInt(levelNames[i]);
        }
        levelButtons[0].interactable = true;
        
        for(i = 0; i < levelNames.Count; i++)
        {

            if (i == levelNames.Count)
            {
                //finalLevelButton Open
                break;
            }
            if (AreTheyPast[i] == 1)
            {
                levelButtons[i+1].enabled = true;
            }
            else
            {
                levelButtons[i+1].enabled = false;
            }


        }


    }

    void PastToLevel(string levelnames)
    {
        SceneManager.LoadScene(levelnames);

    }
}
