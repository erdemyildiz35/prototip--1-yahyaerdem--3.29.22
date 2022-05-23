using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGates : MonoBehaviour
{
    [SerializeField] int OrbsCount;
    [SerializeField] OrbsControl orbsControl;
    [SerializeField] GameObject Orbs;
    [SerializeField] GameObject Portal;
    [SerializeField] bool isGateActive, isTrigger;
    [SerializeField] Collider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        orbsControl = GameObject.Find("Orbs").GetComponent<OrbsControl>();
        Orbs = GameObject.Find("Orbs");
        Collider = gameObject.GetComponent<Collider2D>();

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
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            Debug.Log("keyevent");
            SceneManager.LoadScene("MainLevel");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger && isGateActive)
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
