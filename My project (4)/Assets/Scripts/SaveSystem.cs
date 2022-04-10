using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public Skills skill;
    // Start is called before the first frame update
    void Start()
    {
        skill = GameObject.Find("Hero").GetComponent<Skills>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void save()
    {
        PlayerPrefs.SetInt("str", skill.str);
        PlayerPrefs.SetInt("agi", skill.agi);
        PlayerPrefs.SetInt("sta", skill.sta);
        PlayerPrefs.SetInt("skillpoints", skill.skillpoints);
        PlayerPrefs.SetInt("Exp", skill.Exp);
        PlayerPrefs.SetInt("PlayerLevel", skill.PlayerLevel);
        PlayerPrefs.SetInt("Gold", skill.Gold);
        PlayerPrefs.SetInt("SwordUpgradeLevel", skill.SwordUpgradeLevel);
        PlayerPrefs.SetInt("ArmorUpgradeLevel", skill.ArmorUpgradeLevel);

    }

    public void Load()
    {
        skill.str = PlayerPrefs.GetInt("str");
        skill.agi = PlayerPrefs.GetInt("agi");
        skill.sta = PlayerPrefs.GetInt("sta");
        skill.skillpoints = PlayerPrefs.GetInt("skillpoints");
        skill.Exp = PlayerPrefs.GetInt("Exp");
        skill.PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        skill.Gold = PlayerPrefs.GetInt("Gold");
        skill.SwordUpgradeLevel = PlayerPrefs.GetInt("SwordUpgradeLevel");
        skill.ArmorUpgradeLevel = PlayerPrefs.GetInt("ArmorUpgradeLevel");
    }

    public void NewGame()
    {
        skill.str = 0;
        skill.agi = 0;
        skill.sta = 0;
        skill.skillpoints = 0;
        skill.Exp = 0;
        skill.PlayerLevel = 0;
        skill.SwordUpgradeLevel = 0;
        skill.ArmorUpgradeLevel = 0;

        save();
    }
}
