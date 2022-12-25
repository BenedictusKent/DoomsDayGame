using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Realtime;
using TMPro;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Text textRoomName;
    [SerializeField]
    Text textPlayerList;
    [SerializeField]
    Button buttonStartGame, buttonPreviousAILevel, buttonNextAILevel, buttonPreviousMap, buttonNextMap;

    public TMP_Text AILevelChoose, MapChoose;
    private int AILevelNumber = 4, MapNumber = 2;

    private PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            _pv = this.gameObject.GetComponent<PhotonView>();
            textRoomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
            buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
            buttonNextAILevel.interactable = PhotonNetwork.IsMasterClient;
            buttonPreviousAILevel.interactable = PhotonNetwork.IsMasterClient;
            buttonPreviousMap.interactable = PhotonNetwork.IsMasterClient;
            buttonNextMap.interactable = PhotonNetwork.IsMasterClient;
            if(PhotonNetwork.IsMasterClient){
                AILevel();
                OnClickChangeAILevel(DataBase.AILevel);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
        buttonNextAILevel.interactable = PhotonNetwork.IsMasterClient;
        buttonPreviousAILevel.interactable = PhotonNetwork.IsMasterClient;
        buttonPreviousMap.interactable = PhotonNetwork.IsMasterClient;
        buttonNextMap.interactable = PhotonNetwork.IsMasterClient;
        if(PhotonNetwork.IsMasterClient){
            AILevel();
            OnClickChangeAILevel(DataBase.AILevel);
        }
    }

    public void UpdatePlayerList()
    {
        StringBuilder sb = new StringBuilder();
        foreach(var kvp in PhotonNetwork.PlayerList){
            sb.AppendLine("# " + kvp.NickName);
        }
        textPlayerList.text = sb.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        if(PhotonNetwork.IsMasterClient){
            OnClickChangeAILevel(DataBase.AILevel);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        if(PhotonNetwork.IsMasterClient){
            OnClickChangeAILevel(DataBase.AILevel);
        }
    }

    public void OnClickStartChooseSkill()
    {
        SceneManager.LoadScene("OnlineSelectMenu");
    }
    
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    void AILevel()
    {
        switch(DataBase.AILevel)
        {
            case 0: {
                AILevelChoose.text = "Easy";
                DataBase.AIattacktime = 1.2f;
                DataBase.AImovetime = 0.75f;
                break;
            }
            case 1: {
                AILevelChoose.text = "Normal";
                DataBase.AIattacktime = 0.65f;
                DataBase.AImovetime = 0.65f;
                break;
            }
            case 2: {
                AILevelChoose.text = "Hard";
                DataBase.AIattacktime = 0.4f;
                DataBase.AImovetime = 0.4f;
                break;
            }
            case 3: {
                AILevelChoose.text = "Hell";
                DataBase.AIattacktime = 0.3f;
                DataBase.AImovetime = 0.3f;
                break;
            }
        }
    }

    void Map()
    {
        switch(DataBase.mapID)
        {
            case 0: {
                MapChoose.text = "死亡谷地";
                break;
            }
            case 1: {
                MapChoose.text = "殘酷冰原";
                break;
            }
        }
    }

    public void OnClickNextAILevel()
    {
        DataBase.AILevel = (DataBase.AILevel + 1) % AILevelNumber;
        AILevel();
        OnClickChangeAILevel(DataBase.AILevel);
    }

    public void OnClickPreviousAILevel()
    {
        DataBase.AILevel = (DataBase.AILevel + AILevelNumber - 1) % AILevelNumber;
        AILevel();
        OnClickChangeAILevel(DataBase.AILevel);
    }

    public void OnClickNextMap()
    {
        DataBase.mapID = (DataBase.mapID + 1) % MapNumber;
        Map();
        OnClickChangeMap(DataBase.mapID);
    }

    public void OnClickPreviousMap()
    {
        DataBase.mapID = (DataBase.mapID + MapNumber - 1) % MapNumber;
        Map();
        OnClickChangeMap(DataBase.mapID);
    }

    public void OnClickChangeAILevel(int level)
    {
        _pv.RPC("RpcChangeAILevel", RpcTarget.Others, level);
    }

    [PunRPC]
    void RpcChangeAILevel(int level, PhotonMessageInfo info)
    {
        DataBase.AILevel = level;
        AILevel();
    }

    public void OnClickChangeMap(int id)
    {
        _pv.RPC("RpcChangeMap", RpcTarget.Others, id);
    }

    [PunRPC]
    void RpcChangeMap(int id, PhotonMessageInfo info)
    {
        DataBase.mapID = id;
        Map();
    }
}
