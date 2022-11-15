using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    private Image front, back;
    private bool iscold = false;
    private float times, coldtime;
    private GameObject skill01;
    public Sprite sprite01, sprite02;

    public GameObject Player01;
    private PlayerControl player01_control;
    private float orgspeed;

    public GameObject Particle01;
    public GameObject Particle02;
    private GameObject Particle01_copy;
    private GameObject Particle02_copy;

    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    private PlayerAIControl pc2, pc3, pc4, pc5, pctemp;
    private float os2, os3, os4, os5, ostemp;
    private int to;

    public GameObject Particle03;
    public GameObject Particle04;
    private GameObject Particle03_copy;
    private GameObject Particle04_copy;

    private Finish finish;

    // Start is called before the first frame update
    void Start()
    {
        finish = GetComponent<Finish>();
        skill01 = GameObject.Find("skill01");
        front = skill01.GetComponent<Image>();
        back = skill01.transform.GetChild(0).GetComponent<Image>();
        back.fillAmount = 0f;
        coldtime = 5f;

        switch(DataBase.characterID)
        {
            case 1: {
                front.sprite = sprite01;
                back.sprite = sprite01;
                break;
            }
            case 2: {
                front.sprite = sprite02;
                back.sprite = sprite02;
                break;
            }
        }

        player01_control = Player01.GetComponent<PlayerControl>();
        orgspeed = player01_control.speed;

        PlayerNum.Add(2, GameObject.FindGameObjectsWithTag("Player")[0]);
        PlayerNum.Add(3, GameObject.FindGameObjectsWithTag("Player")[1]);
        PlayerNum.Add(4, GameObject.FindGameObjectsWithTag("Player")[2]);
        PlayerNum.Add(5, GameObject.FindGameObjectsWithTag("Player")[3]);
        pc2 = PlayerNum[2].GetComponent<PlayerAIControl>();
        pc3 = PlayerNum[3].GetComponent<PlayerAIControl>();
        pc4 = PlayerNum[4].GetComponent<PlayerAIControl>();
        pc5 = PlayerNum[5].GetComponent<PlayerAIControl>();
        os2 = pc2.speed;
        os3 = pc3.speed;
        os4 = pc4.speed;
        os5 = pc5.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(iscold)
        {
            times += Time.deltaTime;
            back.fillAmount = (coldtime - times) / coldtime;
            if(times >= 5f)
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
                case 1: {
                    show();
                    player01_control.speed = orgspeed * 2;
                    Particle01_copy = Instantiate(Particle01);
                    Particle01_copy.transform.parent = Player01.transform;
                    Particle01_copy.transform.localPosition = Vector3.zero;
                    Particle02_copy = Instantiate(Particle02);
                    Particle02_copy.transform.parent = Player01.transform;
                    Particle02_copy.transform.localPosition = Vector3.zero;
                    Invoke("endskill01", 2.0f);
                    break;
                }
                case 2: {
                    show();
                    checkrandomvalue();
                    Particle03_copy = Instantiate(Particle03);
                    Particle03_copy.transform.parent = Player01.transform;
                    Particle03_copy.transform.localPosition = Vector3.zero;
                    switch(to)
                    {
                        case 2: {
                            pc2.speed = 0f;
                            pctemp = pc2;
                            ostemp = os2;
                            break;
                        }
                        case 3: {
                            pc3.speed = 0f;
                            pctemp = pc3;
                            ostemp = os3;
                            break;
                        }
                        case 4: {
                            pc4.speed = 0f;
                            pctemp = pc4;
                            ostemp = os4;
                            break;
                        }
                        case 5: {
                            pc5.speed = 0f;
                            pctemp = pc5;
                            ostemp = os5;
                            break;
                        }
                    }
                    Particle04_copy = Instantiate(Particle04);
                    Particle04_copy.transform.parent = PlayerNum[to].transform;
                    Particle04_copy.transform.localPosition = Vector3.zero;
                    Invoke("endskill01", 1.5f);
                    break;
                }
            }
            
        }
    }

    public void show()
    {
        iscold = true;
        back.fillAmount = 1f;
    }

    void endskill01()
    {
        switch(DataBase.characterID)
        {
            case 1: {
                player01_control.speed = orgspeed;
                Destroy(Particle01_copy);
                Destroy(Particle02_copy);
                break;
            }
            case 2: {
                pctemp.speed = ostemp;
                Destroy(Particle03_copy);
                Destroy(Particle04_copy);
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
}