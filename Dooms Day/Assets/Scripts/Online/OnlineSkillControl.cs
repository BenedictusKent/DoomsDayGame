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
    public Sprite sprite00, sprite01, sprite02, sprite03, sprite04, sprite05;
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

    private PhotonView _pv;

    public bool isalive;

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
    public GameObject Particle07;
    private GameObject Particle07_copy;

    // skill04
    public GameObject Particle08;
    private GameObject Particle08_copy;

    private float EnhanceValue;

    // skill05
    public AudioClip audioSkill05;
    private AudioSource _audioSourceSkill05;

    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();
        isalive = true;
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
            case 5: {
                FirstSkill.SetActive(false);
                frontp.sprite = sprite05;
                backp.sprite = sprite05;
                My.GetComponent<OnlineGetMeteorite>().isSkill05 = true;

                _audioSourceSkill05 = this.gameObject.AddComponent<AudioSource>();
                _audioSourceSkill05.loop = false;
                _audioSourceSkill05.volume = DataBase.EffectVolume1;
                _audioSourceSkill05.clip = audioSkill05;
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

        if(Input.GetKeyDown("space") && !iscold && isalive && finish.playnum > 1)
        {
            switch(DataBase.characterID)
            {
                case 0: {
                    show();
                    CallRpcSkill00Particle(DataBase.playerID);
                    MyControl.orgspeed = MyOrgspeed * 2;
                    Particle01_copy = Instantiate(Particle01, My.transform);
                    Particle02_copy = Instantiate(Particle02, My.transform);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 1: {
                    show();
                    checkrandomvalue();
                    CallRpcSkill01Particle(DataBase.playerID, to);
                    CallRpcSkill01(to);
                    Particle03_copy = Instantiate(Particle03, My.transform);
                    Particle04_copy = Instantiate(Particle04, PlayerNum[to].transform);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 2: {
                    show();
                    checkrandomvalue();
                    CallRpcSkill02Particle(DataBase.playerID, to);
                    Particle05_copy = Instantiate(Particle05, My.transform);
                    Particle06_copy = Instantiate(Particle06, PlayerNum[to].transform);
                    Invoke("teleport", 0.5f);
                    Invoke("endFirstSkill", 2.0f);
                    break;
                }
                case 3: {
                    break;
                }
                case 4: {
                    break;
                }
                case 5: {
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
                Destroy(Particle08_copy);
                break;
            }
        }

    }

    void checkrandomvalue()
    {
        to = Random.Range(1, 6);
        while (dead() || to == DataBase.playerID)
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
        CallRpcSkill02(to, DataBase.playerID);
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
        if(isalive){
            EnhanceValue += 0.25f;
            MyControl.orgspeed = MyOrgspeed * EnhanceValue;
            CallRpcSkill04Particle(DataBase.playerID);
            Particle08_copy = Instantiate(Particle08, My.transform);
            Invoke("endFirstSkill", 2.0f);
            backp.fillAmount -= (1f / 3f);
            if(EnhanceValue > 1.4f)
            {
                CancelInvoke("potential");
            }
        }
    }

    // skill00
    public void CallRpcSkill00Particle(int target)
    {
        _pv.RPC("RpcSkill00Particle", RpcTarget.Others, target);
    }

    [PunRPC]
    void RpcSkill00Particle(int target, PhotonMessageInfo info)
    {
        GameObject Particle01_temp = Instantiate(Particle01, PlayerNum[target].transform);
        GameObject Particle02_temp = Instantiate(Particle02, PlayerNum[target].transform);
        StartCoroutine(WaitAndDestroy00(2.0f, Particle01_temp, Particle02_temp));
    }

    private IEnumerator WaitAndDestroy00(float waitTime, GameObject P01, GameObject P02)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(P01);
        Destroy(P02);
    }

    // skill01
    public void CallRpcSkill01Particle(int target1, int target2)
    {
        _pv.RPC("RpcSkill01Particle", RpcTarget.Others, target1, target2);
    }

    [PunRPC]
    void RpcSkill01Particle(int target1, int target2, PhotonMessageInfo info)
    {
        GameObject Particle03_temp = Instantiate(Particle03, PlayerNum[target1].transform);
        GameObject Particle04_temp = Instantiate(Particle04, PlayerNum[target2].transform);
        StartCoroutine(WaitAndDestroy01(2.0f, Particle03_temp, Particle04_temp));
    }

    private IEnumerator WaitAndDestroy01(float waitTime, GameObject P01, GameObject P02)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(P01);
        Destroy(P02);
    }

    public void CallRpcSkill01(int target)
    {
        _pv.RPC("RpcSkill01", RpcTarget.All, target);
    }

    [PunRPC]
    void RpcSkill01(int target, PhotonMessageInfo info)
    {
        if(PlayerNum[target].GetComponent<PhotonView>().IsMine){
            PlayerNumControl[target].orgspeed = 0f;
            StartCoroutine(WaitAndUnfreeze01(2.0f, target));
        }
    }

    private IEnumerator WaitAndUnfreeze01(float waitTime, int target)
    {
        yield return new WaitForSeconds(waitTime);
        if(PlayerNum[target] != null){
            PlayerNumControl[target].orgspeed = PlayerNumOrgspeed[target];
        }
    }

    // skill02
    public void CallRpcSkill02Particle(int target1, int target2)
    {
        _pv.RPC("RpcSkill02Particle", RpcTarget.Others, target1, target2);
    }

    [PunRPC]
    void RpcSkill02Particle(int target1, int target2, PhotonMessageInfo info)
    {
        GameObject Particle05_temp = Instantiate(Particle05, PlayerNum[target1].transform);
        GameObject Particle06_temp = Instantiate(Particle06, PlayerNum[target2].transform);
        StartCoroutine(WaitAndDestroy02(2.0f, Particle05_temp, Particle06_temp));
    }

    private IEnumerator WaitAndDestroy02(float waitTime, GameObject P01, GameObject P02)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(P01);
        Destroy(P02);
    }

    public void CallRpcSkill02(int target1, int target2)
    {
        _pv.RPC("RpcSkill02", RpcTarget.All, target1, target2);
    }

    [PunRPC]
    void RpcSkill02(int target1, int target2, PhotonMessageInfo info)
    {
        if(PlayerNum[target1].GetComponent<PhotonView>().IsMine){
            PlayerNum[target1].transform.position = PlayerNum[target2].transform.position;
        }
    }

    // skill03
    public void CallRpcSkill03Particle(int target)
    {
        _pv.RPC("RpcSkill03Particle", RpcTarget.Others, target);
    }

    [PunRPC]
    void RpcSkill03Particle(int target, PhotonMessageInfo info)
    {
        GameObject Particle07_temp = Instantiate(Particle07, PlayerNum[target].transform);
        StartCoroutine(WaitAndDestroy03(2.0f, Particle07_temp));
    }

    private IEnumerator WaitAndDestroy03(float waitTime, GameObject P01)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(P01);
    }

    // skill04
    public void CallRpcSkill04Particle(int target)
    {
        _pv.RPC("RpcSkill04Particle", RpcTarget.Others, target);
    }

    [PunRPC]
    void RpcSkill04Particle(int target, PhotonMessageInfo info)
    {
        GameObject Particle08_temp = Instantiate(Particle08, PlayerNum[target].transform);
        StartCoroutine(WaitAndDestroy04(2.0f, Particle08_temp));
    }

    private IEnumerator WaitAndDestroy04(float waitTime, GameObject P01)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(P01);
    }

    // skill05
    public void CallRpcSkill04Audio()
    {
        _pv.RPC("RpcSkill04Audio", RpcTarget.All);
    }

    [PunRPC]
    void RpcSkill04Audio(PhotonMessageInfo info)
    {
        _audioSourceSkill05.Play();
    }

}

