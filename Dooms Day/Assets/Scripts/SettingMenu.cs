using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider FullVolumeSlider, BackgroundVolumeSlider, EffectVolume1Slider, EffectVolume2Slider;

    private bool isStart1, isStart2, isStart3, isStart4;

    // Start is called before the first frame update
    void Start()
    {
        isStart1 = true;
        isStart2 = true;
        isStart3 = true;
        isStart4 = true;
        FullVolumeSlider.value = DataBase.FullVolume;
        BackgroundVolumeSlider.value = DataBase.BackgroundVolume;
        EffectVolume1Slider.value = DataBase.EffectVolume1;
        EffectVolume2Slider.value = DataBase.EffectVolume2;
    }

    public void ChangeFullVolume()
    {
        if(isStart1){
            isStart1 = false;
        }
        else{
            AudioListener.volume = FullVolumeSlider.value;
            DataBase.FullVolume = FullVolumeSlider.value;
        }
    }

    public void ChangeBackgroundVolume()
    {
        if(isStart2){
            isStart2 = false;
        }
        else{
            GameObject.Find("StartBGM").GetComponent<AudioSource>().volume = BackgroundVolumeSlider.value;
            DataBase.BackgroundVolume = BackgroundVolumeSlider.value;
        }
    }

    public void ChangeEffectVolume1()
    {
        if(isStart3){
            isStart3 = false;
        }
        else{
            DataBase.EffectVolume1 = EffectVolume1Slider.value;
        }
    }

    public void ChangeEffectVolume2()
    {
        if(isStart4){
            isStart4 = false;
        }
        else{
            DataBase.EffectVolume2 = EffectVolume2Slider.value;
        }
    }
}
