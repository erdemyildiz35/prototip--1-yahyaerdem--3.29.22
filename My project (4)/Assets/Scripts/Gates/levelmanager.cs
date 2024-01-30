using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelmanager : MonoBehaviour
{

    public List<Button> ArkBirButtonlari;
    public List<string> Arkbirİsimleri;
    public GameObject ArkOnePanel;
    public int ArkOneLevelIndexes = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("LevelOfGame")){
            ArkOneLevelIndexes = PlayerPrefs.GetInt("LevelOfGame");
        }
        else
        {
            ArkOneLevelIndexes = 1;
        }
       
        ArcChecker();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArcChecker()
    {
        for (int i = 1; i < ArkBirButtonlari.Count; i++)
        {
            if (i < ArkOneLevelIndexes)
            {
                ArkBirButtonlari[i].enabled = true;
            }
            else
            {
                ArkBirButtonlari[i].enabled = false;
            }
        }
      
    }

   public void SendArk(string NameOfScene)
    {
        SceneManager.LoadScene(NameOfScene);
    }

    public void ArkOneAc()
    {
        ArkOnePanel.SetActive(true);
    }

    public void ArkOneKapa()
    {
        ArkOnePanel.SetActive(false);
    }

}
