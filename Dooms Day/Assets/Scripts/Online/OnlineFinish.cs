using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class OnlineFinish : MonoBehaviourPunCallbacks
{
    public bool enemyDead = false;
    //private float TimeRun = 0.25f;

    public int playnum;
    private int lastnum;

    public GameObject Monster;
    public GameObject Particle01;
    private GameObject Particle01_copy;

    public AudioClip monsterdead01;
    private AudioSource _audioSource;

    private PhotonView _pv;

    public Dictionary<Player, bool> alivePlayerMap = new Dictionary<Player, bool>();

    private OnlineSkillControl skillControl;
    public bool isSkill10 = false;

    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();
        if(PhotonNetwork.CurrentRoom != null){
            foreach(var kvp in PhotonNetwork.CurrentRoom.Players){
                alivePlayerMap[kvp.Value] = true;
            }
        }
        skillControl = GetComponent<OnlineSkillControl>();
        playnum = 5;
        lastnum = 15;

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = DataBase.EffectVolume1;
        _audioSource.clip = monsterdead01;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void CallRpcPlayerDead(int who)
    {
        _pv.RPC("RpcPlayerDead", RpcTarget.All, who);
    }

    [PunRPC]
    void RpcPlayerDead(int who, PhotonMessageInfo info)
    {
        if(alivePlayerMap.ContainsKey(info.Sender)){
            alivePlayerMap[info.Sender] = false;
            playnum--;
            lastnum -= who;
        }

        if(isSkill10){
            if(playnum == 2 && skillControl.isalive){
                skillControl.CallRpcSkill10(DataBase.playerID);
            }
        }

        if(PhotonNetwork.IsMasterClient && CheckGameOver()){
            enemyDead = true;
            Particle01_copy = Instantiate(Particle01, Monster.transform);
            _audioSource.Play();
            if(PhotonNetwork.CurrentRoom.PlayerCount >= lastnum){
                DataBase.WinnerName = PhotonNetwork.PlayerList[lastnum - 1].NickName;
            }
            else{
                DataBase.WinnerName = "AI";
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Invoke("toOnlineEndMenu", 2.25f);
        }

        if(!PhotonNetwork.IsMasterClient && CheckGameOver()){
            enemyDead = true;
            Particle01_copy = Instantiate(Particle01, Monster.transform);
            _audioSource.Play();
            if(PhotonNetwork.CurrentRoom.PlayerCount >= lastnum){
                DataBase.WinnerName = PhotonNetwork.PlayerList[lastnum - 1].NickName;
            }
            else{
                DataBase.WinnerName = "AI";
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    bool CheckGameOver()
    {
        return playnum <= 1;
    }

    void toOnlineEndMenu()
    {
        SceneManager.LoadScene("OnlineEndMenu");
    }

}
