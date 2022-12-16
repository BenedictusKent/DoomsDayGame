using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Realtime;

public class PhotonOnlineSelectMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Text textPlayerList;
    [SerializeField]
    Button buttonStartGame;
    [SerializeField]
    GameObject buttonReadyGame, buttonCancelReady;

    private bool[] ReadyArray;
    private int count;

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
            ReadyArray = new bool[5];
            UpdatePlayerList();
            buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }

    public void UpdatePlayerList()
    {
        count = 0;
        StringBuilder sb = new StringBuilder();
        foreach(var kvp in PhotonNetwork.PlayerList){
            sb.AppendLine("# " + kvp.NickName + (ReadyArray[count] ? " --- Ready" : ""));
            count++;
        }
        textPlayerList.text = sb.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ReadyArray = new bool[5];
        buttonReadyGame.SetActive(true);
        buttonCancelReady.SetActive(false);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ReadyArray = new bool[5];
        buttonReadyGame.SetActive(true);
        buttonCancelReady.SetActive(false);
        UpdatePlayerList();
    }

    public void OnClickStartGame()
    {
        for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++){
            if(!ReadyArray[i]){
                return;
            }
        }
        SceneManager.LoadScene("OnlineGame");
    }
    
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickReadyGame()
    {
        buttonReadyGame.SetActive(false);
        buttonCancelReady.SetActive(true);
        _pv.RPC("RpcRoomPlayerReady", RpcTarget.All);
    }

    [PunRPC]
    void RpcRoomPlayerReady(PhotonMessageInfo info)
    {
        for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++){
            if(PhotonNetwork.PlayerList[i] == info.Sender){
                ReadyArray[i] = true;
            }
        }
        UpdatePlayerList();
    }

    public void OnClickCancelReady()
    {
        buttonReadyGame.SetActive(true);
        buttonCancelReady.SetActive(false);
        _pv.RPC("RpcRoomCancelReady", RpcTarget.All);
    }

    [PunRPC]
    void RpcRoomCancelReady(PhotonMessageInfo info)
    {
        for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++){
            if(PhotonNetwork.PlayerList[i] == info.Sender){
                ReadyArray[i] = false;
            }
        }
        UpdatePlayerList();
    }
}
