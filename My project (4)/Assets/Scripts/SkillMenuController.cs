using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillMenuController : MonoBehaviour
{

    public Skills TempSkills;
    public List<Image> StrImages, AgiImages, StaImages;
    public Button StrPlus, StrMinus, AgiPlus, AgiMinus, StaPlus, StaMinus, CancelButton, ApplyButton, ContinueButton, MainMenu;
    public Text StatsText, HealthText, PunchDamageText, KickDamageText, AttackSpeedText, LuckText;
    public Skills skills;
    public SaveSystem SaveSystem;

    private void Awake()
    {
        TempSkills = GameObject.Find("TempSkills").GetComponent<Skills>();
        skills = GetComponent<Skills>();

        CancelButton = GameObject.Find("CancelButton").GetComponent<Button>();
        ApplyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        MainMenu = GameObject.Find("MainMenuButton").GetComponent<Button>();

        StrPlus = GameObject.Find("Strength+").GetComponent<Button>();
        StrMinus = GameObject.Find("Strength-").GetComponent<Button>();
        AgiPlus = GameObject.Find("Agility+").GetComponent<Button>();
        AgiMinus = GameObject.Find("Agility-").GetComponent<Button>();
        StaPlus = GameObject.Find("Stamina+").GetComponent<Button>();
        StaMinus = GameObject.Find("Stamina-").GetComponent<Button>();

        SaveSystem = GetComponent<SaveSystem>();

    }

    private void Start()
    {
        TempSkills.skillpoints = skills.skillpoints;

        StrPlus.onClick.AddListener(ChangePlusStr);
        StrMinus.onClick.AddListener(ChangeMinusStr);

        AgiPlus.onClick.AddListener(ChangePlusAgi);
        AgiMinus.onClick.AddListener(ChangeMinusAgi);

        StaPlus.onClick.AddListener(ChangePlusSta);
        StaMinus.onClick.AddListener(ChangeMinusSta);

        ApplyButton.onClick.AddListener(ChangeSkillValue);

        skillBar();
    }

    void skillBar()
    {
        //ShowStatsTexts();
        MinusButton();
        PlusButton();

        for (int i = 0; i < 10; i++)
        {
            if (i < skills.str + TempSkills.str)
            {
                StrImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                StrImages[i].GetComponent<Image>().enabled = false;
            }

            if (i < skills.agi + TempSkills.agi)
            {
                AgiImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                AgiImages[i].GetComponent<Image>().enabled = false;
            }


            if (i < skills.sta + TempSkills.sta)
            {
                StaImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                StaImages[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    void otherButtons()
    {
        CancelButton.gameObject.GetComponent<Button>().interactable = false;
        ApplyButton.gameObject.GetComponent<Button>().interactable = false;
        ContinueButton.gameObject.GetComponent<Button>().interactable = false;

        if (TempSkills.str + TempSkills.sta + TempSkills.str > 0)
        {
            ApplyButton.gameObject.GetComponent<Button>().interactable = true;
            CancelButton.gameObject.GetComponent<Button>().interactable = true;
        }

        //if() //Devam etme butonu bu ekrana bölüm sonundan gelindiyse olacak eğer menüden girildiyse o buton çalışmayacak
    }

    void MinusButton()
    {
        StrMinus.gameObject.GetComponent<Button>().interactable = false;
        AgiMinus.gameObject.GetComponent<Button>().interactable = false;
        StaMinus.gameObject.GetComponent<Button>().interactable = false;

        if (TempSkills.str > 0)
        {
            StrMinus.gameObject.GetComponent<Button>().interactable = true;
        }

        if (TempSkills.agi > 0)
        {
            AgiMinus.gameObject.GetComponent<Button>().interactable = true;
        }

        if (TempSkills.sta > 0)
        {
            StaMinus.gameObject.GetComponent<Button>().interactable = true;
        }

    }

    void PlusButton()
    {
        StrPlus.gameObject.GetComponent<Button>().interactable = false;
        AgiPlus.gameObject.GetComponent<Button>().interactable = false;
        StaPlus.gameObject.GetComponent<Button>().interactable = false;

        if (TempSkills.skillpoints > 0)
        {
            if (skills.str + TempSkills.str < 10)
            {
                StrPlus.gameObject.GetComponent<Button>().interactable = true;
            }

            if (skills.agi + TempSkills.agi < 10)
            {
                AgiPlus.gameObject.GetComponent<Button>().interactable = true;
            }

            if (skills.sta + TempSkills.sta < 10)
            {
                StaPlus.gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ChangePlusStr()
    {
        if (TempSkills.str < 10 - skills.str)
        {
            TempSkills.str += 1;
            TempSkills.skillpoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusStr()
    {
        if (TempSkills.str > 0)
        {
            TempSkills.str -= 1;
            TempSkills.skillpoints += 1;
            skillBar();
        }
    }

    public void ChangePlusAgi()
    {
        if (TempSkills.agi < 10 - skills.agi)
        {
            TempSkills.agi += 1;
            TempSkills.skillpoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusAgi()
    {
        if (TempSkills.agi > 0)
        {
            TempSkills.agi -= 1;
            TempSkills.skillpoints += 1;
            skillBar();
        }
    }

    public void ChangePlusSta()
    {
        if (TempSkills.sta < 10 - skills.sta)
        {
            TempSkills.sta += 1;
            TempSkills.skillpoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusSta()
    {
        if (TempSkills.sta > 0)
        {
            TempSkills.sta -= 1;
            TempSkills.skillpoints += 1;
            skillBar();
        }
    }

    public void ChangeSkillValue()
    {
        skills.str += TempSkills.str;
        skills.agi += TempSkills.agi;
        skills.sta += TempSkills.sta;

        TempSkills.str = 0;
        TempSkills.agi = 0;
        TempSkills.sta = 0;

        skills.skillpoints = TempSkills.skillpoints;

        //skills.Gold = saveData.Gold;
        SaveSystem.save();
        skillBar();
    }




    // X butonuna basılırsa sayfa tekrardan yüklenebilir.


}
