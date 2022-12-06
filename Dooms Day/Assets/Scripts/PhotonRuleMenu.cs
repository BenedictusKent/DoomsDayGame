using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PhotonRuleMenu : MonoBehaviourPunCallbacks
{
    public void GoMultiplePlayers()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        SceneManager.LoadScene("Lobby");
    }
}
