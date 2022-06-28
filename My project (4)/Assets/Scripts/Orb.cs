using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Canvas KeyEventCanvas;
    public OrbsControl orbsControl;
    Vector3 TempScale;
    public bool KeyPress = false, CoroutineEnd = true;
    public float IncreaseControl;
    public GameObject Blood;
    public ButtonEvent Buttonevent;
    Skills playerSkills;
    

    // Start is called before the first frame update
    void Start()
    {
        Buttonevent = GameObject.Find("EventKey").GetComponent<ButtonEvent>();
        orbsControl = transform.parent.GetComponent<OrbsControl>();
        playerSkills = FindObjectOfType<Skills>();
        KeyEventCanvas = transform.GetChild(0).gameObject.GetComponent<Canvas>();
        KeyEventCanvas.enabled = false;
        KeyEventCanvas.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        TempScale = gameObject.transform.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (Buttonevent.keydown)
            {
                if(!KeyPress)
                {
                    KeyPress = true;
                    StartCoroutine(OrbAnimation());
                }          
            }
            else
            {
                KeyPress = false;
                StopCoroutine(OrbAnimation());
                gameObject.transform.localScale = TempScale;
                IncreaseControl = 0;
            }
        }
    }
    

    IEnumerator OrbAnimation()
    {
        while (KeyPress)
        {
           
            Debug.Log("Coroutin");
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.05f, gameObject.transform.localScale.y + 0.05f, 0);
            IncreaseControl += 0.05f;
            if (IncreaseControl > 1f)
            {
                for (int i = 0; i < 8; i++)
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.05f, gameObject.transform.localScale.y - 0.05f, 0);
                    yield return new WaitForSeconds(0.05f);
                }
                IncreaseControl = 0;
            }
            yield return new WaitForSeconds(0.05f);
            if (gameObject.transform.localScale.x >= 4)
            {
                orbsControl.DestroyedOrbs++;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                Blood = Resources.Load<GameObject>("BloodParticle2");
                Instantiate(Blood, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(1.0f);
                playerSkills.GainExp(50);
                playerSkills.Gold += 100;
                gameObject.SetActive(false);     
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = false;
    }
}