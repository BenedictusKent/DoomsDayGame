using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenu : MonoBehaviour
{
    private Image skillimg, mapimg;
    private GameObject FirstSkill, Map;
    public Sprite sprite00, sprite01, sprite02, sprite03, sprite04, sprite05, sprite06, sprite07, sprite08, sprite09;
    public Sprite sprite10, sprite11, sprite12;
    public Sprite mapsprite00, mapsprite01;

    public TMP_Text FirstSkillName, FirstSkillDesc;
    public TMP_Text MapName, MapDesc;

    private int skillnumber = 13;
    private int mapnumber = 2;

    // Start is called before the first frame update
    void Start()
    {
        FirstSkill = GameObject.Find("FirstSkill");
        skillimg = FirstSkill.GetComponent<Image>();

        Map = GameObject.Find("Map");
        mapimg = Map.GetComponent<Image>();

        ChangeSkill();
        ChangeMap();
    }

    void ChangeSkill()
    {
        switch(DataBase.characterID)
        {
            case 0: {
                skillimg.sprite = sprite00;
                FirstSkillName.text = "光翼奔馳";
                FirstSkillDesc.text = "解除所有技能造成的緩速及定身效果，並將移動速度提升至2.25倍，持續2秒。CD: 9s";
                break;
            }
            case 1: {
                skillimg.sprite = sprite01;
                FirstSkillName.text = "極冰束縛";
                FirstSkillDesc.text = "隨機將一名其他玩家束縛在原地，持續2秒。CD: 9s";
                break;
            }
            case 2: {
                skillimg.sprite = sprite02;
                FirstSkillName.text = "黑暗新星";
                FirstSkillDesc.text = "隨機將一名玩家傳送至你的身旁，如果有碎片則甩給該玩家，施法1.5秒。CD: 14s";
                break;
            }
            case 3: {
                skillimg.sprite = sprite03;
                FirstSkillName.text = "聖光守護";
                FirstSkillDesc.text = "被動技，移動速度為0.9倍，持有碎片被怪物撞到時，會保護你並將碎片甩給最遠的玩家，能發動2次。";
                break;
            }
            case 4: {
                skillimg.sprite = sprite04;
                FirstSkillName.text = "潛能釋放";
                FirstSkillDesc.text = "被動技，初始移動速度為0.75倍，每10秒增加0.25倍，最多達到1.75倍。";
                break;
            }
            case 5: {
                skillimg.sprite = sprite05;
                FirstSkillName.text = "咒法反饋";
                FirstSkillDesc.text = "被動技，被甩碎片時，有30%機率將其反彈給甩碎片的人。";
                break;
            }
            case 6: {
                skillimg.sprite = sprite06;
                FirstSkillName.text = "斷序之炎";
                FirstSkillDesc.text = "將所有其他玩家的移動速度減緩至0.5倍，持續2秒。CD: 12s";
                break;
            }
            case 7: {
                skillimg.sprite = sprite07;
                FirstSkillName.text = "元素饗宴";
                FirstSkillDesc.text = "使怪物的移動速度大幅提升，持續1.5秒。CD: 10s";
                break;
            }
            case 8: {
                skillimg.sprite = sprite08;
                FirstSkillName.text = "審判之鎚";
                FirstSkillDesc.text = "瞬間將怪物傳送至地圖中心位置，並使之進入失神狀態，持續1.5秒。CD: 10s";
                break;
            }
            case 9: {
                skillimg.sprite = sprite09;
                FirstSkillName.text = "邪靈附體";
                FirstSkillDesc.text = "使所有其他玩家的操控方向顛倒，施法1秒，持續4秒。CD: 20s";
                break;
            }
            case 10: {
                skillimg.sprite = sprite10;
                FirstSkillName.text = "神佑真言";
                FirstSkillDesc.text = "被動技，當場上剩下2名玩家時，移動速度提升至1.5倍，並獲得1次聖光守護。";
                break;
            }
            case 11: {
                skillimg.sprite = sprite11;
                FirstSkillName.text = "雷電反應";
                FirstSkillDesc.text = "被動技，能觸發陷阱但不受其影響，初始移動速度為1.1倍，每觸發1個陷阱增加0.05倍，最多達到1.75倍。";
                break;
            }
            case 12: {
                skillimg.sprite = sprite12;
                FirstSkillName.text = "天堂降臨";
                FirstSkillDesc.text = "使用後的2.5秒內擁有無數次聖光守護的效果。CD: 13s";
                break;
            }
        }
    }

    void ChangeMap()
    {
        switch(DataBase.mapID)
        {
            case 0: {
                mapimg.sprite = mapsprite00;
                MapName.text = "死亡谷地";
                MapDesc.text = "壟罩死亡陰影的谷地，被稱為亡魂收割者的怪物在此處徘徊。";
                break;
            }
            case 1: {
                mapimg.sprite = mapsprite01;
                MapName.text = "殘酷冰原";
                MapDesc.text = "烙印在白色大地上的怪物足跡，說明了冰原的殘酷與凶險，被稱為酷寒暴雪的怪物在此處徘徊。";
                break;
            }
        }
    }

    public void NextSkill()
    {
        DataBase.characterID = (DataBase.characterID + 1) % skillnumber;
        ChangeSkill();
    }

    public void PreviousSkill()
    {
        DataBase.characterID = (DataBase.characterID + skillnumber - 1) % skillnumber;
        ChangeSkill();
    }

    public void NextMap()
    {
        DataBase.mapID = (DataBase.mapID + 1) % mapnumber;
        ChangeMap();
    }

    public void PreviousMap()
    {
        DataBase.mapID = (DataBase.mapID + mapnumber - 1) % mapnumber;
        ChangeMap();
    }
}
