using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public GameObject CreateRoomWindow, JoinRoomWindow;

    [SerializeField]
    InputField inputRoomNameCreate, inputRoomNameJoin;
    [SerializeField]
    InputField inputPlayerName;
    [SerializeField]
    Text textRoomList;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("RuleMenu");
        }
        else
        {
            if(PhotonNetwork.CurrentLobby == null){
                PhotonNetwork.JoinLobby();
            }
            CreateRoomWindow.SetActive(false);
            JoinRoomWindow.SetActive(false);
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby is joined.");
    }

    public string GetPlayerName()
    {
        return inputPlayerName.text.Trim();
    }

    public void OpenCreateRoomWindow()
    {
        JoinRoomWindow.SetActive(false);
        CreateRoomWindow.SetActive(true);
    }

    public void CloseCreateRoomWindow()
    {
        CreateRoomWindow.SetActive(false);
    }

    public void OpenJoinRoomWindow()
    {
        CreateRoomWindow.SetActive(false);
        JoinRoomWindow.SetActive(true);
    }

    public void CloseJoinRoomWindow()
    {
        JoinRoomWindow.SetActive(false);
    }

    public void OnClickCreateRoom()
    {
        string roomName = inputRoomNameCreate.text.Trim();
        string playerName = GetPlayerName();

        if(roomName.Length > 0 && playerName.Length > 0)
        {
            PhotonNetwork.CreateRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
        else
        {
            Debug.Log("Invalid RoomName or PlayerName!");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined.");
        SceneManager.LoadScene("Room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("update");
        StringBuilder sb = new StringBuilder();
        foreach(RoomInfo roomInfo in roomList){
            if(roomInfo.PlayerCount > 0){
                sb.AppendLine("# " + roomInfo.Name + " / 人數： " + roomInfo.PlayerCount);
            }
        }
        textRoomList.text = sb.ToString();
    }

    public void OnClickJoinRoom()
    {
        string roomName = inputRoomNameJoin.text.Trim();
        string playerName = GetPlayerName();

        if(roomName.Length > 0 && playerName.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
        else
        {
            Debug.Log("Invalid RoomName or PlayerName!");
        }
    }
}
