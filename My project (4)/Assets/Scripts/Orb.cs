using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Canvas KeyEventCanvas;
    public OrbsControl orbsControl;
    Castlemanage4r manager;
    Vector3 TempScale;
    public bool KeyPress = false, CoroutineEnd = true;
    public float IncreaseControl;
    public GameObject Blood;
    private AudioSource orbsSource;
    [SerializeField] AudioClip BlowUp;

    // Start is called before the first frame update
    void Start()
    {
        orbsSource = GetComponent<AudioSource>();
        orbsControl = transform.parent.GetComponent<OrbsControl>();
        KeyEventCanvas = transform.GetChild(0).gameObject.GetComponent<Canvas>();
        KeyEventCanvas.enabled = false;
        manager = FindObjectOfType<Castlemanage4r>();
        KeyEventCanvas.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        TempScale = gameObject.transform.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!KeyEventCanvas.enabled)
        {
            KeyEventCanvas.enabled = true;
            KeyEventCanvas.transform.position = gameObject.transform.position;
        }
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
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
            orbsSource.PlayOneShot(BlowUp);
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
                gameObject.SetActive(false);     
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        KeyEventCanvas.enabled = false;
    }
}