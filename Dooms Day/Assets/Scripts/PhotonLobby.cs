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
    public GameObject CreateRoomWindow, JoinRoomWindow, HintMessageWindow;
    public GameObject buttonPrefab, buttonParent;

    [SerializeField]
    InputField inputRoomNameCreate, inputRoomNameJoin;
    [SerializeField]
    InputField inputPlayerName;
    [SerializeField]
    Text textRoomList;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        if(PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("RuleMenu");
        }
        else
        {
            if(PhotonNetwork.IsConnectedAndReady){
                PhotonNetwork.JoinLobby();
            }

            inputPlayerName.text = DataBase.NickName;
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
        HintMessageWindow.SetActive(false);
        CreateRoomWindow.SetActive(true);
    }

    public void CloseCreateRoomWindow()
    {
        CreateRoomWindow.SetActive(false);
    }

    public void OpenJoinRoomWindow()
    {
        CreateRoomWindow.SetActive(false);
        HintMessageWindow.SetActive(false);
        JoinRoomWindow.SetActive(true);
    }

    public void CloseJoinRoomWindow()
    {
        JoinRoomWindow.SetActive(false);
    }

    public void OpenHintMessageWindow()
    {
        CreateRoomWindow.SetActive(false);
        JoinRoomWindow.SetActive(false);
        HintMessageWindow.SetActive(true);
    }

    public void CloseHintMessageWindow()
    {
        HintMessageWindow.SetActive(false);
    }

    public void OnClickCreateRoom()
    {
        string roomName = inputRoomNameCreate.text.Trim();
        string playerName = GetPlayerName();

        if(roomName.Length > 0 && playerName.Length > 0)
        {
            PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = 5 });
            PhotonNetwork.LocalPlayer.NickName = playerName;
            DataBase.NickName = playerName;
        }
        else
        {
            OpenHintMessageWindow();
            //Debug.Log("Invalid RoomName or PlayerName!");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined.");
        SceneManager.LoadScene("Room");
    }

    public void OnClickJoinRoom()
    {
        string roomName = inputRoomNameJoin.text.Trim();
        string playerName = GetPlayerName();

        if(roomName.Length > 0 && playerName.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            DataBase.NickName = playerName;
        }
        else
        {
            OpenHintMessageWindow();
            //Debug.Log("Invalid RoomName or PlayerName!");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RemoveAllChildren(buttonParent);

        foreach(RoomInfo roomInfo in roomList){
            if(roomInfo.PlayerCount > 0){
                GameObject  newButton = Instantiate(buttonPrefab, buttonParent.transform);
                newButton.GetComponent<RoomButton>().RoomName.text = "# " + roomInfo.Name + " / 人數： " + roomInfo.PlayerCount + " / 5";
                newButton.GetComponent<ButtonExtension>().onDoubleClick.AddListener(() => JoinSelectRoom(roomInfo.Name));
            }
        }

    }

    private void JoinSelectRoom(string roomName)
    {
        string playerName = GetPlayerName();

        if(playerName.Length > 0){
            PhotonNetwork.JoinRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            DataBase.NickName = playerName;
        }
        else{
            OpenHintMessageWindow();
        }
    }

    public static void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for(int i = 0; i < parent.transform.childCount; i++){
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }

    public void OnClickMainMenu()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");
    }

}
