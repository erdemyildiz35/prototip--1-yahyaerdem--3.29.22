using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastGate : MonoBehaviour
{
    [SerializeField] GameObject Portal;
    [SerializeField] bool isGateActive, isTrigger;
    [SerializeField] Collider2D Collider;
    [SerializeField] Canvas NextUpdate;
    Skills skills;
    // Start is called before the first frame update
    void Start()
    {
        Collider = gameObject.GetComponent<Collider2D>();
        skills = GameObject.Find("Hero").GetComponent<Skills>();
        NextUpdate = GameObject.Find("NextUpdate").GetComponent<Canvas>();
        Portal = GameObject.Find("LastGate").transform.GetChild(1).gameObject;

        NextUpdate.enabled = false;
        Portal.SetActive(false);
        isGateActive = false;
        isTrigger = false;

        if (!isGateActive)
        {
            GateActive();
        }
    }

    // Update is called once per frame
    void Update()
    { 
        KeyEvent();
    }

    public void GateActive()
    {
        if (skills.CastleComplete == 1 && skills.IceComplete == 1 && skills.DesertComplete == 1)
        {
            isGateActive = true;
            Portal.SetActive(true);
        }
    }

    void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            SceneManager.LoadScene("finalStage");
            //new scene load
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger && isGateActive)
        {
            isTrigger = true;
            NextUpdate.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
