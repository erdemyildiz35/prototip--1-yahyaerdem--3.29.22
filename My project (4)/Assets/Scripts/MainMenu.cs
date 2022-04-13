using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button NewGame, LoadGame, Settings, Store, Exit, NoAds;
    SaveSystem saveSystem;
    GameObject SettingsWindow;
    Skills skills;

    // Start is called before the first frame update
    void Start()
    {
        NewGame = GameObject.Find("NewGame").GetComponent<Button>();
        LoadGame = GameObject.Find("LoadGame").GetComponent<Button>();
        Settings = GameObject.Find("Settings").GetComponent<Button>();
        Store = GameObject.Find("Store").GetComponent<Button>();
        Exit = GameObject.Find("Exit").GetComponent<Button>();
        NoAds = GameObject.Find("NoAds").GetComponent<Button>();
        SettingsWindow = GameObject.Find("SettingsWindow");
        saveSystem = GetComponent<SaveSystem>();
        skills = GetComponent<Skills>();

        SettingsWindow.SetActive(false);

        NewGame.onClick.AddListener(NewGameEvent);
        LoadGame.onClick.AddListener(LoadGameEvent);
        Settings.onClick.AddListener(SettingsEvent);
        Store.onClick.AddListener(StoreEvent);
        Exit.onClick.AddListener(ExitEvent);
        NoAds.onClick.AddListener(NoAdsEvent);

        saveSystem.Load();

        if (skills.CurrentLevel == 0 )
        {
            LoadGame.interactable = false;
        }
    }

    void NewGameEvent()
    {
        saveSystem.NewGame();
        SceneManager.LoadScene("MainLevel");
    }

    void LoadGameEvent()
    {
        saveSystem.Load();
        SceneManager.LoadScene("MainLevel");
    }

    void SettingsEvent()
    {
        SettingsWindow.SetActive(true);
    }

    void StoreEvent()
    {

    }

    void ExitEvent()
    {
        Application.Quit();
    }

    void NoAdsEvent()
    {

    }
}
