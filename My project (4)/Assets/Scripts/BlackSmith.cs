using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    BoxCollider2D BlackSmithCollider;
    Canvas UpgradeCanvas;
    Text UpgradeText, ArmorUpgradeText, SwordUpgradeText, SwordLevelText, ArmorLevelText;
    AventurerMove aventurerMove;
    Button ExitButton, SwordUpgradeButton, ArmorUpgradeButton;
    Skills skill;
    Animator animator;
    SaveSystem saveSystem;
    int SwordUpgradePrice, ArmorUpgradePrice;
    public int SwordUpgradeLevel, ArmorUpgradeLevel;
    public float TempSpeed;

    // Start is called before the first frame update
    void Start()
    {
        BlackSmithCollider = GameObject.Find("blacksmith").GetComponent<BoxCollider2D>();
        UpgradeText = GameObject.Find("UpgradeText").GetComponent<Text>();
        UpgradeCanvas = GameObject.Find("UpgradeWindow").GetComponent<Canvas>();

        SwordLevelText = GameObject.Find("SwordLevelText").GetComponent <Text>();
        SwordUpgradeText = GameObject.Find("SwordUpgradePrice").GetComponent<Text>();
        SwordUpgradeButton = GameObject.Find("SwordUpgradeButton").GetComponent<Button>();

        ArmorLevelText = GameObject.Find("ArmorLevelText").GetComponent<Text>();
        ArmorUpgradeText = GameObject.Find("ArmorUpgradePrice").GetComponent<Text>();
        ArmorUpgradeButton = GameObject.Find("ArmorUpgradeButton").GetComponent<Button>();

        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        skill = GameObject.Find("Hero").GetComponent<Skills>();
        aventurerMove = GameObject.Find("Hero").GetComponent<AventurerMove>();
        animator = GetComponent<Animator>();
        saveSystem = GetComponent<SaveSystem>();

        UpgradeText.enabled = false;
        UpgradeCanvas.enabled = false;

        SwordUpgradeButton.onClick.AddListener(SwordUpgrade);
        ArmorUpgradeButton.onClick.AddListener(ArmorUpgrade);
        ExitButton.onClick.AddListener(ExitEvent);

        saveSystem.Load();
        UpgradePriceChange();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UpgradeText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                TempSpeed = aventurerMove.Speed;
                aventurerMove.Speed = 0;
                UpgradeCanvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UpgradeText.enabled = false;
        }
    }

    void UpgradePriceChange()
    {   
        if(SwordUpgradeLevel>0)
        {
            SwordUpgradePrice = (int)(200 * SwordUpgradeLevel * 1.3f);
        }
        else
        {
            SwordUpgradePrice = 200;
        }

        if(ArmorUpgradeLevel>0)
        {
            ArmorUpgradePrice = (int)(200 * ArmorUpgradeLevel * 1.3f);
        }
        else
        {
            ArmorUpgradePrice = 200;
        }

        SwordUpgradeText.text = SwordUpgradePrice.ToString() + " Gold";
        ArmorUpgradeText.text = ArmorUpgradePrice.ToString() + " Gold";

        SwordLevelText.text ="Level : "+SwordUpgradeLevel.ToString();
        ArmorLevelText.text ="Level : "+ArmorUpgradeLevel.ToString();
    }

    void SwordUpgrade()
    {
        if (SwordUpgradePrice <= skill.Gold)
        {
            Debug.Log("girdi");
            animator.Play("IdleToWork");
            skill.Gold -= SwordUpgradePrice;
            SwordUpgradeLevel++;
            UpgradePriceChange();
            StartCoroutine(ButtonOnOffIE());
        }
    }

    void ArmorUpgrade()
    {
        if (ArmorUpgradePrice <= skill.Gold)
        {
            animator.Play("IdleToWork");
            skill.Gold -= ArmorUpgradePrice;
            ArmorUpgradeLevel++;
            UpgradePriceChange();
            StartCoroutine(ButtonOnOffIE());
        }
    }

    IEnumerator ButtonOnOffIE()
    {
        SwordUpgradeButton.interactable = false;
        ArmorUpgradeButton.interactable = false;
        ExitButton.interactable = false;

        yield return new WaitForSeconds(2.5f);

        SwordUpgradeButton.interactable = true;
        ArmorUpgradeButton.interactable = true;
        ExitButton.interactable = true;
    }

    void ExitEvent()
    {
        aventurerMove.Speed = TempSpeed;
        UpgradeCanvas.enabled = false;
        saveSystem.save();
    }
}
