using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnlineSelectMenu : MonoBehaviour
{
    private Image skillimg, mapimg;
    private GameObject FirstSkill, Map;
    public Sprite sprite00, sprite01, sprite02, sprite03, sprite04, sprite05;
    //public Sprite mapsprite00, mapsprite01;

    public TMP_Text FirstSkillName, FirstSkillDesc;
    //public TMP_Text MapName, MapDesc;

    // Start is called before the first frame update
    void Start()
    {
        FirstSkill = GameObject.Find("FirstSkill");
        skillimg = FirstSkill.GetComponent<Image>();

        //Map = GameObject.Find("Map");
        //mapimg = Map.GetComponent<Image>();

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
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，如果有碎片則甩給該玩家，施法0.5秒。CD: 12s";
                break;
            }
            case 3: {
                skillimg.sprite = sprite03;
                FirstSkillName.text = "聖光守護";
                FirstSkillDesc.text = "被動技，持有碎片被怪物撞到時，會保護你並將碎片甩給最遠的玩家，能發動2次。";
                break;
            }
            case 4: {
                skillimg.sprite = sprite04;
                FirstSkillName.text = "潛能釋放";
                FirstSkillDesc.text = "被動技，初始移動速度為0.75倍，每10秒增加0.25倍，最多達到1.5倍。";
                break;
            }
            case 5: {
                skillimg.sprite = sprite05;
                FirstSkillName.text = "咒法反饋";
                FirstSkillDesc.text = "被動技，被甩碎片時，有25%機率將其反彈給甩碎片的人。";
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
        DataBase.characterID = (DataBase.characterID + 1) % 6;
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
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，如果有碎片則甩給該玩家，施法0.5秒。CD: 12s";
                break;
            }
            case 3: {
                skillimg.sprite = sprite03;
                FirstSkillName.text = "聖光守護";
                FirstSkillDesc.text = "被動技，持有碎片被怪物撞到時，會保護你並將碎片甩給最遠的玩家，能發動2次。";
                break;
            }
            case 4: {
                skillimg.sprite = sprite04;
                FirstSkillName.text = "潛能釋放";
                FirstSkillDesc.text = "被動技，初始移動速度為0.75倍，每10秒增加0.25倍，最多達到1.5倍。";
                break;
            }
            case 5: {
                skillimg.sprite = sprite05;
                FirstSkillName.text = "咒法反饋";
                FirstSkillDesc.text = "被動技，被甩碎片時，有25%機率將其反彈給甩碎片的人。";
                break;
            }
        }
    }

    public void PreviousSkill()
    {
        DataBase.characterID = (DataBase.characterID + 5) % 6;
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
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，如果有碎片則甩給該玩家，施法0.5秒。CD: 12s";
                break;
            }
            case 3: {
                skillimg.sprite = sprite03;
                FirstSkillName.text = "聖光守護";
                FirstSkillDesc.text = "被動技，持有碎片被怪物撞到時，會保護你並將碎片甩給最遠的玩家，能發動2次。";
                break;
            }
            case 4: {
                skillimg.sprite = sprite04;
                FirstSkillName.text = "潛能釋放";
                FirstSkillDesc.text = "被動技，初始移動速度為0.75倍，每10秒增加0.25倍，最多達到1.5倍。";
                break;
            }
            case 5: {
                skillimg.sprite = sprite05;
                FirstSkillName.text = "咒法反饋";
                FirstSkillDesc.text = "被動技，被甩碎片時，有25%機率將其反彈給甩碎片的人。";
                break;
            }
        }
    }

}
