using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineTrapLogic3 : MonoBehaviour
{
    public bool enemySlow = false;
    public bool playerSlow = false;
    public bool allowReset = false;
    public bool allowUpdate = false;
    public int trapActivated = -1;
    public GameObject warning;

    private float x = -10.0f;
    private float y = -10.0f;
    private OnlineTrapMaster master;
    Animator animator;

    private GameObject player;

    private PhotonView _pv;
    private float newx, newy;
    private bool unslowcheck;

    private GameObject GameService;

    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();
        GameService = GameObject.Find("GameService");
        
        animator = GetComponent<Animator>();
        unslowcheck = false;

        if(_pv.IsMine){
            master = GameObject.Find("TrapMaster").GetComponent<OnlineTrapMaster>();
            if(master.randomgen == 2) {
                newx = Random.Range(-6f, 6f);
                newy = Random.Range(-3f, 3f);
                CallRpcTrapLogic3(newx, newy);
            }
            else {
                CallRpcTrapLogic3Out();
            }   
        }
    }

    private void AppearingTrap()
    {
        warning.transform.position = new Vector3(-10.0f, -4.0f, -8.5f);
        transform.position = new Vector3(x, y, -8.5f);
    }

    void FixedUpdate()
    {
        if(_pv.IsMine){
            if(allowUpdate) 
            {
                allowUpdate = false;
                newx = Random.Range(-6f, 6f);
                newy = Random.Range(-3f, 3f);
                CallRpcTrapLogic3(newx, newy);
            }
            else if(allowReset) 
            {
                allowReset = false;
                CallRpcTrapLogic3Out();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_pv.IsMine){
            if(trapActivated == -1)
            {
                if(collider.tag == "Monster") {
                    enemySlow = true;
                    CallRpcTrapLogic3Animation(1);
                }
                else if(collider.tag == "Player"){
                    player = collider.gameObject;
                    if(!player.GetComponent<OnlinePlayerControl>().isdie)
                    {
                        if(player.GetComponent<OnlinePlayerControl>().isSkill11){
                            if(player.GetComponent<OnlinePlayerControl>().orgspeed < 5f * 1.73f){
                                GameService.GetComponent<OnlineSkillControl>().CallRpcSkill11Particle(DataBase.playerID);
                                player.GetComponent<OnlinePlayerControl>().orgspeed += 5f * 0.05f;
                            }
                        }
                        player.GetComponent<OnlinePlayerControl>().slow2 = true;
                        unslowcheck = true;
                        CallRpcTrapLogic3Animation(2);
                    }
                }
            }
        }
        else{
            if(trapActivated == -1)
            {
                if(collider.tag == "Player"){
                    player = collider.gameObject;
                    if(player.GetComponent<OnlinePlayerControl>().PlayerID == DataBase.playerID){
                        if(!player.GetComponent<OnlinePlayerControl>().isdie)
                        {
                            if(player.GetComponent<OnlinePlayerControl>().isSkill11){
                                if(player.GetComponent<OnlinePlayerControl>().orgspeed < 5f * 1.73f){
                                    GameService.GetComponent<OnlineSkillControl>().CallRpcSkill11Particle(DataBase.playerID);
                                    player.GetComponent<OnlinePlayerControl>().orgspeed += 5f * 0.05f;
                                }
                            }
                            player.GetComponent<OnlinePlayerControl>().slow2 = true;
                            unslowcheck = true;
                            CallRpcTrapLogic3Animation(2);
                        }
                    }
                }
            }
        }
    }

    private void UnfreezeEnemy()
    {
        enemySlow = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap3_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }

    private void UnfreezePlayer()
    {
        if(unslowcheck){
            player.GetComponent<OnlinePlayerControl>().slow2 = false;
            unslowcheck = false;
        }
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap3_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }

    public void CallRpcTrapLogic3(float newx, float newy)
    {
        _pv.RPC("RpcTrapLogic3", RpcTarget.All, newx, newy);
    }

    [PunRPC]
    void RpcTrapLogic3(float newx, float newy, PhotonMessageInfo info)
    {   
        x = newx;
        y = newy;
        warning.transform.position = new Vector3(x, y, -8.5f);
        Invoke("AppearingTrap", 1);
        trapActivated = -1;
    }

    public void CallRpcTrapLogic3Out()
    {
        _pv.RPC("RpcTrapLogic3Out", RpcTarget.All);
    }

    [PunRPC]
    void RpcTrapLogic3Out(PhotonMessageInfo info)
    {   
        transform.position = new Vector3(-10.0f, 2.0f, -8.5f);
    }

    public void CallRpcTrapLogic3Animation(int action)
    {
        _pv.RPC("RpcTrapLogic3Animation", RpcTarget.All, action);
    }

    [PunRPC]
    void RpcTrapLogic3Animation(int action, PhotonMessageInfo info)
    {   
        switch(action){
            case 1: {
                trapActivated = 0;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezeEnemy", 2);
                break;
            }
            case 2: {
                trapActivated = 0;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezePlayer", 2);
                break;
            }
        }
    }
}
