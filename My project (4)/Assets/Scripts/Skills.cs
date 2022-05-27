using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Skills : MonoBehaviour
{

    public int str;
    public int agi;
    public int sta;
    public int skillpoints;
    public int Exp;
    public int PlayerLevel;
    public int Gold;
    public int SwordUpgradeLevel;
    public int ArmorUpgradeLevel;
    public int CurrentLevel;
    public Slider ExpSlider;
    public TextMeshProUGUI LevelText,GoldText;
    public int IceComplete, CastleComplete, DesertComplete;

    //public int currentStageLevel;

    private void Start()
    {
        ExpSlider = GameObject.Find("Exp").GetComponent<Slider>();
        LevelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        GoldText = GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>();
        GainExp(0);
        CalculateGold();
    }

    public void GainExp(int exp)
    {
        Exp += exp;
        if (Exp >= 100)
        {
            PlayerLevel++;
            skillpoints++;
            Exp -= 100;
        }
        ExpSlider.value = Exp;
        LevelText.text = "Level : " + PlayerLevel;
    }

    public void CalculateGold()
    {
        GoldText.text = Gold + " Gold";
    }
}
