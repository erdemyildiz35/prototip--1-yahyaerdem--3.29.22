using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    BoxCollider2D BlackSmithCollider;
    Canvas UpgradeCanvas, UpgradeFailCanvas;
    Text UpgradeText, ArmorUpgradeText, SwordUpgradeText, SwordLevelText, ArmorLevelText, GoldText, FailText;
    AventurerMove aventurerMove;
    Button ExitButton, SwordUpgradeButton, ArmorUpgradeButton;
    Skills skill;
    Animator animator;
    SaveSystem saveSystem;
    Vector3 UpgradeTextOffset;
    int SwordUpgradePrice, ArmorUpgradePrice;
    float TempSpeed;
    bool UpgradeWindowIsOpen;


    // Start is called before the first frame update
    void Start()
    {
        BlackSmithCollider = GameObject.Find("blacksmith").GetComponent<BoxCollider2D>();
        UpgradeText = GameObject.Find("UpgradeText").GetComponent<Text>();
        UpgradeCanvas = GameObject.Find("UpgradeWindow").GetComponent<Canvas>();

        SwordLevelText = GameObject.Find("SwordLevelText").GetComponent<Text>();
        SwordUpgradeText = GameObject.Find("SwordUpgradePrice").GetComponent<Text>();
        SwordUpgradeButton = GameObject.Find("SwordUpgradeButton").GetComponent<Button>();

        ArmorLevelText = GameObject.Find("ArmorLevelText").GetComponent<Text>();
        ArmorUpgradeText = GameObject.Find("ArmorUpgradePrice").GetComponent<Text>();
        ArmorUpgradeButton = GameObject.Find("ArmorUpgradeButton").GetComponent<Button>();

        GoldText = GameObject.Find("GoldText").GetComponent<Text>();
        FailText = GameObject.Find("FailText").GetComponent<Text>();

        UpgradeFailCanvas = GameObject.Find("UpgradeFail").GetComponent<Canvas>();

        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        skill = GameObject.Find("Hero").GetComponent<Skills>();
        aventurerMove = GameObject.Find("Hero").GetComponent<AventurerMove>();
        animator = GetComponent<Animator>();
        saveSystem = GameObject.Find("Hero").GetComponent<SaveSystem>();

        UpgradeFailCanvas.enabled = false;
        UpgradeCanvas.enabled = false;
        UpgradeText.enabled = false;

        UpgradeTextOffset = new Vector3(transform.position.x , transform.position.y);

        SwordUpgradeButton.onClick.AddListener(SwordUpgrade);
        ArmorUpgradeButton.onClick.AddListener(ArmorUpgrade);
        ExitButton.onClick.AddListener(ExitEvent);

        saveSystem.Load();
        UpgradePriceChange();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && UpgradeText.enabled && !UpgradeWindowIsOpen)
        {
            UpgradeWindowIsOpen = true;
            TempSpeed = aventurerMove.Speed;
            aventurerMove.Speed = 0;
            UpgradeCanvas.enabled = true;
        }

        UpgradeText.transform.position = Camera.main.WorldToScreenPoint(transform.position+UpgradeTextOffset);

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UpgradeText.enabled = true;
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
        if (skill.SwordUpgradeLevel > 0)
        {
            SwordUpgradePrice = (int)(200 * skill.SwordUpgradeLevel * 1.3f);
        }
        else
        {
            SwordUpgradePrice = 200;
        }

        if (skill.ArmorUpgradeLevel > 0)
        {
            ArmorUpgradePrice = (int)(200 * skill.ArmorUpgradeLevel * 1.3f);
        }
        else
        {
            ArmorUpgradePrice = 200;
        }

        SwordUpgradeText.text = SwordUpgradePrice.ToString() + " Gold";
        ArmorUpgradeText.text = ArmorUpgradePrice.ToString() + " Gold";

        SwordLevelText.text = "Level : " + skill.SwordUpgradeLevel.ToString();
        ArmorLevelText.text = "Level : " + skill.ArmorUpgradeLevel.ToString();

        GoldText.text = skill.Gold.ToString() + " Gold";
    }

    void SwordUpgrade()
    {
        if (SwordUpgradePrice <= skill.Gold)
        {
            animator.Play("IdleToWork");
            skill.Gold -= SwordUpgradePrice;
            skill.SwordUpgradeLevel++;
            UpgradePriceChange();
            StartCoroutine(ButtonOnOffIE());
        }
        else
        {
            StartCoroutine(FailTextIE());
        }
    }

    void ArmorUpgrade()
    {
        if (ArmorUpgradePrice <= skill.Gold)
        {
            animator.Play("IdleToWork");
            skill.Gold -= ArmorUpgradePrice;
            skill.ArmorUpgradeLevel++;
            UpgradePriceChange();
            StartCoroutine(ButtonOnOffIE());
        }
        else
        {
            StartCoroutine(FailTextIE());
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

    IEnumerator FailTextIE()
    {
        if (SwordUpgradePrice > skill.Gold)
        {
            FailText.text = "Yeterli Altýn Yok";
        }
        else if (ArmorUpgradePrice > skill.Gold)
        {
            FailText.text = "Yeterli Altýn Yok";
        }
        else
        {
            FailText.text = "Bir hata oluþtu";
        }

        UpgradeFailCanvas.enabled = true;

        yield return new WaitForSeconds(1.0f);

        UpgradeFailCanvas.enabled = false;
    }

    void ExitEvent()
    {
        UpgradeWindowIsOpen = false;
        aventurerMove.Speed = TempSpeed;
        UpgradeCanvas.enabled = false;
        saveSystem.save();
    }
}
