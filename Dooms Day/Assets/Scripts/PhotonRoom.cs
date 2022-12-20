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
    Button buttonStartGame, buttonPreviousAILevel, buttonNextAILevel;

    public TMP_Text AILevelChoose;
    private int AILevelNumber = 4;

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
}
