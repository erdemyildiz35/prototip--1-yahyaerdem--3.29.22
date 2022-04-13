using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button Music, Sfx, Apply;
    public Slider MusicSlider, SfxSlider;
    public GameObject SettingsWindow;
    public Image MusicDisable, SfxDisable;
    public float MusicVolume, TempMusicVolume, SfxVolume, TempSfxVolume;
    public bool MusicEnabled = true, SfxEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        Music = GameObject.Find("Music").GetComponent<Button>();
        Sfx = GameObject.Find("Sfx").GetComponent<Button>();
        Apply = GameObject.Find("Apply").GetComponent <Button>();

        MusicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        SfxSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();

        MusicDisable = GameObject.Find("MusicDisable").GetComponent<Image>();
        SfxDisable = GameObject.Find("SfxDisable").GetComponent<Image>();

        SettingsWindow = GameObject.Find("SettingsWindow");

        TempMusicVolume = MusicVolume;
        TempSfxVolume = SfxVolume;

        MusicSlider.onValueChanged.AddListener(delegate { MusicSliderValue(); });
        SfxSlider.onValueChanged.AddListener(delegate { SfxSliderValue(); });
        Music.onClick.AddListener(MusicButton);
        Sfx.onClick.AddListener(SfxButton);
        Apply.onClick.AddListener(ApplyEvent);

        MusicSlider.value = 100f;
        SfxSlider.value = 100f;
        SfxDisable.enabled = false;
        MusicDisable.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MusicSliderValue()
    {
        MusicVolume = (float)MusicSlider.value;
    }

    void SfxSliderValue()
    {
        SfxVolume = (float)SfxSlider.value;
    }

    void MusicButton()
    {
        if (MusicEnabled)
        {
            MusicDisable.enabled = true;
            MusicEnabled = false;
            TempMusicVolume = MusicVolume;
            MusicVolume = 0;
        }
        else
        {
            MusicDisable.enabled = false;
            MusicEnabled = true;
            MusicVolume = TempMusicVolume;
        }
        MusicSlider.value = MusicVolume;
    }

    void SfxButton()
    {
        if (SfxEnabled)
        {
            SfxDisable.enabled = true;
            SfxEnabled = false;
            TempSfxVolume = SfxVolume;
            SfxVolume = 0;
        }
        else
        {
            SfxDisable.enabled = false;
            SfxEnabled = true;
            SfxVolume = TempSfxVolume;
        }
        SfxSlider.value = SfxVolume;
    }

    void ApplyEvent()
    {
        SettingsWindow.SetActive(false);
    }

}
