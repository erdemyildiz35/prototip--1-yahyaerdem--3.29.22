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

    void Start()
    {
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
            if (PlayerPrefs.GetInt(Arkbirİsimleri[i]) == 1)
            {
                ArkBirButtonlari[i].interactable = true;
            }
            else
            {
                ArkBirButtonlari[i].interactable = false;
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
