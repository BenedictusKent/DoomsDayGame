using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineTrapLogic : MonoBehaviour
{
    public bool enemyFrozen = false;
    public bool playerFrozen = false;
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
    private bool unfreezecheck;

    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();
        
        animator = GetComponent<Animator>();
        unfreezecheck = false;

        if(_pv.IsMine){
            master = GameObject.Find("TrapMaster").GetComponent<OnlineTrapMaster>();
            if(master.randomgen == 0) {
                newx = Random.Range(-6f, 6f);
                newy = Random.Range(-3f, 3f);
                CallRpcTrapLogic(newx, newy);
            }
            else {
                CallRpcTrapLogicOut();
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
                CallRpcTrapLogic(newx, newy);
            }
            else if(allowReset) 
            {
                allowReset = false;
                CallRpcTrapLogicOut();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_pv.IsMine){
            if(trapActivated == -1)
            {
                if(collider.tag == "Monster") {
                    enemyFrozen = true;
                    CallRpcTrapLogicAnimation(1);
                }
                else if(collider.tag == "Player"){
                    player = collider.gameObject;
                    if(!player.GetComponent<OnlinePlayerControl>().isdie)
                    {
                        player.GetComponent<OnlinePlayerControl>().frozen = true;
                        unfreezecheck = true;
                        CallRpcTrapLogicAnimation(2);
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
                            player.GetComponent<OnlinePlayerControl>().frozen = true;
                            unfreezecheck = true;
                            CallRpcTrapLogicAnimation(2);
                        }
                    }
                }
            }
        }
    }

    private void UnfreezeEnemy()
    {
        enemyFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }

    private void UnfreezePlayer()
    {
        if(unfreezecheck){
            player.GetComponent<OnlinePlayerControl>().frozen = false;
            unfreezecheck = false;
        }
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }

    public void CallRpcTrapLogic(float newx, float newy)
    {
        _pv.RPC("RpcTrapLogic", RpcTarget.All, newx, newy);
    }

    [PunRPC]
    void RpcTrapLogic(float newx, float newy, PhotonMessageInfo info)
    {   
        x = newx;
        y = newy;
        warning.transform.position = new Vector3(x, y, -8.5f);
        Invoke("AppearingTrap", 1);
        trapActivated = -1;
    }

    public void CallRpcTrapLogicOut()
    {
        _pv.RPC("RpcTrapLogicOut", RpcTarget.All);
    }

    [PunRPC]
    void RpcTrapLogicOut(PhotonMessageInfo info)
    {   
        transform.position = new Vector3(-10.0f, 0f, -8.5f);
    }

    public void CallRpcTrapLogicAnimation(int action)
    {
        _pv.RPC("RpcTrapLogicAnimation", RpcTarget.All, action);
    }

    [PunRPC]
    void RpcTrapLogicAnimation(int action, PhotonMessageInfo info)
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
