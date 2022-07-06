using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGates : MonoBehaviour
{
    [SerializeField] int OrbsCount;
    [SerializeField] OrbsControl orbsControl;
    [SerializeField] GameObject Orbs;
    [SerializeField] GameObject Portal;
    [SerializeField] bool isGateActive, isTrigger;
    [SerializeField] Collider2D Collider;
    Skills skills;
    SaveSystem saveSystem;
    public ButtonEvent Buttonevent;
    public Button EventButton;
    public Image EventButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        orbsControl = GameObject.Find("Orbs").GetComponent<OrbsControl>();
        Orbs = GameObject.Find("Orbs");
        Collider = gameObject.GetComponent<Collider2D>();
        skills = GameObject.Find("Hero").GetComponent<Skills>();
        saveSystem = GameObject.Find("Hero").GetComponent<SaveSystem>();
        Buttonevent = GameObject.Find("EventKey").GetComponent<ButtonEvent>();
        EventButton = GameObject.Find("EventKey").GetComponent<Button>();
        EventButtonImage = GameObject.Find("EventKey").GetComponent<Image>();

        Portal = GameObject.Find("Portal");

        Portal.SetActive(false);
        isGateActive = false;
        isTrigger = false;

        OrbsCount = Orbs.transform.childCount;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGateActive)
        {
            GateActive();
        }
        KeyEvent();
    }

    public void GateActive()
    {
        if (OrbsCount == orbsControl.DestroyedOrbs)
        {
            Debug.Log("orb count equal");
            isGateActive = true;
            Portal.SetActive(true);
        }
    }

    void KeyEvent()
    {
        if (Buttonevent.keydown && isTrigger)
        {
            LevelChecker();
            saveSystem.save();
            StartCoroutine(GoMainLevel());
        }
    }

    void LevelChecker()
    {
        if(SceneManager.GetActiveScene().name == "Desert")
        {
            skills.DesertComplete = 1;
            PlayerPrefs.SetFloat("Desert", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Castle")
        {
            skills.CastleComplete = 1;
            PlayerPrefs.SetFloat("Castle", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Map3")
        {
            skills.IceComplete = 1;
            PlayerPrefs.SetFloat("Map3", 1);
        }
        else if (SceneManager.GetActiveScene().name == "CastleV2")
        {
            skills.IceComplete = 1;
            PlayerPrefs.SetFloat("Castle", 1);
        }
        else if (SceneManager.GetActiveScene().name == "map3v2")
        {
            skills.IceComplete = 1;
            PlayerPrefs.SetFloat("Map3", 1);
        }
    }

    IEnumerator GoMainLevel()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainLevel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventButton.enabled = true;
        EventButtonImage.enabled = true;
        if (!isTrigger && isGateActive)
        {
            isTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EventButton.enabled = false;
        EventButtonImage.enabled = false;
        isTrigger = false;
    }
}
