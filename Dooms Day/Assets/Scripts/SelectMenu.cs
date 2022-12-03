using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenu : MonoBehaviour
{
    private Image skillimg;
    private GameObject FirstSkill;
    public Sprite sprite00, sprite01, sprite02;

    public TMP_Text FirstSkillName, FirstSkillDesc;

    // Start is called before the first frame update
    void Start()
    {
        FirstSkill = GameObject.Find("FirstSkill");
        skillimg = FirstSkill.GetComponent<Image>();
        switch(DataBase.characterID)
        {
            case 0: {
                skillimg.sprite = sprite00;
                FirstSkillName.text = "光翼奔馳";
                FirstSkillDesc.text = "將移動速度提升至2倍，持續2秒。CD: 10s";
                break;
            }
            case 1: {
                skillimg.sprite = sprite01;
                FirstSkillName.text = "極冰束縛";
                FirstSkillDesc.text = "隨機將一名其他玩家束縛在原地，持續2秒。CD: 8s";
                break;
            }
            case 2: {
                skillimg.sprite = sprite02;
                FirstSkillName.text = "黑暗新星";
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，施法0.5秒。CD: 12s";
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSkill()
    {
        DataBase.characterID = (DataBase.characterID + 1) % 3;
        switch(DataBase.characterID)
        {
            case 0: {
                skillimg.sprite = sprite00;
                FirstSkillName.text = "光翼奔馳";
                FirstSkillDesc.text = "將移動速度提升至2倍，持續2秒。CD: 10s";
                break;
            }
            case 1: {
                skillimg.sprite = sprite01;
                FirstSkillName.text = "極冰束縛";
                FirstSkillDesc.text = "隨機將一名其他玩家束縛在原地，持續2秒。CD: 8s";
                break;
            }
            case 2: {
                skillimg.sprite = sprite02;
                FirstSkillName.text = "黑暗新星";
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，施法0.5秒。CD: 12s";
                break;
            }
        }
    }

    public void PreviousSkill()
    {
        DataBase.characterID = (DataBase.characterID + 2) % 3;
        switch(DataBase.characterID)
        {
            case 0: {
                skillimg.sprite = sprite00;
                FirstSkillName.text = "光翼奔馳";
                FirstSkillDesc.text = "將移動速度提升至2倍，持續2秒。CD: 10s";
                break;
            }
            case 1: {
                skillimg.sprite = sprite01;
                FirstSkillName.text = "極冰束縛";
                FirstSkillDesc.text = "隨機將一名其他玩家束縛在原地，持續2秒。CD: 8s";
                break;
            }
            case 2: {
                skillimg.sprite = sprite02;
                FirstSkillName.text = "黑暗新星";
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，施法0.5秒。CD: 12s";
                break;
            }
        }
    }
}
