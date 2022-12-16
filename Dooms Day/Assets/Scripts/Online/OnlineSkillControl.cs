using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HashTable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class OnlineSkillControl : MonoBehaviour
{
    private Image front, back, frontp, backp;
    private bool iscold = false;
    private float times, coldtime;
    private GameObject FirstSkill, PassiveSkill;
    public Sprite sprite00, sprite01, sprite02, sprite03, sprite04;
    public TMP_Text counttimetext;
    private int coldtimeint;
    private int counttime;

    private GameObject Meteorite;

    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    Dictionary<int, OnlinePlayerControl> PlayerNumControl = new Dictionary<int, OnlinePlayerControl>();
    Dictionary<int, float> PlayerNumOrgspeed = new Dictionary<int, float>();
    public GameObject P1, P2, P3, P4, P5;
    private OnlinePlayerControl pctemp;
    private float ostemp;

    private GameObject My;
    private OnlinePlayerControl MyControl;
    private float MyOrgspeed;

    // skill00
    public GameObject Particle01;
    public GameObject Particle02;
    private GameObject Particle01_copy;
    private GameObject Particle02_copy;

    // skill01
    private int to;

    public GameObject Particle03;
    public GameObject Particle04;
    private GameObject Particle03_copy;
    private GameObject Particle04_copy;

    private OnlineFinish finish;

    // skill02
    public GameObject Particle05;
    private GameObject Particle05_copy;
    public GameObject Particle06;
    private GameObject Particle06_copy;

    // skill03

    // skill04
    public GameObject Particle07;
    private GameObject Particle07_copy;

    private float EnhanceValue;

    // Start is called before the first frame update
    void Start()
    {
        finish = GetComponent<OnlineFinish>();
        FirstSkill = GameObject.Find("FirstSkill");
        front = FirstSkill.GetComponent<Image>();
        back = FirstSkill.transform.GetChild(0).GetComponent<Image>();
        back.fillAmount = 0f;
        counttimetext.text = "";

        PassiveSkill = GameObject.Find("PassiveSkill");
        frontp = PassiveSkill.GetComponent<Image>();
        backp = PassiveSkill.transform.GetChild(0).GetComponent<Image>();
        backp.fillAmount = 0f;

        PlayerNum.Add(1, P1);
        PlayerNum.Add(2, P2);
        PlayerNum.Add(3, P3);
        PlayerNum.Add(4, P4);
        PlayerNum.Add(5, P5);
        PlayerNumControl.Add(1, PlayerNum[1].GetComponent<OnlinePlayerControl>());
        PlayerNumControl.Add(2, PlayerNum[2].GetComponent<OnlinePlayerControl>());
        PlayerNumControl.Add(3, PlayerNum[3].GetComponent<OnlinePlayerControl>());
        PlayerNumControl.Add(4, PlayerNum[4].GetComponent<OnlinePlayerControl>());
        PlayerNumControl.Add(5, PlayerNum[5].GetComponent<OnlinePlayerControl>());
        PlayerNumOrgspeed.Add(1, PlayerNumControl[1].speed);
        PlayerNumOrgspeed.Add(2, PlayerNumControl[2].speed);
        PlayerNumOrgspeed.Add(3, PlayerNumControl[3].speed);
        PlayerNumOrgspeed.Add(4, PlayerNumControl[4].speed);
        PlayerNumOrgspeed.Add(5, PlayerNumControl[5].speed);

        My = PlayerNum[DataBase.playerID];
        MyControl = PlayerNumControl[DataBase.playerID];
        MyOrgspeed = PlayerNumOrgspeed[DataBase.playerID];

        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];

        switch(DataBase.characterID)
        {
            case 0: {
                PassiveSkill.SetActive(false);
                front.sprite = sprite00;
                back.sprite = sprite00;
                coldtime = 10f;
                coldtimeint = 10;
                break;
            }
            case 1: {
                PassiveSkill.SetActive(false);
                front.sprite = sprite01;
                back.sprite = sprite01;
                coldtime = 8f;
                coldtimeint = 8;
                break;
            }
            case 2: {
                PassiveSkill.SetActive(false);
                front.sprite = sprite02;
                back.sprite = sprite02;
                coldtime = 12f;
                coldtimeint = 12;
                break;
            }
            case 3: {
                FirstSkill.SetActive(false);
                frontp.sprite = sprite03;
                backp.sprite = sprite03;
                My.GetComponent<OnlineGetMeteorite>().HP = 3;
                break;
            }
            case 4: {
                FirstSkill.SetActive(false);
                frontp.sprite = sprite04;
                backp.sprite = sprite04;
                EnhanceValue = 0.75f;
                MyControl.orgspeed = MyOrgspeed * EnhanceValue;
                backp.fillAmount = 1f;
                InvokeRepeating("potential", 10f, 10f);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(iscold)
        {
            times += Time.deltaTime;
            back.fillAmount = (coldtime - times) / coldtime;
            if(times >= coldtime)
            {
                times = 0f;
                iscold = false;
                back.fillAmount = 0f;
            }
        }

        if(Input.GetKeyDown("space") && !iscold && finish.playnum > 1)
        {
            switch(DataBase.characterID)
            {
                case 0: {
                    show();
                    MyControl.orgspeed = MyOrgspeed * 2;
                    Particle01_copy = Instantiate(Particle01, My.transform);
                    Particle02_copy = Instantiate(Particle02, My.transform);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 1: {
                    show();
                    checkrandomvalue();
                    Particle03_copy = Instantiate(Particle03, My.transform);
                    PlayerNumControl[to].orgspeed = 0f;
                    pctemp = PlayerNumControl[to];
                    ostemp = PlayerNumOrgspeed[to];
                    Particle04_copy = Instantiate(Particle04, PlayerNum[to].transform);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 2: {
                    show();
                    checkrandomvalue();
                    Particle05_copy = Instantiate(Particle05, My.transform);
                    Invoke("teleport", 0.5f);
                    Particle06_copy = Instantiate(Particle06, PlayerNum[to].transform);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 3: {
                    break;
                }
                case 4: {
                    break;
                }
            }
            
        }
    }

    public void show()
    {
        iscold = true;
        back.fillAmount = 1f;
        counttime = coldtimeint;
        InvokeRepeating("startcount", 0f, 1f);
    }

    void startcount()
    {
        counttimetext.text = counttime + "";
        counttime--;
        if(counttime == -1){
            counttimetext.text = "";
            CancelInvoke("startcount");
        }
    }

    void endFirstSkill()
    {
        switch(DataBase.characterID)
        {
            case 0: {
                MyControl.orgspeed = MyOrgspeed;
                Destroy(Particle01_copy);
                Destroy(Particle02_copy);
                break;
            }
            case 1: {
                pctemp.orgspeed = ostemp;
                Destroy(Particle03_copy);
                Destroy(Particle04_copy);
                break;
            }
            case 2: {
                Destroy(Particle05_copy);
                Destroy(Particle06_copy);
                break;
            }
            case 4: {
                Destroy(Particle07_copy);
                break;
            }
        }

    }

    void checkrandomvalue()
    {
        to = Random.Range(1, 6);
        while (dead() && to == DataBase.playerID)
        {
            to = Random.Range(1, 6);
        }
    }

    bool dead()
    {
        if(PlayerNum[to] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void teleport()
    {
        PlayerNum[to].transform.position = My.transform.position;
        if (My.GetComponent<OnlineGetMeteorite>().haveMeteorite == true)
        {
            HashTable table = new HashTable();
            table.Add("Action", "Mto");
            table.Add("to", to);
            PhotonNetwork.LocalPlayer.SetCustomProperties(table);
            //My.GetComponent<OnlineGetMeteorite>().haveMeteorite = false;
        }
    }

    void potential()
    {
        EnhanceValue += 0.25f;
        MyControl.orgspeed = MyOrgspeed * EnhanceValue;
        Particle07_copy = Instantiate(Particle07, My.transform);
        Invoke("endFirstSkill", 2.0f);
        backp.fillAmount -= (1f / 3f);
        if(EnhanceValue > 1.4f)
        {
            CancelInvoke("potential");
        }
    }
}

