using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using HashTable = ExitGames.Client.Photon.Hashtable;

public class OnlineMeteoriteTo : MonoBehaviourPunCallbacks
{
    public Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    public GameObject one, two, three, four, five;
    public int to, AInum, AIfarnum;
    public float speed = 1f;
    private float firstSpeed;
    public bool speedbool;

    private float fardistance, fartmp;
    private int farID;

    private GameObject GameService;

    private PhotonView _pv;

    public int preOwner, newOwner;

    // Start is called before the first frame update
    void Start()
    {
        GameService = GameObject.Find("GameService");
        PlayerNum.Add(1, one);
        PlayerNum.Add(2, two);
        PlayerNum.Add(3, three);
        PlayerNum.Add(4, four);
        PlayerNum.Add(5, five);

        _pv = this.gameObject.GetComponent<PhotonView>();

        if(_pv.IsMine){
            randomvalue();
        }
  
        AInum = 0;
        AIfarnum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(AInum > 0)
        {
            if(_pv.IsMine){
                if(GameService.GetComponent<OnlineFinish>().playnum > 1){
                    checkrandomvalue(AInum);
                }
            }
            AInum = 0;
        }

        if(AIfarnum > 0)
        {
            if(_pv.IsMine){
                farvalue(AIfarnum);
            }
            AIfarnum = 0;
        }

        if(speedbool)
        {
            GetFirstSpeed();
            speedbool = false;
        }
        
        if(to > 0){
            MoveBall();
        }
    }

    void GetFirstSpeed()
    {
        firstSpeed = 15 * speed;
    }

    void MoveBall()
    {
        if(PlayerNum[to] != null){
            moveball(PlayerNum[to]);
        }
    }

    void randomvalue()
    {
        int value = Random.Range(1, 6);
        HashTable table = new HashTable();
        table.Add("Action", "Mto");
        table.Add("to", value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);

        CallRpcMeteoriteInitial(value);
    }

    void checkrandomvalue(int ID)
    {
        int value = Random.Range(1, 6);
        while (dead(value) || value == ID)
        {
            value = Random.Range(1, 6);
        }
        HashTable table = new HashTable();
        table.Add("Action", "Mto");
        table.Add("to", value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
    }

    void farvalue(int ID)
    {
        fardistance = 0;

        for(int i = 1; i <= PlayerNum.Count; i++)
        {
            if(PlayerNum[i] != null && ID != i)
            {
                fartmp = Vector3.Distance(transform.position, PlayerNum[i].transform.position);
                if(fartmp > fardistance)
                {
                    fardistance = fartmp;
                    farID = i;
                }
            }
        }

        HashTable table = new HashTable();
        table.Add("Action", "Mto");
        table.Add("to", farID);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
    }

    bool dead(int value)
    {
        if(PlayerNum[value] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void moveball(GameObject x)
    {
        speed = calculateNewSpeed(x);
        transform.position = Vector3.Lerp(transform.position, x.transform.position + new Vector3(0, 0.7f, 0), speed * Time.deltaTime);
    }

    float calculateNewSpeed(GameObject x)
    {
        float tmp = Vector3.Distance(transform.position, x.transform.position + new Vector3(0, 0.7f, 0));

        if (tmp == 0)
            return tmp;
        else
            return (firstSpeed / tmp);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps)
    {
        if((string)changedProps["Action"] == "Mto"){
            to = (int)changedProps["to"];
            speed = 2.0f;
            speedbool = true;
        }
        else if((string)changedProps["Action"] == "Mfar"){
            AIfarnum = (int)changedProps["AIfarnum"];
            speed = 2.0f;
            speedbool = true;
        }
    }

    public void CallRpcMeteoriteOwner(int newValue)
    {
        _pv.RPC("RpcMeteoriteOwner", RpcTarget.All, newValue);
    }

    [PunRPC]
    void RpcMeteoriteOwner(int newValue, PhotonMessageInfo info)
    {
        preOwner = newOwner;
        newOwner = newValue;
    }

    public void CallRpcMeteoriteInitial(int newValue)
    {
        _pv.RPC("RpcMeteoriteInitial", RpcTarget.All, newValue);
    }

    [PunRPC]
    void RpcMeteoriteInitial(int newValue, PhotonMessageInfo info)
    {
        preOwner = newValue;
        newOwner = newValue;
    }

}
