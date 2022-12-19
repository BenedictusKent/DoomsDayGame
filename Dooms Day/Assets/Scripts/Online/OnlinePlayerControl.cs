using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerControl : MonoBehaviour
{
    public bool isAI = false;
    public int PlayerID;

    public PlayerObject _nowObj;
    private GameObject Meteorite;

    public float speed = 10.0f;
    private float LocateTime = DataBase.AIattacktime, LocateNeedTime = DataBase.AIattacktime, MoveNeedTime = DataBase.AImovetime;

    private Rigidbody2D rb;
    private Vector2 movement;

    public bool frozen, slow, slow2;
    public float orgspeed = 10.0f;

    public bool isdie;

    public AudioClip playerdead01;
    private AudioSource _audioSourceDead;

    private PhotonView _pv;

    private GameObject GameService;

    private int RPCaction;
    private bool sendRPC;

    public GameObject Particle01;
    private GameObject Particle01_copy;
    public GameObject Particle02;
    private GameObject Particle02_copy;

    // Start is called before the first frame update
    void Start()
    {
        rb = _nowObj.GetComponent<Rigidbody2D>();
        orgspeed = speed;
        frozen = false;
        slow = false;
        slow2 = false;
        isdie = false;
        GameService = GameObject.Find("GameService");
        _pv = this.gameObject.GetComponent<PhotonView>();
        RPCaction = 1;

        if(isAI)
        {
            Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
            InvokeRepeating("ChangeMove", 0.5f, MoveNeedTime);
        }

        _audioSourceDead = this.gameObject.AddComponent<AudioSource>();
        _audioSourceDead.loop = false;
        _audioSourceDead.volume = DataBase.EffectVolume2;
        _audioSourceDead.clip = playerdead01;
        Invoke("checkIsMine", 0.175f);
    }

    void checkIsMine()
    {
        if(!_pv.IsMine){
            Destroy(this.gameObject.GetComponent<OnlineGetMeteorite>());
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(_pv.IsMine){
            if(!isdie)
            {
                if(frozen)
                {
                    speed = 0f;
                }
                else if(slow)
                {
                    speed = orgspeed / 2;
                }
                else if(slow2)
                {
                    speed = orgspeed / 3;
                }
                else
                {
                    speed = orgspeed;
                }

                if(isAI)
                {
                    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                    AIattack();
                }
                else
                {
                    HumanControl();
                }
            }
        }
    }

    // Human
    void HumanControl()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //A,D
        movement.y = Input.GetAxisRaw("Vertical"); //W,S
        sendRPC = false;

        if(movement.x != 0 && movement.y != 0)
        {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

        if(movement.x == 0 && movement.y == 0)
        {
            _nowObj.Idle();
            if(RPCaction != 1){
                sendRPC = true;
                RPCaction = 1;
            }
        }
        else
        {
            _nowObj.Run();
            if(RPCaction != 2){
                sendRPC = true;
                RPCaction = 2;
            }
        }

        if(movement.x > 0)
        {
            _nowObj.TurnRight();
        }
        else if(movement.x < 0)
        {
            _nowObj.TurnLeft();
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if(sendRPC){
            CallRpcPlayerAnimation(PlayerID, RPCaction);
        }
    }

    // AI
    void AIattack()
    {
        if (_nowObj.GetComponent<OnlineGetMeteorite>().haveMeteorite == true)
        {
            LocateTime -= Time.fixedDeltaTime;
            if(LocateTime <= 0)
            {
                LocateTime = LocateNeedTime;
                _nowObj.GetComponent<OnlineGetMeteorite>().haveMeteorite = false;
                attack();
            }
        }
    }

    void attack()
    {
        if(GameService.GetComponent<OnlineFinish>().playnum > 1){
            GameService.GetComponent<OnlineAudioControl>().CallRpcAudioThrowball();
            Meteorite.GetComponent<OnlineMeteoriteTo>().AInum = _nowObj.GetComponent<OnlineGetMeteorite>().PlayerID;
        }
    }

    void ChangeMove()
    {
        if(_pv.IsMine){
            if(!isdie)
            {
                movement.x = Random.Range(-1, 2); //A,D
                movement.y = Random.Range(-1, 2); //W,S

                if(movement.x != 0 && movement.y != 0)
                {
                    movement.x *= 0.7f;
                    movement.y *= 0.7f;
                }

                if(movement.x == 0 && movement.y == 0)
                {
                    _nowObj.Idle();
                    RPCaction = 1;
                }
                else
                {
                    _nowObj.Run();
                    RPCaction = 2;
                }

                if(movement.x > 0)
                {
                    _nowObj.TurnRight();
                }
                else if(movement.x < 0)
                {
                    _nowObj.TurnLeft();
                }

                CallRpcPlayerAnimation(PlayerID, RPCaction);
            }
        }
    }

    public void CallRpcPlayerAnimation(int who, int action)
    {
        _pv.RPC("RpcPlayerAnimation", RpcTarget.Others, who, action);
    }

    [PunRPC]
    void RpcPlayerAnimation(int who, int action, PhotonMessageInfo info)
    {
        /* action
            1. Idle
            2. Run
        */
        
        if(who == PlayerID){
            switch(action){
                case 1: _nowObj.Idle(); break;
                case 2: _nowObj.Run(); break;
            }

        }
    }

    public void CallRpcPlayerDeadAnimation(int who)
    {
        _pv.RPC("RpcPlayerDeadAnimation", RpcTarget.Others, who);
    }

    [PunRPC]
    void RpcPlayerDeadAnimation(int who, PhotonMessageInfo info)
    {
        if(who == PlayerID){
            _nowObj.Death();
            isdie = true;
            Particle01_copy = Instantiate(Particle01, transform);
            Particle02_copy = Instantiate(Particle02, transform);
            _audioSourceDead.Play();
        }
    }
}

