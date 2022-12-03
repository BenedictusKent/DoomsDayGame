using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillControl : MonoBehaviour
{
    private Image front, back;
    private bool iscold = false;
    private float times, coldtime;
    private GameObject FirstSkill;
    public Sprite sprite00, sprite01, sprite02;
    public TMP_Text counttimetext;
    private int coldtimeint;
    private int counttime;

    private GameObject Meteorite;

    // skill00
    public GameObject Player01;
    private PlayerControl player01_control;
    private float orgspeed;

    public GameObject Particle01;
    public GameObject Particle02;
    private GameObject Particle01_copy;
    private GameObject Particle02_copy;

    // skill01
    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    public GameObject P2, P3, P4, P5;
    private PlayerControl pc2, pc3, pc4, pc5, pctemp;
    private float os2, os3, os4, os5, ostemp;
    private int to;

    public GameObject Particle03;
    public GameObject Particle04;
    private GameObject Particle03_copy;
    private GameObject Particle04_copy;

    private Finish finish;

    // skill02
    public GameObject Particle05;
    private GameObject Particle05_copy;
    public GameObject Particle06;
    private GameObject Particle06_copy;

    // Start is called before the first frame update
    void Start()
    {
        finish = GetComponent<Finish>();
        FirstSkill = GameObject.Find("FirstSkill");
        front = FirstSkill.GetComponent<Image>();
        back = FirstSkill.transform.GetChild(0).GetComponent<Image>();
        back.fillAmount = 0f;
        counttimetext.text = "";

        switch(DataBase.characterID)
        {
            case 0: {
                front.sprite = sprite00;
                back.sprite = sprite00;
                coldtime = 10f;
                coldtimeint = 10;
                break;
            }
            case 1: {
                front.sprite = sprite01;
                back.sprite = sprite01;
                coldtime = 8f;
                coldtimeint = 8;
                break;
            }
            case 2: {
                front.sprite = sprite02;
                back.sprite = sprite02;
                coldtime = 12f;
                coldtimeint = 12;
                break;
            }
        }

        player01_control = Player01.GetComponent<PlayerControl>();
        orgspeed = player01_control.speed;

        PlayerNum.Add(2, P2);
        PlayerNum.Add(3, P3);
        PlayerNum.Add(4, P4);
        PlayerNum.Add(5, P5);
        pc2 = PlayerNum[2].GetComponent<PlayerControl>();
        pc3 = PlayerNum[3].GetComponent<PlayerControl>();
        pc4 = PlayerNum[4].GetComponent<PlayerControl>();
        pc5 = PlayerNum[5].GetComponent<PlayerControl>();
        os2 = pc2.speed;
        os3 = pc3.speed;
        os4 = pc4.speed;
        os5 = pc5.speed;

        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
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

        if(Input.GetKeyDown("space") && !iscold && finish.playernum > 1)
        {
            switch(DataBase.characterID)
            {
                case 0: {
                    show();
                    player01_control.orgspeed = orgspeed * 2;
                    Particle01_copy = Instantiate(Particle01);
                    Particle01_copy.transform.parent = Player01.transform;
                    Particle01_copy.transform.localPosition = Vector3.zero;
                    Particle02_copy = Instantiate(Particle02);
                    Particle02_copy.transform.parent = Player01.transform;
                    Particle02_copy.transform.localPosition = Vector3.zero;
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 1: {
                    show();
                    checkrandomvalue();
                    Particle03_copy = Instantiate(Particle03);
                    Particle03_copy.transform.parent = Player01.transform;
                    Particle03_copy.transform.localPosition = Vector3.zero;
                    switch(to)
                    {
                        case 2: {
                            pc2.orgspeed = 0f;
                            pctemp = pc2;
                            ostemp = os2;
                            break;
                        }
                        case 3: {
                            pc3.orgspeed = 0f;
                            pctemp = pc3;
                            ostemp = os3;
                            break;
                        }
                        case 4: {
                            pc4.orgspeed = 0f;
                            pctemp = pc4;
                            ostemp = os4;
                            break;
                        }
                        case 5: {
                            pc5.orgspeed = 0f;
                            pctemp = pc5;
                            ostemp = os5;
                            break;
                        }
                    }
                    Particle04_copy = Instantiate(Particle04);
                    Particle04_copy.transform.parent = PlayerNum[to].transform;
                    Particle04_copy.transform.localPosition = Vector3.zero;
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 2: {
                    show();
                    checkrandomvalue();
                    Particle05_copy = Instantiate(Particle05);
                    Particle05_copy.transform.parent = Player01.transform;
                    Particle05_copy.transform.localPosition = Vector3.zero;
                    Invoke("teleport", 0.5f);
                    Particle06_copy = Instantiate(Particle06);
                    Particle06_copy.transform.parent = PlayerNum[to].transform;
                    Particle06_copy.transform.localPosition = Vector3.zero;
                    Invoke("endFirstSkill", 2.0f);
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
                player01_control.orgspeed = orgspeed;
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
        }

    }

    void checkrandomvalue()
    {
        to = Random.Range(2, 6);
        while (dead())
        {
            to = Random.Range(2, 6);
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
        PlayerNum[to].transform.position = Player01.transform.position;
        if (Player01.GetComponent<GetMeteorite>().haveMeteorite == true)
        {
            Meteorite.GetComponent<MeteoriteTo>().to = to;
            Meteorite.GetComponent<MeteoriteTo>().speed = 2;
            Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
            Player01.GetComponent<GetMeteorite>().haveMeteorite = false;
        }
    }
}
