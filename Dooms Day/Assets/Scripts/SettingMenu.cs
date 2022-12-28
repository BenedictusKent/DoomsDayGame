using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider FullVolumeSlider, BackgroundVolumeSlider, EffectVolume1Slider, EffectVolume2Slider;
    public Toggle ScreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        FullVolumeSlider.value = DataBase.FullVolume;
        BackgroundVolumeSlider.value = DataBase.BackgroundVolume;
        EffectVolume1Slider.value = DataBase.EffectVolume1;
        EffectVolume2Slider.value = DataBase.EffectVolume2;
        ScreenToggle.isOn = DataBase.isFullScreen;
    }

    public void ChangeFullVolume()
    {
        AudioListener.volume = FullVolumeSlider.value;
        DataBase.FullVolume = FullVolumeSlider.value;
    }

    public void ChangeBackgroundVolume()
    {
        GameObject.Find("StartBGM").GetComponent<AudioSource>().volume = BackgroundVolumeSlider.value;
        DataBase.BackgroundVolume = BackgroundVolumeSlider.value;
    }

    public void ChangeEffectVolume1()
    {
        DataBase.EffectVolume1 = EffectVolume1Slider.value;
    }

    public void ChangeEffectVolume2()
    {
        DataBase.EffectVolume2 = EffectVolume2Slider.value;
    }

    public void ChangeScreenMode()
    {
        Screen.fullScreen = ScreenToggle.isOn;
        DataBase.isFullScreen = ScreenToggle.isOn;
    }
}
