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

    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();
        if(PhotonNetwork.CurrentRoom != null){
            foreach(var kvp in PhotonNetwork.CurrentRoom.Players){
                alivePlayerMap[kvp.Value] = true;
            }
        }
        playnum = 5;
        lastnum = 15;

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = 1f;
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

        if(PhotonNetwork.IsMasterClient && CheckGameOver()){
            enemyDead = true;
            Particle01_copy = Instantiate(Particle01);
            Particle01_copy.transform.parent = Monster.transform;
            Particle01_copy.transform.localPosition = Vector3.zero;
            _audioSource.Play();
            if(PhotonNetwork.CurrentRoom.PlayerCount >= lastnum){
                DataBase.WinnerName = PhotonNetwork.CurrentRoom.Players[lastnum].NickName;
            }
            else{
                DataBase.WinnerName = "AI";
            }
            Invoke("toOnlineEndMenu", 2.25f);
        }

        if(!PhotonNetwork.IsMasterClient && CheckGameOver()){
            enemyDead = true;
            Particle01_copy = Instantiate(Particle01);
            Particle01_copy.transform.parent = Monster.transform;
            Particle01_copy.transform.localPosition = Vector3.zero;
            _audioSource.Play();
            if(PhotonNetwork.CurrentRoom.PlayerCount >= lastnum){
                DataBase.WinnerName = PhotonNetwork.CurrentRoom.Players[lastnum].NickName;
            }
            else{
                DataBase.WinnerName = "AI";
            }
        }
    }

    bool CheckGameOver()
    {
        return playnum <= 1;
    }

    void toOnlineEndMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("OnlineEndMenu");
    }

}
