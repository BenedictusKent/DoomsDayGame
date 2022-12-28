using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SingleGame : MonoBehaviourPunCallbacks
{
    public void PlayGameEasy()
    {
        DataBase.AILevel = 0;
        DataBase.AIattacktime = 1.2f;
        DataBase.AImovetime = 0.75f;
        GoMultiplePlayers();
        //SceneManager.LoadScene("Game");
    }

    public void PlayGameNormal()
    {
        DataBase.AILevel = 1;
        DataBase.AIattacktime = 0.65f;
        DataBase.AImovetime = 0.65f;
        GoMultiplePlayers();
        //SceneManager.LoadScene("Game");
    }

    public void PlayGameHard()
    {
        DataBase.AILevel = 2;
        DataBase.AIattacktime = 0.4f;
        DataBase.AImovetime = 0.4f;
        GoMultiplePlayers();
        //SceneManager.LoadScene("Game");
    }

    public void PlayGameHell()
    {
        DataBase.AILevel = 3;
        DataBase.AIattacktime = 0.3f;
        DataBase.AImovetime = 0.3f;
        GoMultiplePlayers();
        //SceneManager.LoadScene("Game");
    }

    private void GoMultiplePlayers()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.CreateRoom("", new Photon.Realtime.RoomOptions { IsVisible = false });
        PhotonNetwork.LocalPlayer.NickName = "Player";
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("OnlineGame");
    }
}
